using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.StartUp;

public static class RegisterConfigurationsExtensions
{
    public static IServiceCollection RegisterConfigurations(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseConfig, DatabaseConfig>();
        services.AddSingleton<YrConfig>();
        services.AddSingleton<OpenWeatherConfig>();

        return services;
    }
}





