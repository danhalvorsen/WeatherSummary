using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.Query
{
    public interface IGetDatesForCityQuery
    {
        Task<List<WeatherForecast.WeatherData>> GetDatesForCity(string cityName, IGetWeatherDataStrategy strategy);
    }
}
