namespace WeatherWebAPI.Factory
{
    public interface IConfig
    {
        public string? DataSource { get; }
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }
    }
}