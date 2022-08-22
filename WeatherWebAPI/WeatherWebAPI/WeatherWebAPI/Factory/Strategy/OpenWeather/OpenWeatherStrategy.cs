using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherStrategy : IGetWeatherDataStrategy<WeatherForecast>, IGetCityDataStrategy<CityDto>, IOpenWeatherStrategy
    {
        private readonly OpenWeatherConfig _openWeatherConfig;
        private readonly HttpClient _httpClient;

        public OpenWeatherStrategy(OpenWeatherConfig config, HttpClient httpClient)
        {
            _openWeatherConfig = config;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows 10, Win64; x64; rv: 100.0) Gecko/20100101 FireFox/100.0");
        }

        public async Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await _httpClient.GetAsync($"data/2.5/onecall?lat={city.Latitude}&lon={city.Longitude}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

                //Mapper
                _openWeatherConfig.Get(queryDate);


                var resultWeatherData = _openWeatherConfig.MapperConfig.CreateMapper().Map<WeatherForecast>(weatherData);
                return resultWeatherData;
            }

            return new WeatherForecast();
        }

        public async Task<List<CityDto>> GetCityDataFor(string city) // Have to use list when using streamasync
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            //var httpClient = _openWeatherConfig.HttpClientFactory.CreateClient("OpenWeather");
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

        public string GetDataSource()
        {
            return _openWeatherConfig.DataSource!;
        }
    }
}