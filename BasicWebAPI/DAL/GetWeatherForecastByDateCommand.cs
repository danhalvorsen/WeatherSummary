using BasicWebAPI.Controllers;
using BasicWebAPI.Factory;
using BasicWebAPI.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net;

namespace BasicWebAPI.DAL
{
    public class GetWeatherForecastByDateCommand : BaseWeatherForecastQuery
    {

        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastByDateCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IStrategy> getWeatherDataStrategies)
        {
            string cityName = query.CityQuery.City;
            DateTime date = query.DateQuery.Date;

            try
            {
                var getCitiesQuery = new GetCitiesQuery(_config);
                var cities = await getCitiesQuery.GetAllCities();

                if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
                {
                    await (new CreateCityCommand(_config).InsertCityIntoDatabase(cityName, new OpenWeatherStrategy(), _factory));

                    var getCitiesQueryUpdate = new GetCitiesQuery(_config);
                    var citiesUpdated = await getCitiesQueryUpdate.GetAllCities();

                    foreach (var strategy in getWeatherDataStrategies)
                    {
                        var city = citiesUpdated.Where(c => c.Name.Equals(cityName)).First();

                        await (new AddWeatherDataForCityCommand(_config).GetWeatherDataForCity(city, strategy));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Message: {e.Message}");
            }

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE CAST([Date] as Date) = '{date}' AND City.Name = '{cityName}'";
            

            return DatabaseQuery(queryString);
        }

        //private CityDto GetCityRequestInfo(string city)
        //{
        //    var getCitiesQuery = new GetCitiesQuery(_config);
        //    var cities = getCitiesQuery.GetAllCities();

        //    foreach (var c in cities)
        //    {
        //        if (c.Name == city)
        //            return c;
        //    }
        //    return null;
        //}

        //private bool CityExist(string city)
        //{
        //    var citycommands = new CityCommands(_config);
        //    var cities = citycommands.GetCities();

        //    foreach (var c in cities)
        //    {
        //        if (c.Name == city)
        //            return true;
        //    }
        //    return false;
        //}
    }
}
