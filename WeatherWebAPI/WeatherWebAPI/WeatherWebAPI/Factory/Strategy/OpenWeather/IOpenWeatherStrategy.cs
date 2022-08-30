using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public interface IOpenWeatherStrategy
    {
        Task<List<CityDto>> GetCityDataFor(string city);
        Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}