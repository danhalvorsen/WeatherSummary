using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace WeatherWebAPI.Automapper.Profiles
{
    public class OpenWeatherProfile : Profile
    {
        private const int PERCENTAGE_FACTOR = 100;

        static WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

        public OpenWeatherProfile()
        {
            CreateMap<ApplicationOpenWeather, CityDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.city!.name))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.city!.lon))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.city!.lat))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.city!.country));

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CreateMap<Daily, WeatherForecast.WeatherData>()
                .ForMember(dest => dest.DateForecast, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.dt)))
                .ForMember(dest => dest.WeatherType, opt => opt.MapFrom(src => src.weather[0].description))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.temp.day))
                .ForMember(dest => dest.Windspeed, opt => opt.MapFrom(src => src.wind_speed))
                .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.wind_deg))
                .ForMember(dest => dest.WindspeedGust, opt => opt.MapFrom(src => src.wind_gust))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.humidity))
                .ForMember(dest => dest.ProbOfRain, opt => opt.MapFrom(src => src.pop * PERCENTAGE_FACTOR))
                .ForMember(dest => dest.AmountRain, opt => opt.MapFrom(src => src.rain))
                .ForMember(dest => dest.CloudAreaFraction, opt => opt.MapFrom(src => src.clouds))
                .AfterMap((src, dest) => dest.Date = DateTime.UtcNow.Date)
                .AfterMap((src, dest) => dest.Source.DataProvider = WeatherProvider.ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            // OpenWeather doesn't show fog area fraction or thunder for dailiy forecasts.

            CreateMap<ApplicationOpenWeather, WeatherForecast>()
                .ForMember(dest => dest.Forecast, opt => opt.MapFrom(src => src.daily));

            
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);/*ToLocalTime();*/
            return dateTime;
        }
    }
}