using FluentValidation.AspNetCore;
using Microsoft.Net.Http.Headers;
using System.Reflection;
using WeatherWebAPI;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddHostedService<BackgroundServiceGetWeatherData>();
builder.Services.AddHostedService<BackgroundServiceGetScore>();
builder.Services.AddAutoMapper(new List<Assembly> { Assembly.GetExecutingAssembly() });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<YrHttpClient>();
builder.Services.AddHttpClient<OpenWeatherHttpClient>();

//builder.Services.AddHttpClient<OpenWeatherStrategy>(httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");

//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.Accept, "application/json");
//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.UserAgent, "Mozilla / 5.0(Windows 10, Win64; x64; rv: 100.0) Gecko / 20100101 FireFox / 100.0");
//});
//builder.Services.AddHttpClient("Yr", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://api.met.no/weatherapi/");

//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.Accept, "application/json");
//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.UserAgent, "Mozilla / 5.0(Windows 10, Win64; x64; rv: 100.0) Gecko / 20100101 FireFox / 100.0");
//});

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
        services.AddTransient(typeof(IFactory), typeof(StrategyBuilderFactory));
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<DateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<BetweenDateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<WeekQueryAndCity>();
        });

        return services;
    }
}
