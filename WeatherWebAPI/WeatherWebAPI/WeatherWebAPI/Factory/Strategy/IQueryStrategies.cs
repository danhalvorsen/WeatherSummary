using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy
{
    public interface IGetWeatherDataFromDatabaseStrategy : IStrategy
    {
        public Task<List<WeatherForecast.WeatherData>> Get(string queryString);
    }

    public interface IGetScoreFromDatabaseStrategy : IStrategy
    {
        public Task<List<Scores>> Get();
    }
}
