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

        public List<ScoreDto> Get(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var scoreDtos = new List<ScoreDto>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        scoreDtos.Add(new ScoreDto
                        {
                            FK_WeatherDataId = Convert.ToInt32(reader["FK_WeatherDataId"]),
                            Score = (float)Convert.ToDouble(reader["Score"]),
                            ScoreWeighted = (float)Convert.ToDouble(reader["ScoreWeighted"]),
                        });
                    }
                }
                command.ExecuteNonQuery();
                return scoreDtos;
            }
        }
    }
}
