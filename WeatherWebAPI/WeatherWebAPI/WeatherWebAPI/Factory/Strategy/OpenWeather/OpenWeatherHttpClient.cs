using Microsoft.Net.Http.Headers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherConfig _openWeatherConfig;

        public OpenWeatherHttpClient(OpenWeatherConfig openWeatherConfig, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _openWeatherConfig = openWeatherConfig;

            _httpClient.BaseAddress = _openWeatherConfig.BaseUrl;

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla / 5.0(Windows 10, Win64; x64; rv: 100.0) Gecko / 20100101 FireFox / 100.0");
        }
    }
}
