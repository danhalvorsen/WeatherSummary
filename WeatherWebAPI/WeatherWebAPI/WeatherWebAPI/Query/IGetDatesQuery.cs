using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public interface IGetDatesQuery
    {
        Task<List<WeatherForecastDto>> GetDatesForCity(string cityName);
    }
}
