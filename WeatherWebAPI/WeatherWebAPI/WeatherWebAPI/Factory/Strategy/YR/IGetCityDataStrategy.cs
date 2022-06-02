using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public interface IGetCityDataStrategy<T> : IStrategy<T>
    {
        List<T> GetData(CityDto cityDto);
    }
}