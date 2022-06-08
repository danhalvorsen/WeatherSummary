using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public interface IOpenWeatherStrategy
    {
        Task<List<CityDto>> GetCityDataFor(string city);
        Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate);
    }
}