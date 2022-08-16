using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public interface IGetWeatherDataStrategy<T>
    {
        public Task<WeatherForecast> GetWeatherDataFrom(CityDto city, DateTime queryDate);
        //public List<WeatherForecast> GetHistoricData(CityDto city, DateTime from, DateTime to);
        public string GetDataSource();
    }
}