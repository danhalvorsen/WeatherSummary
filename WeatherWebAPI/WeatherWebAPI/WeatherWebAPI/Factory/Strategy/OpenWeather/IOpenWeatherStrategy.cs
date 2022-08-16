using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public interface IOpenWeatherStrategy
    {
        Task<List<CityDto>> GetCityDataFor(string city);
        Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}