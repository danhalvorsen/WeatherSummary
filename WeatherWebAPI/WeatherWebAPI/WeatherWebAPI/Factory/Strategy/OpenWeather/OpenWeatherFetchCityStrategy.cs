using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherFetchCityStrategy : IOpenWeatherFetchCityStrategy
    {
        private readonly HttpClient _httpClient;
        public StrategyType StrategyType => StrategyType.OpenWeatherGetCity;

        public OpenWeatherFetchCityStrategy(OpenWeatherConfig config, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = config.BaseUrl;

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox/100.0");
        }

        public async Task<List<CityDto>> GetCityDataFor(string city) // Have to use list when using streamasync
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await _httpClient.GetAsync($"geo/1.0/direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var streamTask = await _httpClient.GetStreamAsync($"geo/1.0/direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");
                var cityData = await JsonSerializer.DeserializeAsync<List<CityDto>>(streamTask);

                if (cityData != null)
                    return cityData;
                throw new Exception();
            }

            return new List<CityDto>();
        }
    }
}
