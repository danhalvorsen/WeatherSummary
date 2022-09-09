using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.DAL
{
    public interface IBackgroundServiceCalculateScoreQuery
    {
        Task<List<Scores>> CalculateScore();
    }
}