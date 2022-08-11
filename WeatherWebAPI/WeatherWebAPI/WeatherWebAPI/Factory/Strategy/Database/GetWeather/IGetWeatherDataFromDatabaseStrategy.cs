using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetWeatherDataFromDatabaseStrategy
    {
        public Task<List<WeatherForecastDto>> Get(string queryString);
    }
}
