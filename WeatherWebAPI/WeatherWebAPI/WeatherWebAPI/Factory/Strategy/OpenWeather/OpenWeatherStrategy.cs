using System.Net.Http.Headers;

namespace WeatherWebAPI.Factory
{
    public class OpenWeatherStrategy : IWeatherDataStrategy
    {
        public OpenWeatherStrategy()
        {
            DataSource = this.GetType().Name;
            Uri = "";
            BaseUrl = new Uri("http://api.openweathermap.org/data/2.5/");
            HomePage = new Uri("https://openweathermap.org/");
            BaseGeoUrl = new Uri("http://api.openweathermap.org/geo/1.0/");
            GeoUri = "";
        }

        public string DataSource { get; }
        public string Uri { get; set; }
        public string GeoUri { get; set; }
        public Uri BaseUrl { get; }
        public Uri? BaseGeoUrl { get; }
        public Uri HomePage { get; set; }

        public string MakeGeoUriCityCall(string city)
        {
            return GeoUri = $"direct?q={city}&appid=7397652ad9c5f55e36782bb22811ca43";
        }

        public string MakeUriWeatherCall(double lat, double lon)
        {
            return Uri = $"onecall?lat={lat}&lon={lon}&units=metric&appid=7397652ad9c5f55e36782bb22811ca43";
        }

        public HttpClient MakeHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }

        public HttpClient MakeGeoHttpClientConnection()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = BaseGeoUrl;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/6.0 (Windows 10, Win64; x64; rv:100.0) Gecko/20100101 FireFox/100.0");

            return httpClient;
        }
    }
}