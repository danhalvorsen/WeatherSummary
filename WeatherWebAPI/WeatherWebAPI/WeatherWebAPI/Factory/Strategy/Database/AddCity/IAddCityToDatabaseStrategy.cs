using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IAddCityToDatabaseStrategy
    {
        Task Add(List<CityDto> city);
    }
}
