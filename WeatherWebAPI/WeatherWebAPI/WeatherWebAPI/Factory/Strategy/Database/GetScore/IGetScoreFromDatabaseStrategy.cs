using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetScoreFromDatabaseStrategy
    {
        public Task<List<ScoresDto>> Get();
    }
}
