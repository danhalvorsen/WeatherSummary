﻿using System.Data.SqlClient;
using System.Diagnostics;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class GetWeatherDataFromDatabaseStrategy : IGetWeatherDataFromDatabaseStrategy
    {
        private readonly IConfiguration _config;

        public GetWeatherDataFromDatabaseStrategy(IConfiguration config)
        {
            _config = config;
        }

        public StrategyType StrategyType => StrategyType.GetWeatherDataFromDatabase;

        public async Task<List<WeatherForecast.WeatherData>> Get(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("WeatherForecastDatabase")))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var WeatherForecasts = new List<WeatherForecast.WeatherData>();

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

                        var score = new Scores
                        {
                            WeatherDataId = reader["FK_WeatherDataId"] != System.DBNull.Value ? Convert.ToInt32(reader["FK_WeatherDataId"]) : 0,
                            Value = reader["Value"] != System.DBNull.Value ? (float)Math.Round(Convert.ToDouble(reader["Value"]), 2) : 0,
                            ValueWeighted = reader["ValueWeighted"] != System.DBNull.Value ? (float)Math.Round(Convert.ToDouble(reader["ValueWeighted"]), 2) : 0
                        };

                        WeatherForecasts.Add(new WeatherForecast.WeatherData
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
                return WeatherForecasts;
            }
        }
    }
}
