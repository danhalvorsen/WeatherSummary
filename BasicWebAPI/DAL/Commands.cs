using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using BasicWebAPI;
using BasicWebAPI.Query;
using BasicWebAPI.Controllers;

namespace BasicWebAPI.DAL
{
    public class Commands
    {
        private const string Cloudy = "Cloudy";
        private const string Sunny = "Sunny";
        private const string Rainy = "Rainy";
        private const string Snowy = "Snowy";
        private const string Stormy = "Stormy";
        private readonly IConfiguration config;

        public Commands(IConfiguration config)
        {
            this.config = config;
        }
        public List<WeatherForecastDto> GetWeatherForecastByDate(DateQueryAndCity query)
        {
            string city = query.CityQuery.City;
            var listWeatherForecast = new List<WeatherForecastDto>();

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                //$"INNER JOIN WeatherType ON FK_WeatherId = WeatherData.Id " +
                                                        $"WHERE CAST([Date] as Date) = '{query.DateQuery.Date}' AND City.Name = '{city}'";

            return DatabaseQuery(listWeatherForecast, queryString);
        }

        public List<WeatherForecastDto> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity query)
        {
            var utcDateFrom = query.BetweenDateQuery.From.Date.ToUniversalTime();
            var utcDateTo = query.BetweenDateQuery.To.Date.ToUniversalTime();
            string city = query.CityQuery.City;
            var listWeatherForecastBetweenDates = new List<WeatherForecastDto>();

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                            //$"INNER JOIN WeatherType ON FK_WeatherId = WeatherData.Id " +
                                                        $"WHERE CAST([Date] as Date) BETWEEN '{utcDateFrom}' AND '{utcDateTo}' AND City.Name = '{city}'";

            return DatabaseQuery(listWeatherForecastBetweenDates, queryString);
        }

        public List<WeatherForecastDto> GetWeatherForecastByWeek(int week, CityQuery query)
        {
            var listWeatherForecastByWeek = new List<WeatherForecastDto>();

            string queryString = $"SET DATEFIRST 1 " +
                                  $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        //$"INNER JOIN WeatherType ON FK_WeatherId = WeatherData.Id " +
                                                        $"WHERE DATEPART(week, [Date]) = {week} AND City.Name = '{query.City}'";

            return DatabaseQuery(listWeatherForecastByWeek, queryString);
        }

        public WeatherForecastDto AddWeatherDataToWeatherDataAndSourceWeatherDataTable(WeatherForecastDto addWeatherData)
        {
            string queryString = $"DECLARE @city_id INT " +
                                    $"DECLARE @source_id INT " +
                                        $"DECLARE @fk_weatherdata_id INT " +
                                            $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                                $"SELECT @city_id = Id FROM City WHERE City.Name = '{addWeatherData.City}' " +
                                                    $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{addWeatherData.Source.DataProvider}' " +
                                                        $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                                            $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId) " +
                                                                $"VALUES('{addWeatherData.Date.ToUniversalTime()}', {addWeatherData.WeatherType}, {addWeatherData.Temperature}, {addWeatherData.Windspeed}, " +
                                                                    $"{addWeatherData.WindDirection}, {addWeatherData.WindspeedGust}, {addWeatherData.Pressure}, {addWeatherData.Humidity}, {addWeatherData.ProbOfRain}, " +
                                                                        $"{addWeatherData.AmountRain}, {addWeatherData.CloudAreaFraction}, {addWeatherData.FogAreaFraction}, {addWeatherData.ProbOfThunder}, @city_id)" +

                                                    $"SELECT @fk_weatherdata_id = Id From WeatherData" +
                                                        $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId)" +
                                                            $"VALUES({addWeatherData.Date.ToUniversalTime()}, @source_id, @fk_weatherdata_id)";


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
            var connectionString = this.config.GetConnectionString("WeatherForecastDatabase");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        var weatherSource = new WeatherSourceDto();
                            weatherSource.DataProvider = reader["SourceName"].ToString();
                        

                        //var cloudy = (bool)reader[Cloudy];
                        //var sunny = (bool)reader[Sunny];
                        //var rainy = (bool)reader[Rainy];
                        //var snowy = (bool)reader[Snowy];
                        //var stormy = (bool)reader[Stormy];

                        //var weatherTypes = new List<WeatherTypeDto>();
                        
                        //if (cloudy)
                        //    InsertIntoTempWeatherTypeList(weatherTypes, Cloudy);
                        //if (sunny)
                        //    InsertIntoTempWeatherTypeList(weatherTypes, Sunny);
                        //if (rainy)
                        //    InsertIntoTempWeatherTypeList(weatherTypes, Rainy);
                        //if (snowy)
                        //    InsertIntoTempWeatherTypeList(weatherTypes, Snowy);
                        //if (stormy)
                        //    InsertIntoTempWeatherTypeList(weatherTypes, Stormy);

                        list.Add(new WeatherForecastDto
                        {
                            //Id = Convert.ToInt32(reader["Id"]),
                            //FK_CityId = Convert.ToInt32(reader["FK_CityId"]),
                           
                            City = reader["CityName"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"]),
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
                            //WeatherTypes = weatherTypes
                            Source = weatherSource,
                        });
                    }
                }
                return list;
            }
        }

        //private static void InsertIntoTempWeatherTypeList(List<WeatherTypeDto> weatherType, string type) //// Ikke i bruk dersom WeatherTypeTable blir tatt bort
        //{
        //    weatherType.Add(new WeatherTypeDto
        //    {
        //        Type = type
        //    });
        //}
    }
}
