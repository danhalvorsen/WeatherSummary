using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public interface ICityQuery
    {
        Task<List<CityDto>> GetAllCities();
    }
}
