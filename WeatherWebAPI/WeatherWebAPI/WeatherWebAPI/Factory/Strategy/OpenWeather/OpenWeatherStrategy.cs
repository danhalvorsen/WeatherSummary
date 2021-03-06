using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherStrategy : IGetWeatherDataStrategy<WeatherForecastDto>, IGetCityDataStrategy<CityDto>, IOpenWeatherStrategy
    {
        private readonly OpenWeatherConfig _openWeatherConfig;

        public OpenWeatherStrategy(OpenWeatherConfig config)
        {
            _openWeatherConfig = config;
        }

        public async Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = _openWeatherConfig.BaseUrl
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var httpClient = _openWeatherConfig.HttpClientFactory.CreateClient("OpenWeather");
            var response = await httpClient.GetAsync($"data/2.5/onecall?lat={city.Latitude}&lon={city.Longitude}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

                //Mapper
                _openWeatherConfig.Get(queryDate);


                var resultWeatherData = _openWeatherConfig.MapperConfig.CreateMapper().Map<WeatherForecastDto>(weatherData);
                return resultWeatherData;
            }

            return new WeatherForecastDto();
        }

        public async Task<List<CityDto>> GetCityDataFor(string city) // Have to use list when using streamasync
        {
            var httpClient = new HttpClient
            {
                BaseAddress = _openWeatherConfig.BaseUrl
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var httpClient = _openWeatherConfig.HttpClientFactory.CreateClient("OpenWeather");
            var response = await httpClient.GetAsync($"geo/1.0/direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var streamTask = await httpClient.GetStreamAsync($"geo/1.0/direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");
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