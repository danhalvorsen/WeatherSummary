using System.Data.SqlClient;
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

        public List<WeatherForecastDto> Query(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var WeatherForecastDtos = new List<WeatherForecastDto>();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    foreach (object o in reader)
                    {
                        var weatherSource = new WeatherSourceDto();
                        weatherSource.DataProvider = reader["SourceName"].ToString();

                        WeatherForecastDtos.Add(new WeatherForecastDto
                        {
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
                            Source = weatherSource,
                        });
                    }
                }
                command.ExecuteNonQuery();
                return WeatherForecastDtos;
            }
        }
    }
}
