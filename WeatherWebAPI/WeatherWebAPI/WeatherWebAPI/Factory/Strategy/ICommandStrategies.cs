using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy
{
    public interface IAddCityToDatabaseStrategy : IStrategy
    {
        Task Add(List<CityDto> city);
    }

    public interface IAddScoreToDatabaseStrategy : IStrategy
    {
        Task Add(List<Scores> scores);
    }

    public interface IAddWeatherDataToDatabaseStrategy : IStrategy
    {
        Task Add(List<WeatherForecast.WeatherData> weatherData);
    }
}
