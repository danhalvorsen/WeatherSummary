using System.Net.Http.Headers;
using System.Text.Json;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.YR;

namespace WeatherWebAPI.Factory
{

    public interface IStrategy<T> 
    {
        List<T> GetDataAsync();
    }

 
    public interface IHttpStrategy<T> : IStrategy<T>
    {
        //string DataSource { get; } // ?

        
        //string MakeGeoUriCityCall(string city);
        //string MakeUriWeatherCall(double lat, double lon);
        //HttpClient MakeHttpClientConnection();
        //HttpClient MakeGeoHttpClientConnection();
    }

    public interface IWeatherDataStrategy<T> : IHttpStrategy<T>
    {
        public T GetWeatherDataAsync(CityDto city);
        public List<T> GetHistoricData(CityDto city, DateTime from, DateTime to);
    }

    public class YrStrategy : IWeatherDataStrategy<WeatherForecastDto>
    {
        private readonly YrAutomapperConfig yrAutomapperConfig;
        private readonly YrConfig yrConfig;

        public YrStrategy(YrAutomapperConfig yrAutomapperConfig, YrConfig yrConfig )
        {
            this.yrAutomapperConfig = yrAutomapperConfig;
            this.yrConfig = yrConfig;
            
        }
 
        //public string MakeGeoUriCityCall(string city)
        //{
        //    return GeoUri = "";
        //}

        //private Uri MakeUriWeatherCall(double lat, double lon)
        //{
        //    return new Uri();
        //}

        public List<WeatherForecastDto> GetWeatherDataAsync(CityDto cityDto)
        {
            throw new NotImplementedException();
        }

        public List<WeatherForecastDto> GetHistoricData(CityDto cityDto, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WeatherForecastDto>> GetDataAsync()
        {
            //HttpClient httpClient = new HttpClient();

            //httpClient.BaseAddress = new Uri(yrConfig.BaseUrl + $"complete?lat={city.Latitude}&lon={city.Longitude}");
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var res = await httpClient.SendAsync(new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get
            //});

            //var result = await res.
            throw new NotImplementedException();
        }

        public async Task<WeatherForecastDto> GetWeatherData(CityDto city)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(yrConfig.BaseUrl + $"complete?lat={city.Latitude}&lon={city.Longitude}");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            //var response = await httpClient.GetAsync(strategy.MakeUriWeatherCall(lat, lon));
            var response = await httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get
            });

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

                return this.automapperConfig.CreateMapper().Map<WeatherForecastDto>(weatherData);

            }

            return new WeatherForecastDto();
        }

        public List<T> GetData()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseGeoUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }

        public List<T> GetHistoryData(CityDto cityDto, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        WeatherForecastDto IWeatherDataStrategy<WeatherForecastDto>.GetWeatherDataAsync(CityDto cityDto)
        {
            throw new NotImplementedException();
        }

        List<WeatherForecastDto> IStrategy<WeatherForecastDto>.GetDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}