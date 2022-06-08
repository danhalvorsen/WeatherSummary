using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public interface IGetCityDataStrategy<T>
    {
        public Task<List<CityDto>> GetCityDataFor(string city);
    }
}