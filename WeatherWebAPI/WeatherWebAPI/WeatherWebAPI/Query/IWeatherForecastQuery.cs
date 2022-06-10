namespace WeatherWebAPI.Query
{
    public interface IWeatherForecastQuery
    {
        List<WeatherForecastDto> Query(string queryString);
    }
}
