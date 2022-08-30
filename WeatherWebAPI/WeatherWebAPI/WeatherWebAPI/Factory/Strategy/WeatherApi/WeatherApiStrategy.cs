using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public class WeatherApiStrategy : IGetWeatherDataStrategy<WeatherForecast.WeatherData>, IWeatherApiStrategy
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        eDataSource eDataSource => eDataSource.WeatherApi;

        public WeatherApiStrategy(IMapper mapper, WeatherApiConfig config, HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = config.BaseUrl;

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox /100.0");
        }

        public async Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            //var httpClient = _httpClientFactory.CreateClient("Yr");
            var response = await _httpClient.GetAsync($"v1/forecast.json?key=27c1ef24ebc84fe5a8e81359221608&q={city.Name}&days=8&aqi=no&alerts=no");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationWeatherApi>(responseBody);

                var resultWeatherData = _mapper.Map<WeatherForecast>(weatherData);
                return resultWeatherData.GetByDate(queryDate.Date + new TimeSpan(12, 0, 0));
            }

            return new WeatherForecast.WeatherData();
        }

        public string GetDataSource()
        {
            return eDataSource.ToString();
        }
    }
}
