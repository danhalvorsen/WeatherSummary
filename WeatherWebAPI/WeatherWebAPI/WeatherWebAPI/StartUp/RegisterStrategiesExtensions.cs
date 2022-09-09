using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.StartUp;


public static class RegisterStrategiesExtensions
{
    public static IServiceCollection RegisterStrategies(this IServiceCollection services)
    {
        services.AddTransient<IYrStrategy, YrStrategy>();
        services.AddTransient<IOpenWeatherStrategy, OpenWeatherStrategy>();
        services.AddTransient<IOpenWeatherFetchCityStrategy, OpenWeatherFetchCityStrategy>();
        services.AddTransient<IAddWeatherDataToDatabaseStrategy, AddWeatherDataToDatabaseStrategy>();
        services.AddTransient<IAddCityToDatabaseStrategy, AddCityToDatabaseStrategy>();
        services.AddTransient<IAddScoreToDatabaseStrategy, AddScoreToDatabaseStrategy>();
        services.AddTransient<IGetWeatherDataFromDatabaseStrategy, GetWeatherDataFromDatabaseStrategy>();
        services.AddTransient<IGetScoreFromDatabaseStrategy, GetScoreFromDatabaseStrategy>();
        
        services.AddTransient<StrategyResolver>(strategyProvider => weatherProvider =>
            {
                switch (weatherProvider)
                {
                    case WeatherProvider.Yr:
                        return strategyProvider.GetService<IYrStrategy>()!;
                    case WeatherProvider.OpenWeather:
                        return strategyProvider.GetService<IOpenWeatherStrategy>()!;
                    default:
                        throw new KeyNotFoundException();
                }
            });

        return services;
    }
}





