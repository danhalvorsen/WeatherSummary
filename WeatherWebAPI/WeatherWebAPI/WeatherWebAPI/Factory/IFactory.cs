namespace WeatherWebAPI.Factory
{
    public interface IFactory
    {
        IWeatherDataStrategy<WeatherForecastDto> BuildYrStrategy();
    }
}