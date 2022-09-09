using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.DAL
{
    public interface IBackgroundServiceFetchWeatherDataQuery
    {
        Task<List<WeatherForecast.WeatherData>> GetOneWeekWeatherForecastForAllCitiesFor(IGetWeatherDataStrategy strategy, List<CityDto> cities);
    }
}