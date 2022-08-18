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

        public WeatherApiStrategy(WeatherApiConfig config)
        {
            _weatherApiConfig = config;
        }

        public async Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = _weatherApiConfig.BaseUrl
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var httpClient = _httpClientFactory.CreateClient("Yr");
            var response = await httpClient.GetAsync($"v1/forecast.json?key=27c1ef24ebc84fe5a8e81359221608&q={city.Name}&days=8&aqi=no&alerts=no");

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
