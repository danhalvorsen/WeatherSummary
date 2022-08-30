using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherConfig : IHttpConfig
    {
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }

        public OpenWeatherConfig()
        {
            BaseUrl = new Uri("https://api.openweathermap.org/");
            HomePage = new Uri("https://openweathermap.org/");
        }
    }
}
