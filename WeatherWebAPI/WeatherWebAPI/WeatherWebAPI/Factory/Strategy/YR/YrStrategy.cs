using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrStrategy : IGetWeatherDataStrategy<WeatherForecast>, IYrStrategy
    {
        private readonly IMapper _mapper;
        private readonly YrConfig _yrConfig;
        private readonly HttpClient _httpClient;
        

        public YrStrategy(IMapper mapper, YrConfig config, HttpClient httpClient)
        {
            _mapper = mapper;
            _yrConfig = config;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.met.no/weatherapi/");

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox/100.0");
        }

        public async Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await _httpClient.GetAsync($"locationforecast/2.0/complete?lat={city.Latitude}&lon={city.Longitude}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

                // Mapper
                //_yrConfig.Get(queryDate);

                var resultWeatherData = _mapper.Map<WeatherForecast>(weatherData);
                return resultWeatherData;
            }

            return new WeatherForecast();
        }

        public string GetDataSource()
        {
            return _yrConfig.DataSource!;
        }
    }
}