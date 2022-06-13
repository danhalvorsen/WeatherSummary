namespace WeatherWebAPI.Factory.Strategy.Database.GetWeather
{
    public interface IGetWeatherDataFromDatabaseStrategy
    {
        public List<WeatherForecastDto> Query(string queryString);
    }
}
