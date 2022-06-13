namespace WeatherWebAPI.Factory.Strategy.Database.GetWeather
{
    public class GetWeatherDataFromDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
