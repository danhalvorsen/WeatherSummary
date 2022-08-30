using AutoMapper;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Automapper.Profiles
{
    public class WeatherForecastDtoProfile : Profile
    {
        public WeatherForecastDtoProfile()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CreateMap<WeatherForecast.WeatherData, WeatherForecastDto>()
                .ForPath(dest => dest.City, opt => opt
                    .MapFrom(src => src.City))
                .ForPath(dest => dest.DateForecast, opt => opt
                    .MapFrom(src => src.DateForecast))
                .ForPath(dest => dest.Date, opt => opt
                    .MapFrom(src => src.Date))
                .ForPath(dest => dest.WeatherType, opt => opt
                    .MapFrom(src => src.WeatherType))
                .ForPath(dest => dest.Temperature, opt => opt
                    .MapFrom(src => src.Temperature))
                .ForPath(dest => dest.Windspeed, opt => opt
                    .MapFrom(src => src.Windspeed))
                .ForPath(dest => dest.WindDirection, opt => opt
                    .MapFrom(src => src.WindspeedGust))
                .ForPath(dest => dest.Pressure, opt => opt
                    .MapFrom(src => src.Pressure))
                .ForPath(dest => dest.Humidity, opt => opt
                    .MapFrom(src => src.Humidity))
                .ForPath(dest => dest.ProbOfRain, opt => opt
                    .MapFrom(src => src.ProbOfRain))
                .ForPath(dest => dest.AmountRain, opt => opt
                    .MapFrom(src => src.AmountRain))
                .ForPath(dest => dest.CloudAreaFraction, opt => opt
                    .MapFrom(src => src.CloudAreaFraction))
                .ForPath(dest => dest.FogAreaFraction, opt => opt
                    .MapFrom(src => src.FogAreaFraction))
                .ForPath(dest => dest.ProbOfThunder, opt => opt
                    .MapFrom(src => src.ProbOfThunder))
                .ForPath(dest => dest.Source, opt => opt
                    .MapFrom(src => src.Source))
                .ForPath(dest => dest.Score.Value, opt => opt
                    .MapFrom(src => src.Score.Value))
                .ForPath(dest => dest.Score.ValueWeighted, opt => opt
                    .MapFrom(src => src.Score.ValueWeighted));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
