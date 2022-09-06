using FluentValidation.AspNetCore;
using System.Reflection;
using WeatherWebAPI;
using WeatherWebAPI.DAL;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddConfig();

builder.Services.AddHostedService<BackgroundServiceGetWeatherData>();
builder.Services.AddHostedService<BackgroundServiceGetScore>();
builder.Services.AddAutoMapper(new List<Assembly> { Assembly.GetExecutingAssembly() });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<YrStrategy>();
builder.Services.AddHttpClient<OpenWeatherStrategy>();

//builder.Services.AddSingleton<IFactory<IStrategy, StrategyType>, StrategyBuilderFactory>();
//builder.Services.AddSingleton<IFactory<IStrategy, StrategyType>, CommandFactory>();
//builder.Services.AddSingleton<IFactory<IStrategy, StrategyType>, QueryFactory>();
//builder.Services.AddSingleton<IMyConfiguration>(new BackgroundServiceGetWeatherDataCommandConfiguration());

builder.Services.AddCommonArgs(builder.Configuration);


builder.Services.AddTransient<IGetCitiesQuery, GetCitiesQuery>();
builder.Services.AddTransient<IGetWeatherDataForRatingQuery, GetWeatherDataForRatingQuery>();


builder.Services.AddTransient<BackgroundServiceGetWeatherDataCommand>();
builder.Services.AddTransient<BackgroundServiceCalculateScoreQuery>();
//builder.Services.AddHttpClient<WeatherApiStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception e)
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var logger = serviceProvider.GetService<ILogger<Program>>();

    logger?.LogError(e.Message);
}



public static class RegisterValidatorsExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection Services)
    {
        Services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<DateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<BetweenDateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<WeekQueryAndCity>();
        });

        return Services;
    }
}






