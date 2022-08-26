using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherConfig : BaseMapperConfigFunctions, IHttpConfig
    {
        private MapperConfiguration _mapperConfig;
        public string? DataSource { get; }
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }

        public MapperConfiguration MapperConfig { get => _mapperConfig; set => _mapperConfig = value; }

        public OpenWeatherConfig()
        {
            DataSource = "OpenWeather";
            BaseUrl = new Uri("https://api.openweathermap.org/");
            HomePage = new Uri("https://openweathermap.org/");
            _mapperConfig = MapperConfig;
        }

//        public MapperConfiguration Get(DateTime queryDate) // ADD DIFFERENT CFG FOR TODAY OR FUTURE??
//        {
//            if (queryDate.Date >= DateTime.UtcNow.Date) // Future Dates Config
//            {
//                queryDate = queryDate.Date + new TimeSpan(11, 0, 0);

//#pragma warning disable CS8604 // Possible null reference argument.
//#pragma warning disable CS8602 // Dereference of a possibly null reference.

//                MapperConfig = new MapperConfiguration(

//                cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecast>()
//                .ForPath(dest => dest.DateForecast, opt => opt
//                .MapFrom(src => UnixTimeStampToDateTime(src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).dt))) // date <- this is an UNIX int type
//                .ForPath(dest => dest.WeatherType, opt => opt // weathertype
//                    .MapFrom(src => src.daily 
//                       .ToList()
//                       .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).weather[0].description)) // <-- Got a mapper exception once, because the city of stockholm had 2 descriptions. Just made this one enter the first one each time. Should work.
//                .ForPath(dest => dest.Temperature, opt => opt  // temperature
//                    .MapFrom(src => src.daily
//                       .ToList()
//                       .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).temp.day))
//                .ForPath(dest => dest.Windspeed, opt => opt // windspeed
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).wind_speed))
//                .ForPath(dest => dest.WindDirection, opt => opt // wind direction
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).wind_deg))
//                .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).wind_gust))
//                .ForPath(dest => dest.Pressure, opt => opt // pressure
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).pressure))
//                .ForPath(dest => dest.Humidity, opt => opt // humidity
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).humidity))
//                .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
//                 .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).pop * 100))
//                .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
//                .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).rain))
//                .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
//                .MapFrom(src => src.daily
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate))).clouds))
//                .AfterMap((s, d) => d.Source.DataProvider = DataSource) // Adding the datasource name to weatherforceastdto
//                .AfterMap((s, d) => d.Date = DateTime.UtcNow.Date)
//                );
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//#pragma warning restore CS8604 // Possible null reference argument.
//            }
//            else
//            {
//                queryDate = queryDate.Date + new TimeSpan(DateTime.UtcNow.Hour + 3, 0, 0); // !!!! OBS Pass på daylightSAVE TIME ? !!!!!!!!!!!!

//#pragma warning disable CS8602 // Dereference of a possibly null reference.
//#pragma warning disable CS8604 // Possible null reference argument.
//                MapperConfig = new MapperConfiguration(

//                cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecast>()
//             .ForPath(dest => dest.DateForecast, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.current.dt))) // date <- this is an UNIX int type
//             .ForPath(dest => dest.WeatherType, opt => opt // weathertype
//                 .MapFrom(src => src.current.weather[0].description)) // <-- Got a mapper exception once, because the city of stockholm had 2 descriptions. This should work                                                   
//             .ForPath(dest => dest.Temperature, opt => opt  // temperature
//                 .MapFrom(src => src.current.temp))
//             .ForPath(dest => dest.Windspeed, opt => opt // windspeed
//                 .MapFrom(src => src.current.wind_speed))
//             .ForPath(dest => dest.WindDirection, opt => opt // wind direction
//                 .MapFrom(src => src.current.wind_deg))
//             .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
//                 .MapFrom(src => src.current.wind_gust))
//             .ForPath(dest => dest.Pressure, opt => opt // pressure
//                 .MapFrom(src => src.current.pressure))
//             .ForPath(dest => dest.Humidity, opt => opt // humidity
//                 .MapFrom(src => src.current.humidity))
//             .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
//                 .MapFrom(src => src.hourly
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
//                        .pop * 100))
//             .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
//                .MapFrom(src => src.hourly
//                    .ToList()
//                    .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
//                        .rain._1h))
//             .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
//                .MapFrom(src => src.current.clouds))
//             .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
//                .MapFrom(src => src.current.visibility))
//              .AfterMap((s, d) => d.Source.DataProvider = DataSource) // Adding the datasource name to weatherforceastdto
//              .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction(d.FogAreaFraction))
//              .AfterMap((s,d) => d.Date = DateTime.UtcNow.Date)
//                 );
//#pragma warning restore CS8604 // Possible null reference argument.
//#pragma warning restore CS8602 // Dereference of a possibly null reference.
//            }
//            return MapperConfig;
//        }
    }
}
