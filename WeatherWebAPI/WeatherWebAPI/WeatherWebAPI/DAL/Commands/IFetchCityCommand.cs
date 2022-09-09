using WeatherWebAPI.Controllers;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Commands
{
    public interface IFetchCityCommand
    {
        Task<List<CityDto>> FetchCity(CityQuery query);
    }
}