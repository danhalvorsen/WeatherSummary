using FluentValidation.AspNetCore;
using System.Reflection;
using WeatherWebAPI;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddTransient( typeof(IFactory), typeof(StrategyBuilderFactory) );

builder.Services.AddConfig(builder.Configuration);

builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddAutoMapper(new List<Assembly> { Assembly.GetExecutingAssembly() });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();


public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(
         this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient( typeof(IFactory), typeof(StrategyBuilderFactory) );
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<DateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<BetweenDateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<WeekQueryAndCity>();
        });

        return services;
    }
}
