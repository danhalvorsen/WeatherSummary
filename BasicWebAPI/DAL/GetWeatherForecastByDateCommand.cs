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
            foreach (var strategy in getWeatherDataStrategies)
            {

                try
                {
                    var getCitiesQuery = new GetCitiesQuery(_config);
                    var cities = await getCitiesQuery.GetAllCities();

                    if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
                    {
                        await InsertCityIntoDatabase(cityName, strategy, _factory); //ToDo: Own command for createCity???
                        var city = cities.Where(c => c.Name.Equals(cityName)).First();
                        await (new AddWeatherDataForFirstTimeSearchedCityCommand(_config)).GetWeatherDataForFirstTimeSearchedCity(city, strategy);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception Message: {e.Message}");
                }
            }

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE CAST([Date] as Date) = '{query.DateQuery.Date}' AND City.Name = '{cityName}'";

            return DatabaseQuery(queryString);
        }

        private async Task InsertCityIntoDatabase(string city, IStrategy strategy, GetWeatherDataFactory factory)
        {
            try
            {

                var result = await factory.GetCityDataFrom(city, strategy);

                // Getting the full country name
                var twoLetterCountryAbbreviation = new CultureInfo(result[0].Country);
                var countryName = new RegionInfo(twoLetterCountryAbbreviation.Name);
                result[0].Country = countryName.EnglishName;

                string queryString = $"INSERT INTO City ([Name], Country, Altitude, Longitude, Latitude) " +
                                        $"VALUES('{result[0].Name}', '{result[0].Country}', {result[0].Altitude}, {result[0].Longitude}, {result[0].Latitude})";

                using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("WeatherForecastDatabase")))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Message: {e.Message}");
            }
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
