using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public interface IOpenWeatherFetchCityStrategy : IStrategy
    {
        public Task<List<CityDto>> GetCityDataFor(string city);
    }
}
