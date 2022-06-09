using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public interface IYrStrategy
    {
        Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}