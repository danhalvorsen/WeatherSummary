using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherDataForRatingQuery : IGetWeatherDataForRatingQuery
    {
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;

        public GetWeatherDataForRatingQuery(IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy)
        {
            _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
        }

        public async Task<WeatherTuple> Get(CityDto city)
        {


            string getActualWeather = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                          $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                              $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                  $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                      $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                          $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                              $"WHERE CAST(DateForecast as date) = CAST([Date] as date) AND City.Name = '{city.Name}' AND Score.FK_WeatherDataId IS null " +
                                                                  $"ORDER BY[Date]";

            string getPredictedWeather = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE CAST(DateForecast as date) != CAST([Date] as date) AND City.Name = '{city.Name}' AND Score.FK_WeatherDataId IS null " +
                                                                    $"ORDER BY [Date]";

            return new WeatherTuple(
                await _getWeatherDataFromDatabaseStrategy.Get(getActualWeather),
                await _getWeatherDataFromDatabaseStrategy.Get(getPredictedWeather));
        }

    }

}
