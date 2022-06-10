using AutoMapper;
using WeatherWebAPI.OpenWeather;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherConfig : IHttpConfig
    {
        private MapperConfiguration mapperConfig;
        public string? DataSource { get; }
        //public string? Uri { get; set; }
        //public string? GeoUri { get; set; }
        public Uri? BaseUrl { get; }
        public Uri? BaseGeoUrl { get; }
        public Uri? HomePage { get; set; }

        public MapperConfiguration MapperConfig { get => mapperConfig; set => mapperConfig = value; }

        public OpenWeatherConfig()
        {
            DataSource = "OpenWeather";
            //Uri = "";
            BaseUrl = new Uri("http://api.openweathermap.org/data/2.5/");
            HomePage = new Uri("https://openweathermap.org/");
            BaseGeoUrl = new Uri("http://api.openweathermap.org/geo/1.0/");
            //GeoUri = "";
            mapperConfig = MapperConfig;
        }

        public MapperConfiguration Get(DateTime queryDate)
        {
            MapperConfig = new MapperConfiguration(

            cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecastDto>()
                      .ForPath(dest => dest.Date, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.current.dt))) // date <- this is an UNIX int type
                      .ForPath(dest => dest.WeatherType, opt => opt // weathertype
                          .MapFrom(src => src.current.weather[0].description)) // <-- Got a mapper exception once, because the city of stockholm had 2 descriptions. Just made this one
                                                                               // enter the first one each time. Should work.
                                                                               //.ToList()
                                                                               //.Single()
                                                                               //   .description))
                      .ForPath(dest => dest.Temperature, opt => opt  // temperature
                          .MapFrom(src => src.current.temp))
                      .ForPath(dest => dest.Windspeed, opt => opt // windspeed
                          .MapFrom(src => src.current.wind_speed))
                      .ForPath(dest => dest.WindDirection, opt => opt // wind direction
                          .MapFrom(src => src.current.wind_deg))
                      .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
                          .MapFrom(src => src.current.wind_gust))
                      .ForPath(dest => dest.Pressure, opt => opt // pressure
                          .MapFrom(src => src.current.pressure))
                      .ForPath(dest => dest.Humidity, opt => opt // humidity
                          .MapFrom(src => src.current.humidity))
                      .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
                          .MapFrom(src => src.hourly
                             .ToList()
                             .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
                                 .pop * 100))
                      .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
                         .MapFrom(src => src.minutely
                             .ToList()
                             .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
                                 .precipitation))
                      .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
                         .MapFrom(src => src.current.clouds))
                      .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
                         .MapFrom(src => src.current.visibility))
                       //.ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
                       //   .MapFrom(src => src.properties.timeseries
                       //       .ToList()
                       //       .Single(i => i.time.Equals(queryDate))
                       //           .data.next_1_hours.details.probability_of_thunder))
                       .AfterMap((s, d) => d.Source.DataProvider = DataSource) // Adding the datasource name to weatherforceastdto
                       .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction((double)d.FogAreaFraction))
                      );
            return MapperConfig;
        }

        // Need to convert from Unix to DateTime when fetching data from OpenWeather datasource and vice versa
        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private static int DateTimeToUnixTime(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime(); // If this is not done, the time would be 2 hours ahead of what we'd actually want.
            int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        private static double VisibilityConvertedToFogAreaFraction(double value)
        {
            return Math.Abs((value / 100) - 100);
        }
    }
}
