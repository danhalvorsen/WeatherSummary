namespace WeatherWebAPI.Factory
{
    public interface IHttpConfig
    {
        public string? DataSource { get; }
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }
    }
}