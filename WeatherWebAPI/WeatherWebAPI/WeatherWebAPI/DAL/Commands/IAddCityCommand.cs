using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Commands
{
    public interface IAddCityCommand
    {
        Task AddCity(CityQuery query);
    }
}