using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrStrategy : IGetWeatherDataStrategy<WeatherForecast>, IYrStrategy
    {
        private readonly YrConfig _yrConfig;
        //private readonly YrHttpClient _httpClient;

        public YrStrategy(YrConfig config/*, YrHttpClient httpClient*/)
        {
            _yrConfig = config;
            //_httpClient = httpClient;
        }

        public IEnumerable<YrHttpClient>? Response { get; set; }

        public async Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = _yrConfig.BaseUrl
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var httpClient = _httpClientFactory.CreateClient("Yr");
            var response = await httpClient.GetAsync($"locationforecast/2.0/complete?lat={city.Latitude}&lon={city.Longitude}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

                // Mapper
                _yrConfig.Get(queryDate);


                var resultWeatherData = _yrConfig.MapperConfig.CreateMapper().Map<WeatherForecast>(weatherData);
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