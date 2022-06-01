using BasicWebAPI.Controllers;
using BasicWebAPI.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BasicWebAPI.DAL
{
    public class AddWeatherDataForCityCommand : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;
        public AddWeatherDataForCityCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }


        public async Task GetWeatherDataForCity(CityDto city, IStrategy strategy) // Adding without date -> backgroundworker uses this
        {
            Debug.Assert(city != null, "city is null");
            Debug.Assert(strategy != null, "strategy is null");

            try
            {
                var weatherForecastData = await _factory.GetWeatherDataFrom(city.Latitude, city.Longitude, strategy);
                AddWeatherDataToDatabaseForCityQuery(weatherForecastData, city);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task GetWeatherDataForCity(DateTime DateQuery, CityDto city, IStrategy strategy) // Adding based on date
        {
            Debug.Assert(city != null, "city is null");
            Debug.Assert(strategy != null, "strategy is null");

            try
            {
                var weatherForecastData = await _factory.GetWeatherDataFrom(DateQuery, city.Latitude, city.Longitude, strategy);
                AddWeatherDataToDatabaseForCityQuery(weatherForecastData, city);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateWeatherDataForCity(DateTime DateQuery, CityDto city, IStrategy strategy)
        {
            Debug.Assert(city != null, "city is null");
            Debug.Assert(strategy != null, "strategy is null");

            try
            {
                var weatherForecastData = await _factory.GetWeatherDataFrom(DateQuery, city.Latitude, city.Longitude, strategy);
                UpdateWeatherDataToDatabaseForCityQuery(weatherForecastData, city, DateQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private WeatherForecastDto AddWeatherDataToDatabaseForCityQuery(WeatherForecastDto addWeatherData, CityDto cityData)
        {
            string queryString = $"DECLARE @city_id INT " +
                                    $"DECLARE @source_id INT " +
                                        $"DECLARE @fk_weatherdata_id INT " +
                                            $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                                $"SELECT @city_id = Id FROM City WHERE City.Name = '{cityData.Name}' " +
                                                    $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{addWeatherData.Source.DataProvider}' " +
                                                        $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                                            $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId) " +
                                    $"VALUES('{addWeatherData.Date}', '{addWeatherData.WeatherType}', {addWeatherData.Temperature}, {addWeatherData.Windspeed}, " +
                                        $"{addWeatherData.WindDirection}, {addWeatherData.WindspeedGust}, {addWeatherData.Pressure}, {addWeatherData.Humidity}, {addWeatherData.ProbOfRain}, " +
                                            $"{addWeatherData.AmountRain}, {addWeatherData.CloudAreaFraction}, {addWeatherData.FogAreaFraction}, {addWeatherData.ProbOfThunder}, @city_id)" +

                                    $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                        $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId) " +
                                            $"VALUES('{addWeatherData.Date}', @source_id, @fk_weatherdata_id)";

            return InsertIntoDatabase(addWeatherData, queryString);
        }

        private WeatherForecastDto UpdateWeatherDataToDatabaseForCityQuery(WeatherForecastDto addWeatherData, CityDto cityData, DateTime dateToBeUpdated)
        {
            string queryString = $"UPDATE WeatherData " +
                                    $"SET [Date] = '{addWeatherData.Date}', " +
                                    $"WeatherType = '{addWeatherData.WeatherType}', " +
                                    $"Temperature = {addWeatherData.Temperature}, " +
                                    $"Windspeed = {addWeatherData.Windspeed}, " +
                                    $"WindDirection = {addWeatherData.WindDirection}, " +
                                    $"WindspeedGust = {addWeatherData.WindspeedGust}, " +
                                    $"Pressure = {addWeatherData.Pressure}, " +
                                    $"Humidity = {addWeatherData.Humidity}, " +
                                    $"ProbOfRain = {addWeatherData.ProbOfRain}, " +
                                    $"AmountRain = {addWeatherData.AmountRain}, " +
                                    $"CloudAreaFraction = {addWeatherData.CloudAreaFraction}, " +
                                    $"FogAreaFraction = {addWeatherData.FogAreaFraction}, " +
                                    $"ProbOfThunder = {addWeatherData.ProbOfThunder} " +
                                        $"FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"WHERE CAST([Date] as Date) = '{dateToBeUpdated.Date}' AND City.Name = '{cityData.Name}'";

            return InsertIntoDatabase(addWeatherData, queryString);
        }
    }
}
