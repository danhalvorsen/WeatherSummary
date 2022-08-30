using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public class WeatherApiConfig : IHttpConfig
    {
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }

        public WeatherApiConfig()
        {
            BaseUrl = new Uri("https://api.weatherapi.com/");
            HomePage = new Uri("https://www.weatherapi.com/");
        }
    }
}
