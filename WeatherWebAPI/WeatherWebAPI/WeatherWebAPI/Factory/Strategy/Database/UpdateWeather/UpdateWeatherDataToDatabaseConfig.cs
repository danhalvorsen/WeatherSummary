namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class UpdateWeatherDataToDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
