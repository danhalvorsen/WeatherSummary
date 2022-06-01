using BasicWebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BasicWebAPI.DAL
{

    public interface IBaseWeatherForecastQuery
    {
        List<WeatherForecastDto> DatabaseQuery(string queryString);
    }

    public abstract class BaseWeatherForecastQuery : IBaseWeatherForecastQuery
    {
        protected readonly IConfiguration _config;

        public BaseWeatherForecastQuery(IConfiguration config)
        {
            _config = config;
        }

        public List<WeatherForecastDto> DatabaseQuery(string queryString)
        {
            try
            {
                var connectionString = this._config.GetConnectionString("WeatherForecastDatabase");
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                Date = Convert.ToDateTime(reader["Date"]), //.ToString("dd-mm-yyyy hh:mm:"),
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
                    return WeatherForecastDtos;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected WeatherForecastDto InsertIntoDatabase(WeatherForecastDto addWeatherData, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("WeatherForecastDatabase")))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
            return addWeatherData;
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
