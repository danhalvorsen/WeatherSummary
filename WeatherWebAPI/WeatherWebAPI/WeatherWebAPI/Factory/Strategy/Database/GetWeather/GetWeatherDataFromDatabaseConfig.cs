namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetWeatherDataFromDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
