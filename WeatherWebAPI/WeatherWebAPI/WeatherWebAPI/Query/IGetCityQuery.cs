using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public interface IGetCityQuery
    {
        Task<List<CityDto>> GetAllCities();
    }
}
