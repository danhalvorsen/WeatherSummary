using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.DAL.Query
{
    public interface IGetWeatherDataForRatingQuery
    {
        Task<WeatherTuple> Get(CityDto city);
    }

}
