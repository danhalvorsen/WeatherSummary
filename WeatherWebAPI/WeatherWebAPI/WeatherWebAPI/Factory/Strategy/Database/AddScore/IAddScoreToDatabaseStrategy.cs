namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IAddScoreToDatabaseStrategy
    {
        Task Add(double score, double weightedScore, int weatherDataId);
    }
}
