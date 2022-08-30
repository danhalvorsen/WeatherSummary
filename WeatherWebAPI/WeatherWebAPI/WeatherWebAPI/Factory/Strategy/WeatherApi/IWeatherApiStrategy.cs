using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public interface IWeatherApiStrategy
    {
        Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}
