using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.Query
{
    public interface IGetDatesQuery
    {
        Task<List<WeatherForecast.WeatherData>> GetDatesForCity(string cityName, IGetWeatherDataStrategy<WeatherForecast> strategy);
    }
}
