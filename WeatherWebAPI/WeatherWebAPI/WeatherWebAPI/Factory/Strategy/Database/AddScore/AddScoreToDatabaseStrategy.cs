using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddScoreToDatabaseStrategy : IAddScoreToDatabaseStrategy
    {
        private readonly IConfiguration _config;

        public AddScoreToDatabaseStrategy(IConfiguration config)
        {
            _config = config;
        }

        public StrategyType StrategyType => StrategyType.AddScoreToDatabase;

        public async Task Add(List<Scores> scores)
        {
            try
            {
                foreach (var score in scores)
                {
                    string queryString = $"INSERT INTO Score(Value, ValueWeighted, FK_WeatherDataId) " +
                            $"VALUES({score.Value}, {score.ValueWeighted}, {score.WeatherDataId})";

                    using SqlConnection connection = new(_config.GetConnectionString("WeatherForecastDatabase"));

                    SqlCommand command = new(queryString, connection);
                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
