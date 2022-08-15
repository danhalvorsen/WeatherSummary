using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetScoreFromDatabaseStrategy : IGetScoreFromDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public GetScoreFromDatabaseStrategy(IDatabaseConfig config)
        {
            _config = config;
        }

        public async Task<List<ScoresDto>> Get()
        {
            string queryString = "SELECT * FROM Score";

            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var scoreDtos = new List<ScoresDto>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        scoreDtos.Add(new ScoresDto
                        {
                            WeatherDataId = Convert.ToInt32(reader["FK_WeatherDataId"]),
                            Score = (float)Convert.ToDouble(reader["Score"]),
                            ScoreWeighted = (float)Convert.ToDouble(reader["ScoreWeighted"]),
                        });
                    }
                }
                await command.ExecuteNonQueryAsync();
                return scoreDtos;
            }
        }
    }
}
