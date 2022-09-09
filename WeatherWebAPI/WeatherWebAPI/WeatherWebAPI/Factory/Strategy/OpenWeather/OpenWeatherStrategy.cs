using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherStrategy : IOpenWeatherStrategy
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public StrategyType StrategyType => StrategyType.OpenWeather;
        public WeatherProvider WeatherProvider => WeatherProvider.OpenWeather;

        public OpenWeatherStrategy(IMapper mapper, OpenWeatherConfig config, HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = config.BaseUrl;

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox/100.0");
        }

        public async Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await _httpClient.GetAsync($"data/2.5/onecall?lat={city.Latitude}&lon={city.Longitude}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

                var resultWeatherData = _mapper.Map<WeatherForecast>(weatherData);

                return resultWeatherData.GetByDate(queryDate.Date + new TimeSpan(11, 0, 0));
            }

            return new WeatherForecast.WeatherData();
        }

        public string GetDataSource()
        {
            return WeatherProvider.ToString();
        }
    }
}