using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.OpenWeather;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherStrategy : IGetWeatherDataStrategy<WeatherForecastDto>, IGetCityDataStrategy<CityDto>, IOpenWeatherStrategy
    {
        private readonly OpenWeatherConfig openWeatherConfig;


        public OpenWeatherStrategy(OpenWeatherConfig config)
        {
            openWeatherConfig = config;
        }

        public async Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = openWeatherConfig.BaseUrl
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            var response = await httpClient.GetAsync($"onecall?lat={city.Latitude}&lon={city.Longitude}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

                // Mapper
                TimeSpan ts = new TimeSpan(queryDate.Hour + 1, 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
                queryDate = queryDate.Date + ts;
                openWeatherConfig.Get(queryDate);


                var resultWeatherData = openWeatherConfig.MapperConfig.CreateMapper().Map<WeatherForecastDto>(weatherData);
                return resultWeatherData;
            }

            return new WeatherForecastDto();
        }

        public async Task<List<CityDto>> GetCityDataFor(string city) // Have to use list when using streamasync
        {
            var httpClient = new HttpClient
            {
                BaseAddress = openWeatherConfig.BaseGeoUrl
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");
            var response = await httpClient.GetAsync($"direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");

            if (response.IsSuccessStatusCode)
            {
                var streamTask = httpClient.GetStreamAsync($"direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43");
                var cityInfo = await JsonSerializer.DeserializeAsync<List<CityDto>>(await streamTask);

                return cityInfo;
            }

            return new List<CityDto>();
        }
    }
}