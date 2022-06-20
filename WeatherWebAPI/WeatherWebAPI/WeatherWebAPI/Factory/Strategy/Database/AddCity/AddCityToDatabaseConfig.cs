namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddCityToDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
