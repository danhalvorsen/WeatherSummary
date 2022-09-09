using WeatherWebAPI.DAL;
using WeatherWebAPI.DAL.Commands;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.StartUp;

public static class RegisterServicesExtensions 
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<SetTimeHour>();
        services.AddSingleton<SetTimeHourValidator>();
       
        services.AddSingleton<IBackgroundServiceFetchWeatherDataQuery, BackgroundServiceFetchWeatherDataQuery>();
        services.AddSingleton<IBackgroundServiceCalculateScoreQuery, BackgroundServiceCalculateScoreQuery>();

        services.AddTransient<IGetDatesForCityQuery, GetDatesForCityQuery>();
        services.AddTransient<IGetCitiesQuery, GetCitiesQuery>();
        services.AddTransient<IGetWeatherDataForRatingQuery, GetWeatherDataForRatingQuery>();

        services.AddTransient<IFetchCityCommand, FetchCityCommand>();
        
        return services;
    }
}





