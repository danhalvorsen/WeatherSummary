using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public interface IYrStrategy
    {
        Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}