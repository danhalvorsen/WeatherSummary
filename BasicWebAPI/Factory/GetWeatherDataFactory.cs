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
using BasicWebAPI.OpenWeather;
using BasicWebAPI.Controllers;

namespace BasicWebAPI.Factory
{

    public interface IStrategy
    {
        string DataSource { get; }
        string Uri { get; set; }
        string GeoUri { get; set; }
        Uri BaseUrl { get; }
        Uri BaseGeoUrl { get; }
        Uri HomePage { get; set; }
        string MakeGeoUriCityCall(string city);
        string MakeUriWeatherCall(double lat, double lon);
        HttpClient MakeHttpClientConnection();
        HttpClient MakeGeoHttpClientConnection();
    }
    public class YrStrategy : IStrategy
    {
        public YrStrategy()
        {
            DataSource = this.GetType().Name;
            Uri = "";
            BaseUrl = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
            HomePage = new Uri("https://www.yr.no/");
            BaseGeoUrl = null;
            GeoUri = "";
        }

        public string DataSource { get; }
        public string Uri { get; set; }
        public string GeoUri { get; set; }
        public Uri BaseUrl { get; }
        public Uri BaseGeoUrl { get; }
        public Uri HomePage { get; set; }

        public string MakeGeoUriCityCall(string city)
        {
            return GeoUri = "";
        }

        public string MakeUriWeatherCall(double lat, double lon)
        {
            return Uri = $"complete?lat={lat}&lon={lon}";
        }

        public HttpClient MakeHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }

        public HttpClient MakeGeoHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseGeoUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }
    }

    public class OpenWeatherStrategy : IStrategy
    {
        public OpenWeatherStrategy()
        {
            DataSource = this.GetType().Name;
            Uri = "";
            BaseUrl = new Uri("http://api.openweathermap.org/data/2.5/");
            HomePage = new Uri("https://openweathermap.org/");
            BaseGeoUrl = new Uri("http://api.openweathermap.org/geo/1.0/");
            GeoUri = "";
        }

        public string DataSource { get; }
        public string Uri { get; set; }
        public string GeoUri { get; set; }
        public Uri BaseUrl { get; }
        public Uri BaseGeoUrl { get; }
        public Uri HomePage { get; set; }

        public string MakeGeoUriCityCall(string city)
        {
            return GeoUri = $"direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43";
        }

        public string MakeUriWeatherCall(double lat, double lon)
        {
            return Uri = $"onecall?lat={lat}&lon={lon}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43";
        }

        public HttpClient MakeHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }

        public HttpClient MakeGeoHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseGeoUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }
    }

    public class GetWeatherDataFactory
    {
        private IMapper _mapper;
        private MapperConfiguration _config;
        private static HttpClient httpClient;

        private MapperConfiguration CreateConfigForFetchingWeatherData(DateTime queryDate, IStrategy strategy)
        {
            MapperConfiguration config;

            if (strategy.GetType() == typeof(YrStrategy))
            {
                config = new MapperConfiguration(

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
                            .data.next_1_hours.details.probability_of_thunder))
                .AfterMap((s, d) => d.Source.DataProvider = strategy.DataSource.ToString().Replace("Strategy", "")));

                return config;
            }
            if (strategy.GetType() == typeof(OpenWeatherStrategy))
            {
                config = new MapperConfiguration(

                    cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecastDto>()
                 .ForPath(dest => dest.Date, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.current.dt))) // date <- this is an UNIX int type
                 .ForPath(dest => dest.WeatherType, opt => opt // weathertype
                     .MapFrom(src => src.current.weather
                         .ToList()
                         .Single()
                            .description))
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
                  .AfterMap((s, d) => d.Source.DataProvider = strategy.DataSource.ToString().Replace("Strategy", "")) // Adding the datasource name to weatherforceastdto
                  .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction(d.FogAreaFraction))
                 );
                return config;
            }
            else
                return null;
        }
        private MapperConfiguration CreateConfigForFetchingCityCoordinates()
        {
            MapperConfiguration config;

            config = new MapperConfiguration(

            cfg => cfg.CreateMap<ApplicationOpenWeather, CityDto>()
            .ForPath(dest => dest.Name, opt => opt
                .MapFrom(src => src.city.name)) // name
            .ForPath(dest => dest.Longitude, opt => opt
                .MapFrom(src => src.city.lon))
            .ForPath(dest => dest.Latitude, opt => opt
                .MapFrom(src => src.city.lat))
            .ForPath(dest => dest.Country, opt => opt
                .MapFrom(src => src.city.country))
            );
            return config;
        } // Not in use due to StreamAsync


        public async Task<WeatherForecastDto> GetWeatherDataFrom(double lat, double lon, IStrategy strategy)
        {
            //-- HtppClient getting data from chosen strategy
            httpClient = strategy.MakeHttpClientConnection();
            var response = await httpClient.GetAsync(strategy.MakeUriWeatherCall(lat, lon));

            if (response.IsSuccessStatusCode && strategy.GetType() == typeof(YrStrategy))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

                //-- Mapper
                DateTime queryDate = DateTime.UtcNow;
                TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
                queryDate = queryDate.Date + ts;

                _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
                _mapper = new Mapper(_config);

                var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

                return resultWeatherData;
            }
            if (response.IsSuccessStatusCode && strategy.GetType() == typeof(OpenWeatherStrategy))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

                //-- Mapper
                DateTime queryDate = DateTime.Now;
                TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
                queryDate = queryDate.Date + ts;

                _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
                _mapper = new Mapper(_config);

                var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

                return resultWeatherData;
            }
            else
                return null;
        }

        public async Task<List<CityDto>> GetCityDataFrom(string city, IStrategy strategy) // Have to use list when using streamasync
        {
            //try
            //{
            httpClient = strategy.MakeGeoHttpClientConnection();

            // By default BaseAddress = BaseUrl, so we have to change that here.
            var response = await httpClient.GetAsync(strategy.MakeGeoUriCityCall(city));
            await Task.CompletedTask;

            if (response.IsSuccessStatusCode)
            {
                var streamTask = httpClient.GetStreamAsync(strategy.GeoUri);
                var cityInfo = await JsonSerializer.DeserializeAsync<List<CityDto>>(await streamTask);

                return cityInfo;
            }
            else
            {
                Console.WriteLine("ikkje nesten response success");
            }
            return null;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Excepton message: {e.Message}");
            //}
            //return null;
            
            
            //if (response.IsSuccessStatusCode)
            //{
            //    var responseBody = response.Content.ReadAsStringAsync();

            //    var cityData = JsonSerializer.Deserialize<List<ApplicationOpenWeather>>(await responseBody);

            //    //-- Mapper
            //    _config = CreateConfigForFetchingCityCoordinates();
            //    _mapper = new Mapper(_config);

            //    var resultCityData = _mapper.Map<CityDto>(cityData[0]);

            //    return resultCityData;
            //}
            //else
            //    return null;
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
            dateTime = dateTime.ToUniversalTime();
            int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        private double VisibilityConvertedToFogAreaFraction(double value)
        {
            return Math.Abs((value / 100) - 100);
        }
    }
}