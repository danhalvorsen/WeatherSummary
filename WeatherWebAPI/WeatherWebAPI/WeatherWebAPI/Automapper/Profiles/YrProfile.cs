using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Automapper.Profiles
{
    public class YrProfile : Profile
    {
        public string? DataSource { get; }
        public DateTime QueryDate { get; set; }

        public YrProfile()
        {
            DataSource = "Yr";

            if (QueryDate.Date == DateTime.UtcNow.Date && DateTime.UtcNow.Hour > 12)
                QueryDate = QueryDate.Date + new TimeSpan(DateTime.UtcNow.Hour, 0, 0);
            else
                QueryDate = QueryDate.Date + new TimeSpan(12, 0, 0);

#pragma warning disable CS8602 // Dereference of a possibly null reference.

            CreateMap<Timeseries, WeatherForecast.Data>()
                .ForMember(dest => dest.DateForecast, opt => opt.MapFrom(src => src.time))
                .ForMember(dest => dest.WeatherType, opt => opt.MapFrom(src => src.data.next_6_hours.summary.symbol_code))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.data.instant.details.air_temperature))
                .ForMember(dest => dest.Windspeed, opt => opt.MapFrom(src => src.data.instant.details.wind_speed))
                .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.data.instant.details.wind_from_direction))
                .ForMember(dest => dest.WindspeedGust, opt => opt.MapFrom(src => src.data.instant.details.wind_speed_of_gust))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.data.instant.details.air_pressure_at_sea_level))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.data.instant.details.relative_humidity))
                .ForMember(dest => dest.ProbOfRain, opt => opt.MapFrom(src => src.data.next_6_hours.details.probability_of_precipitation))
                .ForMember(dest => dest.AmountRain, opt => opt.MapFrom(src => src.data.next_6_hours.details.precipitation_amount))
                .ForMember(dest => dest.CloudAreaFraction, opt => opt.MapFrom(src => src.data.instant.details.cloud_area_fraction))
                .ForMember(dest => dest.FogAreaFraction, opt => opt.MapFrom(src => src.data.instant.details.fog_area_fraction))
                .ForMember(dest => dest.ProbOfThunder, opt => opt.MapFrom(src => src.data.next_1_hours.details.probability_of_thunder));
            //.AfterMap((s, d) => d.Date = DateTime.UtcNow.Date);

            CreateMap<ApplicationYr, WeatherForecast>()
                .ForMember(dest => dest.Forecast, opt => opt.MapFrom(src => src.properties.timeseries));

#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}


