namespace WeatherWebAPI.Query
{
    public interface IDatabaseQuery
    {
        List<WeatherForecastDto> Query(string queryString);
    }
}
