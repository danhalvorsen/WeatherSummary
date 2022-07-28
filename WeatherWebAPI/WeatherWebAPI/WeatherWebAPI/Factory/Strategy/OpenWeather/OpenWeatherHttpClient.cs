using Microsoft.Net.Http.Headers;

namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class OpenWeatherHttpClient
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla / 5.0(Windows 10, Win64; x64; rv: 100.0) Gecko / 20100101 FireFox / 100.0");
        }
    }
}
