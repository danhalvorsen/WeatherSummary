namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddWeatherDataToDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
