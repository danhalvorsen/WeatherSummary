using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory
{
    public interface IStrategy
    {
        public StrategyType StrategyType { get; }
    }

    public interface IGetWeatherDataStrategy : IStrategy
    {
        public Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate);

        public WeatherProvider WeatherProvider { get; }

        public string GetDataSource();
    }
}