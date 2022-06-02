namespace WeatherWebAPI.Factory.Strategy.YR
{
    public class YrConfig
    {
        public string DataSource { get; }
        public string Uri { get; set; }
        public string GeoUri { get; set; }
        public Uri BaseUrl { get; }
        public Uri? BaseGeoUrl { get; }
        public Uri HomePage { get; set; }

        public YrConfig()
        {
            DataSource = this.GetType().Name;
            Uri = "";
            BaseUrl = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
            HomePage = new Uri("https://www.yr.no/");
            BaseGeoUrl = null;
            GeoUri = "";
        }
    }
}
