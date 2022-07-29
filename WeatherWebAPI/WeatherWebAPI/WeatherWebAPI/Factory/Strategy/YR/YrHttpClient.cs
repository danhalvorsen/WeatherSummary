using Microsoft.Net.Http.Headers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrHttpClient : YrConfig
    {
        private readonly HttpClient _httpClient;

        public YrHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.met.no/weatherapi/");

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla / 5.0(Windows 10, Win64; x64; rv: 100.0) Gecko / 20100101 FireFox / 100.0");
        }
    }
}
