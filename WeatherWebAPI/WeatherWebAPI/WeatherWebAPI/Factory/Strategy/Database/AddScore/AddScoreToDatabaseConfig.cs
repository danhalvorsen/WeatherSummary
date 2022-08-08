namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddScoreToDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
