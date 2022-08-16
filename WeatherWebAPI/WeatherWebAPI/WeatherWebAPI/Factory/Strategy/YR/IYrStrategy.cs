using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public interface IYrStrategy
    {
        Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}