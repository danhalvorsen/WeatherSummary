using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IGetScoreFromDatabaseStrategy
    {
        public List<ScoreDto> Get(string queryString);
    }
}
