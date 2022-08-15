﻿using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{


    public class GetWeatherForecastByDateCommand : BaseCommands
    {
        public GetWeatherForecastByDateCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? citySearchedFor = query?.CityQuery?.City;
            string? cityName = "";
            DateTime date = query!.DateQuery!.Date.ToUniversalTime();

            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            //var getDatesQueryDatabase = new GetDatesForCityQuery(_config);

            try
            {
 
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;

                    if(cityName != "")
                    {
                        if (!CityExists(cityName!))
                        {
                            await AddCityToDatabase(cityData);
                            _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();
                        }
                    }
                    return new List<WeatherForecastDto>();
                }
                else
                {
                    cityName = citySearchedFor;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return await GetWeatherForecastFromDatabase(cityName, date, weatherDataStrategies);
        }

        private async Task<List<WeatherForecastDto>> GetWeatherForecastFromDatabase(string? cityName, DateTime date, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            DateTime now;
            
            if (DateTime.UtcNow.Date > date.Date)
                now = date;
            else
                now = DateTime.UtcNow;
                

            string queryString = $"SELECT TOP {weatherDataStrategies.Count} WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                            $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                $"WHERE CAST([DateForecast] as Date) = '{date}' AND CAST([Date] as date) = '{now}'  AND City.Name = '{cityName}' " +
                                    $"ORDER BY DateForecast";

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return await getWeatherDataFromDatabaseStrategy.Get(queryString);
        }
    }

}
