using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetScoreFromDatabaseStrategy : IGetScoreFromDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public GetScoreFromDatabaseStrategy(IDatabaseConfig config)
        {
            _config = config;
        }

        public StrategyType StrategyType => StrategyType.GetScoreFromDatabase;

        public async Task<List<Scores>> Get()
        {
            string queryString = "SELECT * FROM Score";

            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var scores = new List<Scores>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        scores.Add(new Scores
                        {
                            WeatherDataId = Convert.ToInt32(reader["FK_WeatherDataId"]),
                            Value = (float)Convert.ToDouble(reader["Value"]),
                            ValueWeighted = (float)Convert.ToDouble(reader["ValueWeighted"])
                        });
                    }
                }
                await command.ExecuteNonQueryAsync();
                return scores;
            }
        }
    }
}
