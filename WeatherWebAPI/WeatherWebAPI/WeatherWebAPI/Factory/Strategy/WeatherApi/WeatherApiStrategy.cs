using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Factory.Strategy.WeatherApi
{
    public class WeatherApiStrategy : IGetWeatherDataStrategy<WeatherForecast>, IWeatherApiStrategy
    {
        private readonly WeatherApiConfig _weatherApiConfig;
        private readonly HttpClient _httpClient;

        public WeatherApiStrategy(WeatherApiConfig config, HttpClient httpClient)
        {
            _weatherApiConfig = config;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.weatherapi.com/");

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox /100.0");
        }

        public async Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            //var httpClient = _httpClientFactory.CreateClient("Yr");
            var response = await _httpClient.GetAsync($"v1/forecast.json?key=27c1ef24ebc84fe5a8e81359221608&q={city.Name}&days=8&aqi=no&alerts=no");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationWeatherApi>(responseBody);

                // Mapper
                _weatherApiConfig.Get(queryDate);

                var resultWeatherData = _weatherApiConfig.MapperConfig.CreateMapper().Map<WeatherForecast>(weatherData);
                return resultWeatherData;
            }

            return new WeatherForecast();
        }

        public string GetDataSource()
        {
            return _weatherApiConfig.DataSource!;
        }
    }
}
