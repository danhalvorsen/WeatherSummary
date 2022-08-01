using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetWeatherDataFromDatabaseStrategy
    {
        public List<WeatherForecastDto> Get(string queryString);
    }
}
