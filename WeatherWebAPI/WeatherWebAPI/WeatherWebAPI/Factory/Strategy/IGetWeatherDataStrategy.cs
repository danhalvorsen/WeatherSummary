using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public interface IGetWeatherDataStrategy<T> : IHttpStrategy
    {
        public Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate);
        //public List<WeatherForecastDto> GetHistoricData(CityDto city, DateTime from, DateTime to);
    }
}