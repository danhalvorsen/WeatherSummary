using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public interface IWeatherApiStrategy
    {
        Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}
