using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public class WeatherApiConfig : BaseMapperConfigFunctions, IHttpConfig
    {
        private MapperConfiguration _mapperConfig;
        public string? DataSource { get; }
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }
        public MapperConfiguration MapperConfig { get => _mapperConfig; set => _mapperConfig = value; }

        public WeatherApiConfig()
        {
            DataSource = "WeatherApi";
            BaseUrl = new Uri("https://api.weatherapi.com/");
            HomePage = new Uri("https://www.weatherapi.com/");
            _mapperConfig = MapperConfig;
        }

        public MapperConfiguration Get(DateTime queryDate)
        {
            if (queryDate.Date >= DateTime.UtcNow.Date)
                queryDate = queryDate.Date + new TimeSpan(12, 0, 0);


#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            MapperConfig = new MapperConfiguration(
        cfg => cfg.CreateMap<ApplicationWeatherApi, WeatherForecast>()
            .ForPath(dest => dest.DateForecast, opt => opt
                .MapFrom(src => UnixTimeStampToDateTime(src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).time_epoch)))
            .ForPath(dest => dest.WeatherType, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).condition.text))
            .ForPath(dest => dest.Temperature, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).temp_c))
            .ForPath(dest => dest.Windspeed, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).wind_kph))
            .ForPath(dest => dest.WindDirection, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).wind_degree))
            .ForPath(dest => dest.WindspeedGust, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).gust_kph))
            .ForPath(dest => dest.Pressure, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).pressure_mb))
            .ForPath(dest => dest.Humidity, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).humidity))
            .ForPath(dest => dest.ProbOfRain, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).chance_of_rain))
            .ForPath(dest => dest.AmountRain, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).precip_mm))
            .ForPath(dest => dest.CloudAreaFraction, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).cloud))
            .ForPath(dest => dest.FogAreaFraction, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).vis_km))
            .AfterMap((s, d) => d.Source.DataProvider = DataSource)
            .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction(d.FogAreaFraction))
            .AfterMap((s, d) => d.Date = DateTime.UtcNow.Date)
            .AfterMap((s, d) => d.Windspeed = (float)Math.Round(d.Windspeed * 5 / 18, 2))
            .AfterMap((s, d) => d.WindspeedGust = (float)Math.Round(d.WindspeedGust * 5 / 18, 2))
            );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

            return MapperConfig;
        }
    }
}
