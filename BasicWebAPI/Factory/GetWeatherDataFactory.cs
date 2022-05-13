using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using BasicWebAPI.YR;
using System.Linq;

namespace BasicWebAPI.Factory
{

    public interface IStrategy
    {
        string DataSource { get; }
        Uri BaseUrl { get; }
        string Uri { get; }


    }

    public class YrStrategy : IStrategy
    {
        public YrStrategy()
        {
            DataSource = this.GetType().Name;
            Uri = $"complete?lat=58.970443618256432&lon=5.7331978346398227";
            BaseUrl = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
        }
        public string DataSource { get; }
        public string Uri { get; }
        public Uri BaseUrl { get; }
    }

    public class GetWeatherDataFactory
    {
        private /*readonly*/ IMapper _mapper;
        private MapperConfiguration _config;
        private static HttpClient httpClient;

        private MapperConfiguration CreateConfig(DateTime queryDate)
        {
            var config = new MapperConfiguration(
             cfg => cfg.CreateMap<Application, WeatherForecastDto>()
             .ForPath(dest => dest.Date, opt => opt.MapFrom(src => src.properties.meta.updated_at)) // date
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
                        .data.next_1_hours.details.probability_of_thunder))
             );

            return config;
        }

        public async Task<WeatherForecastDto> GetWeatherDataFrom(IStrategy strategy)
        {
            //-- HtppClient getting data from chosen strategy
            httpClient = new HttpClient();
            httpClient.BaseAddress = strategy.BaseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");
            var response = await httpClient.GetAsync(strategy.Uri);


            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var weatherData = JsonSerializer.Deserialize<Application>(responseBody);

                //-- Mapper
                DateTime queryDate = DateTime.Now;
                TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
                queryDate = queryDate.Date + ts;

                _config = CreateConfig(queryDate);
                _mapper = new Mapper(_config);

                var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

                return resultWeatherData;
            }
            else
                return null;
        }
    }
}
