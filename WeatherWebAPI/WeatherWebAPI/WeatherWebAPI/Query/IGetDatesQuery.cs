using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.Query
{
    public interface IGetDatesForCityQuery
    {
        Task<List<WeatherForecastDto>> GetDatesForCity(string cityName, IGetWeatherDataStrategy<WeatherForecastDto> strategy);
    }
}
