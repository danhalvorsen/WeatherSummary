using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetWeatherDataFromDatabaseStrategy
    {
        public Task<List<WeatherForecast>> Get(string queryString);
    }
}
