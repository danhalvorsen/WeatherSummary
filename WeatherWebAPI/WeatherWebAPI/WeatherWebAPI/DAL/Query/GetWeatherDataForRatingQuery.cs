using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI.DAL.Query
{
    public class WeatherTuple : Tuple<
        List<WeatherForecast.WeatherData>,
        List<WeatherForecast.WeatherData>>
    {
        public WeatherTuple(List<WeatherForecast.WeatherData> a, List<WeatherForecast.WeatherData> b) : base(a, b)
        {
            ActualWeather = a;
            PredictedWeather = b;
        }

        public List<WeatherForecast.WeatherData> ActualWeather { get; }
        public List<WeatherForecast.WeatherData> PredictedWeather { get; }
    }

    public class GetWeatherDataForRatingQuery : IGetWeatherDataForRatingQuery
    {

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

            var currentStrategy = (IGetWeatherDataFromDatabaseStrategy)commonArgs.Factory!.Build(StrategyType.GetWeatherDataFromDatabase);

            return new WeatherTuple(
                await currentStrategy.Get(getActualWeather),
                await currentStrategy.Get(getPredictedWeather));
        }

    }

}
