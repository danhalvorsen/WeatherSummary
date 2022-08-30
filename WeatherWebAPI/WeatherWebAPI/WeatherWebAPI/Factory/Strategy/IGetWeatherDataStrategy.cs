using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public enum eDataSource { Yr, OpenWeather, WeatherApi }

    public interface IGetWeatherDataStrategy<T>
    {
        public Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate);

        public string GetDataSource();
    }
}