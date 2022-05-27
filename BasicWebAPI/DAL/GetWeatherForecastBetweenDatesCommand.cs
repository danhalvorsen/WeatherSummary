using BasicWebAPI.Factory;
using BasicWebAPI.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BasicWebAPI.DAL
{
    public class GetWeatherForecastBetweenDatesCommand : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastBetweenDatesCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public List<WeatherForecastDto> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity)
        {
            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                        $"WHERE CAST([Date] as Date) BETWEEN '{betweenDateQueryAndCity.BetweenDateQuery.From}' " +
                                            $"AND '{betweenDateQueryAndCity.BetweenDateQuery.To}' AND City.Name = '{betweenDateQueryAndCity.CityQuery.City}'";

            return DatabaseQuery(queryString);
        }

    }
}
