using AutoMapper;
using WeatherWebAPI.YR;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrAutomapperConfig
    {
        private MapperConfiguration config;

        public YrAutomapperConfig()
        {
            config = Config;
        }

        public MapperConfiguration Config { get => config; set => config = value; }

        MapperConfiguration Get(object? queryDate)
        {
            Config =  new MapperConfiguration(

               cfg => cfg.CreateMap<ApplicationYr, WeatherForecastDto>()
               .ForPath(dest => dest.Date, opt => opt.MapFrom(src => src.properties.meta.updated_at)) // date
               .ForPath(dest => dest.WeatherType, opt => opt // weathertype
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.next_1_hours.summary.symbol_code))
               .ForPath(dest => dest.Temperature, opt => opt  // temperature
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.instant.details.air_temperature))
               .ForPath(dest => dest.Windspeed, opt => opt // windspeed
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
               .ForPath(dest => dest.Pressure, opt => opt // pressure
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.instant.details.air_pressure_at_sea_level))
               .ForPath(dest => dest.Humidity, opt => opt // humidity
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.instant.details.relative_humidity))
               .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.next_1_hours.details.probability_of_precipitation))
               .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
                   .MapFrom(src => src.properties.timeseries
                       .ToList()
                       .Single(i => i.time.Equals(queryDate))
                           .data.next_1_hours.details.precipitation_amount))
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
                           .data.next_1_hours.details.probability_of_thunder)));

            return Config;
            //.AfterMap((s, d) => d.Source.DataProvider = this.DataSource.ToString().Replace("Strategy", "")));
        }
    }}
