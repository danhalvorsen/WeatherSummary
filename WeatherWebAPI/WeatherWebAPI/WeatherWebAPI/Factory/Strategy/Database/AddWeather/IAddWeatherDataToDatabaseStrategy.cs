using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IAddWeatherDataToDatabaseStrategy
    {
        Task Add(WeatherForecast weatherData, CityDto city);
    }
}
