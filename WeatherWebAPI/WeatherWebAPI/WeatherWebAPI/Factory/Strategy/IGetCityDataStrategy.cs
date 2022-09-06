using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy
{
    public interface IGetCityDataStrategy : IStrategy
    {
        public Task<List<CityDto>> GetCityDataFor(string city);
    }
}
