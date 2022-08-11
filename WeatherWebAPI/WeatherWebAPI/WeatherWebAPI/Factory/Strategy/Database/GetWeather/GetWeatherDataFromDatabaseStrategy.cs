using System.Data.SqlClient;
using System.Diagnostics;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetWeatherDataFromDatabaseStrategy : IGetWeatherDataFromDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public GetWeatherDataFromDatabaseStrategy(IDatabaseConfig config)
        {
            this._config = config;
        }

        public async Task<List<WeatherForecastDto>> Get(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var weatherForecastDtos = new List<WeatherForecastDto>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //Debug.Assert(reader != null, "Reader should not be null");
                    //Debug.Assert(reader.HasRows != false, "Reader should have rows");

                    foreach (object o in reader)
                    {
                        var weatherSource = new WeatherSourceDto
                        {
                            DataProvider = reader["SourceName"].ToString()
                        };

                        var score = new ScoreDto
                        {
                            FK_WeatherDataId = reader["FK_WeatherDataId"] != System.DBNull.Value ? Convert.ToInt32(reader["FK_WeatherDataId"]) : 0,
                            Score = reader["Score"] != System.DBNull.Value ? Math.Round((float)Convert.ToDouble(reader["Score"]), 2) : 0,
                            ScoreWeighted = reader["ScoreWeighted"] != System.DBNull.Value ? Math.Round((float)Convert.ToDouble(reader["ScoreWeighted"]), 2) : 0
                        };

                        weatherForecastDtos.Add(new WeatherForecastDto
                        {
                            WeatherForecastId = Convert.ToInt32(reader["Id"]),
                            City = reader["CityName"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"]).ToUniversalTime(),
                            WeatherType = reader["WeatherType"].ToString(),
                            Temperature = (float)Convert.ToDouble(reader["Temperature"]),
                            Windspeed = (float)Convert.ToDouble(reader["Windspeed"]),
                            WindDirection = (float)Convert.ToDouble(reader["WindDirection"]),
                            WindspeedGust = (float)Convert.ToDouble(reader["WindspeedGust"]),
                            Pressure = (float)Convert.ToDouble(reader["Pressure"]),
                            Humidity = (float)Convert.ToDouble(reader["Humidity"]),
                            ProbOfRain = (float)Convert.ToDouble(reader["ProbOfRain"]),
                            AmountRain = (float)Convert.ToDouble(reader["AmountRain"]),
                            CloudAreaFraction = (float)Convert.ToDouble(reader["CloudAreaFraction"]),
                            FogAreaFraction = (float)Convert.ToDouble(reader["FogAreaFraction"]),
                            ProbOfThunder = (float)Convert.ToDouble(reader["ProbOfThunder"]),
                            DateForecast = Convert.ToDateTime(reader["DateForecast"]).ToUniversalTime(),
                            Source = weatherSource,
                            Score = score
                        });
                    }
                }
                await command.ExecuteNonQueryAsync();
                return weatherForecastDtos;
            }
        }
    }
}
