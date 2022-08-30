﻿using System.Globalization;
using WeatherWebAPI.Arguments;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherForecastPredictionByDateQuery : BaseFunctionsForQueriesAndCommands
    {
        public GetWeatherForecastPredictionByDateQuery(CommonArgs commonArgs) : base(commonArgs)
        {

        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastPredictionByDateForOneWeek(DateQueryAndCity query)
        {
            string? citySearchedFor = query?.CityQuery?.City;
            string? cityName;
            DateTime date = query!.DateQuery!.Date.ToUniversalTime();

            var dtoList = new List<WeatherForecastDto>();
            var getCitiesQueryDatabase = new GetCitiesQuery(_commonArgs.Config);

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
                }
                else
                {
                    cityName = citySearchedFor;
                }

                string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                                            $"WHERE CAST([Date] as Date) = '{date}' AND City.Name = '{cityName}' " +
                                                                $"ORDER BY DateForecast";

                await MakeWeatherForecastDto(_commonArgs.Mapper, dtoList, queryString);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dtoList;
        }

    }
}
