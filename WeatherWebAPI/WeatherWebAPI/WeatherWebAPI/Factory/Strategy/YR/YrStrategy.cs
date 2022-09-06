using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrStrategy : IGetWeatherDataStrategy
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        
        public WeatherProvider WeatherProvider => WeatherProvider.Yr;
        public WeatherWebAPI.StrategyType StrategyType => WeatherWebAPI.StrategyType.Yr;

        public YrStrategy(IMapper mapper, YrConfig config, HttpClient httpClient)
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

            var response = await _httpClient.GetAsync($"locationforecast/2.0/complete?lat={city.Latitude}&lon={city.Longitude}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

                var resultWeatherData = _mapper.Map<WeatherForecast>(weatherData);

                return resultWeatherData.GetByDate(SetQueryDate(queryDate));
            }

            return new WeatherForecast.WeatherData();
        }

        private static DateTime SetQueryDate(DateTime queryDate)
        {
            if (queryDate.Date == DateTime.UtcNow.Date && DateTime.UtcNow.Hour > 12)
                queryDate = queryDate.Date + new TimeSpan(DateTime.UtcNow.Hour, 0, 0);
            else
                queryDate = queryDate.Date + new TimeSpan(12, 0, 0);


            return queryDate;
        }

        public string GetDataSource()
        {
            return WeatherProvider.ToString();
        }
    }
}