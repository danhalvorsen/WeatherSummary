using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IUpdateWeatherDataToDatabaseStrategy
    {
        Task Update(WeatherForecast weatherData, CityDto city, DateTime dateToBeUpdated);
    }
}
