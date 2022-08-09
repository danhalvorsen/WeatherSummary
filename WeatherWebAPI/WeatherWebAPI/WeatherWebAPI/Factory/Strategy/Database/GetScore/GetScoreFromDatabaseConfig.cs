namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetScoreFromDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
