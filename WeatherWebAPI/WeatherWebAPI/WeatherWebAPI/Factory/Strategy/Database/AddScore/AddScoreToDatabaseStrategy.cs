using System.Data.SqlClient;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddScoreToDatabaseStrategy : IAddScoreToDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public AddScoreToDatabaseStrategy(IDatabaseConfig config)
        {
            _config = config;
        }

        public async Task Add(double score, double weightedScore, int weatherDataId)
        {
            string queryString = $"INSERT INTO Score(Score, ScoreWeighted, FK_WeatherDataId) " +
                                    $"VALUES({score}, {weightedScore}, {weatherDataId}";

            using SqlConnection connection = new(_config.ConnectionString);
            SqlCommand command = new(queryString, connection);
            connection.Open();

            await command.ExecuteNonQueryAsync();
        }
    }
}
