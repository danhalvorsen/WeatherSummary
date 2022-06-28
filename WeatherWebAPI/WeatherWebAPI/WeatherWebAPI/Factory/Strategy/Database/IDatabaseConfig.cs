namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IDatabaseConfig
    {
        string? ConnectionString { get; set; }
    }
}
