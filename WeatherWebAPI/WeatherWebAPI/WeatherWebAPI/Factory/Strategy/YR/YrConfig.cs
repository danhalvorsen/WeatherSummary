using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrConfig : IHttpConfig
    {
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }

        public YrConfig()
        {
            BaseUrl = new Uri("https://api.met.no/weatherapi/");
            HomePage = new Uri("https://www.yr.no/");
        }
    }

}
