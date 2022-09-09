using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Automapper
{
    public enum eDataSource { Yr, OpenWeather, WeatherApi }

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<WeatherForecast.WeatherData, WeatherForecast>();
            CreateMap<ApplicationYr, WeatherForecast>();
            CreateMap<ApplicationOpenWeather, WeatherForecast>();
            CreateMap<ApplicationOpenWeather, CityDto>();
            CreateMap<ApplicationWeatherApi, WeatherForecast>();
        }
    }
}


