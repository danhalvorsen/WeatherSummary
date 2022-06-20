using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IAddWeatherDataToDatabaseStrategy
    {
        Task Add(WeatherForecastDto weatherData, CityDto city);
    }
}
