namespace WeatherWebAPI.StartUp;

public static class RegisterBackgroundServicesExtensions
{
    public static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<BackgroundServiceGetWeatherData>();
        services.AddHostedService<BackgroundServiceCalculateScore>();

        return services;
    }
}






