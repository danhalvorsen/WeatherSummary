using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetScoreFromDatabaseStrategy
    {
        public Task<List<Scores>> Get();
    }
}
