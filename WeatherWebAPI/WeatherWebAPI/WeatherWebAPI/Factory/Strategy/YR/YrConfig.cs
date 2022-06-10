using AutoMapper;
using WeatherWebAPI.YR;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrConfig : IHttpConfig
    {
        private MapperConfiguration mapperConfig;
        public string? DataSource { get; }
        //public string? Uri { get; set; }
        //public string? GeoUri { get; set; }
        public Uri? BaseUrl { get; }
        //public Uri? BaseGeoUrl { get; }
        public Uri? HomePage { get; set; }

        public MapperConfiguration MapperConfig { get => mapperConfig; set => mapperConfig = value; }

        public YrConfig()
        {
            DataSource = "Yr";
            //Uri = "";
            BaseUrl = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
            HomePage = new Uri("https://www.yr.no/");
            //BaseGeoUrl = null;
            //GeoUri = "";
            mapperConfig = MapperConfig;
        }

        public MapperConfiguration Get(object? queryDate)
        {
            MapperConfig = new MapperConfiguration(
            cfg => cfg.CreateMap<ApplicationYr, WeatherForecastDto>()
            .ForPath(dest => dest.Date, opt => opt         // date
            .MapFrom(src => src.properties.meta.updated_at)) // (OR SHOULD WE HAVE TIMESERIES WHERE ITS ADDED FROM?)
            .ForPath(dest => dest.WeatherType, opt => opt  // weathertype
               .MapFrom(src => src.properties.timeseries
                   .ToList()
                   .Single(i => i.time.Equals(queryDate))
                       .data.next_6_hours.summary.symbol_code))
            .ForPath(dest => dest.Temperature, opt => opt  // temperature
            .MapFrom(src => src.properties.timeseries
                .ToList()
                    .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.air_temperature))
            .ForPath(dest => dest.Windspeed, opt => opt    // windspeed
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.wind_speed))
            .ForPath(dest => dest.WindDirection, opt => opt // wind direction
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.wind_from_direction))
            .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.wind_speed_of_gust))
            .ForPath(dest => dest.Pressure, opt => opt     // pressure
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.air_pressure_at_sea_level))
            .ForPath(dest => dest.Humidity, opt => opt     // humidity
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.relative_humidity))
            .ForPath(dest => dest.ProbOfRain, opt => opt   // probability of percipitation (probability of rain)
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.next_6_hours.details.probability_of_precipitation))
            .ForPath(dest => dest.AmountRain, opt => opt   // percipitation amount (amount of rain)
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.next_6_hours.details.precipitation_amount))
            .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.cloud_area_fraction))
            .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.instant.details.fog_area_fraction))
            .ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
            .MapFrom(src => src.properties.timeseries
                .ToList()
                .Single(i => i.time.Equals(queryDate))
                    .data.next_1_hours.details.probability_of_thunder))
            .AfterMap((s, d) => d.Source.DataProvider = "Yr") // DataSource.ToString().Replace("Strategy", "")) // Adding the datasource name to weatherforceastdto
            );

            return MapperConfig;
        }
    }

}
