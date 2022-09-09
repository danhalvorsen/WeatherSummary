using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.StartUp;

public static class RegistertHttpClientsExtensions
{
    public static IServiceCollection RegisterHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<YrStrategy>();
        services.AddHttpClient<OpenWeatherStrategy>();
        //services.AddHttpClient<WeatherApiStrategy>();

        return services;
    }
}





