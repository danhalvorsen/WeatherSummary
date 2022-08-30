using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory.Strategy.WeatherApi;

namespace WeatherWebAPI.Automapper.Profiles
{
    public class WeatherApiProfile : Profile
    {
        private const double CONVERT_TO_METER_PER_SECOND = 5.0 / 18.0;
        private const int MAX_VALUE_VISIBILITY = 10;
        private const int PERCENTAGE_FACTOR = 10;

        eDataSource eDataSource => eDataSource.WeatherApi;

        public WeatherApiProfile()
        {
            CreateMap<Hour, WeatherForecast.WeatherData>()
                .ForMember(dest => dest.DateForecast, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.time_epoch)))
                .ForMember(dest => dest.WeatherType, opt => opt.MapFrom(src => src.condition!.text))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.temp_c))
                .ForMember(dest => dest.Windspeed, opt => opt.MapFrom(src => Math.Round(src.wind_kph * CONVERT_TO_METER_PER_SECOND, 2)))
                .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.wind_degree))
                .ForMember(dest => dest.WindspeedGust, opt => opt.MapFrom(src => Math.Round(src.gust_kph * CONVERT_TO_METER_PER_SECOND, 2)))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.pressure_mb))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.humidity))
                .ForMember(dest => dest.ProbOfRain, opt => opt.MapFrom(src => src.chance_of_rain))
                .ForMember(dest => dest.AmountRain, opt => opt.MapFrom(src => src.precip_mm))
                .ForMember(dest => dest.CloudAreaFraction, opt => opt.MapFrom(src => src.cloud))
                .ForMember(dest => dest.FogAreaFraction, opt => opt.MapFrom(src => VisibilityConvertedToFogAreaFraction(src.vis_km)))
                .AfterMap((src, dest) => dest.Date = DateTime.UtcNow.Date)
                .AfterMap((src, dest) => dest.Source.DataProvider = eDataSource.ToString());
            // WeatherApi doesn't have prob of thunder

            CreateMap<ApplicationWeatherApi, WeatherForecast>()
                .ForMember(dest => dest.Forecast, opt => opt.MapFrom(src => src.forecast!.forecastday!.SelectMany(i => i.hour!)));
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);/*ToLocalTime();*/
            return dateTime;
        }

        private static double VisibilityConvertedToFogAreaFraction(double value)
        {
            return Math.Abs((value - MAX_VALUE_VISIBILITY) * PERCENTAGE_FACTOR);
        }
    }
}


