using AutoMapper;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrConfig : IHttpConfig
    {

        private MapperConfiguration _mapperConfig;
        //private readonly IHttpClientFactory _httpClientFactory;
        public string? DataSource { get; }
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }

        public MapperConfiguration MapperConfig { get => _mapperConfig; set => _mapperConfig = value; }
        //public IHttpClientFactory HttpClientFactory { get => _httpClientFactory; }

        public YrConfig()
        {
            DataSource = "Yr";
            BaseUrl = new Uri("https://api.met.no/weatherapi/");
            HomePage = new Uri("https://www.yr.no/");
            _mapperConfig = MapperConfig;
            //_httpClientFactory = HttpClientFactory;
        }

        public MapperConfiguration Get(DateTime queryDate)
        {
            if (queryDate.Date >= DateTime.UtcNow.Date)
                queryDate = queryDate.Date + new TimeSpan(12, 0, 0);

            //if (queryDate.Date == DateTime.UtcNow.Date)
            //    queryDate = queryDate.Date + new TimeSpan(DateTime.UtcNow.Hour + 1, 0, 0);


#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            MapperConfig = new MapperConfiguration(
            cfg => cfg.CreateMap<ApplicationYr, WeatherForecastDto>()
            .ForPath(dest => dest.DateForecast, opt => opt         // dateforecast
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                        .Single(i => i.time.Equals(queryDate)).time))
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
            .AfterMap((s, d) => d.Source.DataProvider = DataSource) // Adding the datasource name to weatherforceastdto
            .AfterMap((s, d) => d.Date = DateTime.UtcNow.Date)
            );
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return MapperConfig;
        }
    }

}
