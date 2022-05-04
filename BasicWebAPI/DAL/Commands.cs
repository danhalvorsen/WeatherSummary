using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using BasicWebAPI;
using BasicWebAPI.Query;

namespace BasicWebAPI.DAL
{
    public class Commands
    {
        private readonly IConfiguration config;

        public Commands(IConfiguration config)
        {
            this.config = config;
        }
        public List<WeatherForecastDto> GetWeatherForecastByDate(DateQueryAndCity query)
        {
            var utcDate = query.DateQuery.Date.ToUniversalTime();
            string city = query.CityQuery.City;
            var listWeatherForecast = new List<WeatherForecastDto>();


            string queryString = $"SELECT * FROM WeatherData " +
                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                        $"WHERE CAST([Date] as Date) = '{utcDate}' AND Name = '{city}'";

            return DatabaseQuery(listWeatherForecast, queryString);
        }

        public List<WeatherForecastDto> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity query)
        {
            var utcDateFrom = query.BetweenDateQuery.From.Date.ToUniversalTime();
            var utcDateTo = query.BetweenDateQuery.To.Date.ToUniversalTime();
            string city = query.CityQuery.City;
            var listWeatherForecastBetweenDates = new List<WeatherForecastDto>();

            string queryString = $"SELECT * FROM WeatherData " +
                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                        $"WHERE CAST([Date] as Date) BETWEEN '{utcDateFrom}' AND '{utcDateTo}' AND Name = '{city}'";

            return DatabaseQuery(listWeatherForecastBetweenDates, queryString);
        }

        public List<WeatherForecastDto> GetWeatherForecastByWeek(int week, CityQuery query)
        {
            var listWeatherForecastByWeek = new List<WeatherForecastDto>();

            string queryString = $"SET DATEFIRST 1 " +
                                    $"SELECT * FROM WeatherData " +
                                        $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                            $"WHERE DATEPART(week, [Date]) = {week} AND Name = '{query.City}'";

            return DatabaseQuery(listWeatherForecastByWeek, queryString);
        }

        public WeatherForecastDto AddWeatherDataToWeatherDataTable(WeatherForecastDto addWeatherData)
        {
            addWeatherData = new WeatherForecastDto();

            string queryString = $"DECLARE @city_id INT " +
                                    $"SELECT @city_id = Id FROM City WHERE City.Name = '{addWeatherData.City}' " +
                                        $"INSERT INTO WeatherData([Date], Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                            $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId)" +
                                                $"VALUES({addWeatherData.Date}, {addWeatherData.Temperature}, {addWeatherData.Windspeed}, {addWeatherData.WindDirection}, {addWeatherData.WindspeedGust}, " +
                                                    $"{addWeatherData.Pressure}, {addWeatherData.Humidity}, {addWeatherData.ProbOfRain}, {addWeatherData.AmountRain}, {addWeatherData.CloudAreaFraction}, " +
                                                        $"{addWeatherData.FogAreaFraction}, {addWeatherData.ProbOfThunder}, @city_id)";

            using (SqlConnection connection = new SqlConnection(config.GetConnectionString("WeatherForecastDatabase")))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
            return addWeatherData;
        }

        private List<WeatherForecastDto> DatabaseQuery(List<WeatherForecastDto> list, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(config.GetConnectionString("WeatherForecastDatabase")))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        Console.WriteLine(reader);
                        list.Add(new WeatherForecastDto
                        {
                            //Id = Convert.ToInt32(reader["Id"]),
                            //FK_CityId = Convert.ToInt32(reader["FK_CityId"]),
                            City = reader["Name"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"]),
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
                        });
                    }
                }
                return list;
            }
        }
    }
}
