using BasicWebAPI.DAL;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BasicWebAPI.Query
{
    public class GetWeatherForecastBetweenDatesQuery : BaseWeatherForecastQuery
    {
        public GetWeatherForecastBetweenDatesQuery(IConfiguration config) : base(config)
        {
        }

        public List<WeatherForecastDto> Query(BetweenDateQueryAndCity betweenDateQueryAndCity) 
        {

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE CAST([Date] as Date) BETWEEN '{betweenDateQueryAndCity.BetweenDateQuery.From}' AND '{betweenDateQueryAndCity.BetweenDateQuery.To}' AND City.Name = '{betweenDateQueryAndCity.CityQuery.City}'";


            return DatabaseQuery(queryString);
        }
    }
}
