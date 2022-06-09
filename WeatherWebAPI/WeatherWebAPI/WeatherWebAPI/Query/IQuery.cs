using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public interface IQuery
    {
        Task<List<CityDto>> GetAllCities();
    }
}
