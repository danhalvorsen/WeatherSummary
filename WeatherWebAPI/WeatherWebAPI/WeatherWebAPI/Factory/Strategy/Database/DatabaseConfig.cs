namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
