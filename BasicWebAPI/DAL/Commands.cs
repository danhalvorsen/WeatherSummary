using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using BasicWebAPI;
using BasicWebAPI.Query;
using BasicWebAPI.Controllers;
using BasicWebAPI.Factory;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BasicWebAPI.DAL
{
    public class Commands : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;

        public Commands(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public List<WeatherForecastDto> GetWeatherForecastByWeek(int week, CityQuery query)
        {

            string queryString = $"SET DATEFIRST 1 " +
                                  $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE DATEPART(week, [Date]) = {week} AND City.Name = '{query.City}'";

            return DatabaseQuery(queryString);
        }

        // Post endpoint -> obsolete 
        //public WeatherForecastDto AddWeatherDataToDatabase(WeatherForecastDto addWeatherData)
        //{
        //    string queryString = $"DECLARE @city_id INT " +
        //                            $"DECLARE @source_id INT " +
        //                                $"DECLARE @fk_weatherdata_id INT " +
        //                                    $"SELECT @fk_weatherdata_id = Id From WeatherData " +
        //                                        $"SELECT @city_id = Id FROM City WHERE City.Name = '{addWeatherData.City}' " +
        //                                            $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{addWeatherData.Source.DataProvider}' " +
        //                                                $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
        //                                                    $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId) " +
        //                                                        $"VALUES('{addWeatherData.Date}', '{addWeatherData.WeatherType}', {addWeatherData.Temperature}, {addWeatherData.Windspeed}, " +
        //                                                            $"{addWeatherData.WindDirection}, {addWeatherData.WindspeedGust}, {addWeatherData.Pressure}, {addWeatherData.Humidity}, {addWeatherData.ProbOfRain}, " +
        //                                                                $"{addWeatherData.AmountRain}, {addWeatherData.CloudAreaFraction}, {addWeatherData.FogAreaFraction}, {addWeatherData.ProbOfThunder}, @city_id)" +

        //                                            $"SELECT @fk_weatherdata_id = Id From WeatherData " +
        //                                                $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId) " +
        //                                                    $"VALUES('{addWeatherData.Date}', @source_id, @fk_weatherdata_id)";

        //    return InsertIntoDatabase(addWeatherData, queryString);
        //}
    }
}
