namespace WeatherWebAPI.DAL
{
    public interface IBackgroundServiceGetWeatherDataCommand
    {
        Task GetOneWeekWeatherForecastForAllCities();
    }
}