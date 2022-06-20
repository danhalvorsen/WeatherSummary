using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IUpdateWeatherDataToDatabaseStrategy
    {
        Task Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated);
    }
}
