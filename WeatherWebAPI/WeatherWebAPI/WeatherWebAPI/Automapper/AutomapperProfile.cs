using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Automapper
{
    public class AutomapperProfile : Profile
    {
        public string? DataSource { get; }

        public AutomapperProfile()
        {
            CreateMap<ApplicationYr, WeatherForecast>();
            CreateMap<ApplicationOpenWeather, WeatherForecast>();
            CreateMap<ApplicationOpenWeather, CityDto>();
            CreateMap<ApplicationWeatherApi, WeatherForecast>();
        }
    }
}


