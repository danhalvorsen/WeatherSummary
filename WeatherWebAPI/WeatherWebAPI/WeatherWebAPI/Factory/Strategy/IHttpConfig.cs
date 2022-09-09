namespace WeatherWebAPI.Factory
{
    public interface IHttpConfig
    {
        public Uri? BaseUrl { get; }
        public Uri? HomePage { get; set; }
    }
}