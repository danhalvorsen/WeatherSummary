using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.Database;

namespace WeatherWebAPI.Query
{
    public interface IGetCitiesQuery
    {
        Task<List<CityDto>> GetAllCities();
    }
}
