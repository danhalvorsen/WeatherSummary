using BasicWebAPI.Factory;
using BasicWebAPI.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebAPI.DAL
{
    public class GetWeatherForecastBetweenDatesCommand : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastBetweenDatesCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDatesAsync(BetweenDateQueryAndCity betweenDateQueryAndCity, List<IStrategy> getWeatherDataStrategies)
        {
            string cityName = betweenDateQueryAndCity.CityQuery.City;
            DateTime FromDate = betweenDateQueryAndCity.BetweenDateQuery.From;
            DateTime ToDate = betweenDateQueryAndCity.BetweenDateQuery.To;

            try
            {
                // Itterating through all the cities in the database
                var getCitiesQuery = new GetCitiesQuery(_config);
                var cities = await getCitiesQuery.GetAllCities();

                // Itterating through all the dates in the database
                var getDatesQuery = new GetDatesQuery(_config);
                var dates = await getDatesQuery.GetAllDates();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

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

                if (!dates.ToList().Any(d => d.Date.Date.Equals(date.Date)))
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
                string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                        $"WHERE CAST([Date] as Date) BETWEEN '{FromDate}' AND '{ToDate}' AND City.Name = '{cityName}'";

            return DatabaseQuery(queryString);
        }
    }
}
