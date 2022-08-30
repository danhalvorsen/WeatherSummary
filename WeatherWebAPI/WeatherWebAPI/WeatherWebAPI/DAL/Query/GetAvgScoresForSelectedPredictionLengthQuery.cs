using WeatherWebAPI.Arguments;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetAvgScoresForSelectedPredictionLengthQuery : BaseFunctionsForQueriesAndCommands
    {
        public GetAvgScoresForSelectedPredictionLengthQuery(CommonArgs commonArgs) : base(commonArgs)
        {

        }

        public async Task<List<ScoresAverageDto>> CalculateAverageScoresForSelectedPredictionLength(DaysQuery query, List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
            var days = query.Days;
            var avgScoreList = new List<ScoresAverageDto>();
            try
            {
                foreach (var strategy in weatherDataStrategies)
                {

                    string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND Score.FK_WeatherDataId IS NOT null AND CAST([DateForecast] as Date) = [Date] + {days}";

                    IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _commonArgs.Factory.Build<IGetWeatherDataFromDatabaseStrategy>();

                    var weatherForecasts = await getWeatherDataFromDatabaseStrategy.Get(queryString);
                    var weatherForecastsDto = _commonArgs.Mapper.Map<List<WeatherForecastDto>>(weatherForecasts);

                    float sumScore = 0;
                    float sumScoreWeighted = 0;

                    foreach (var forecast in weatherForecastsDto)
                    {
                        sumScore += forecast.Score.Value;
                        sumScoreWeighted += forecast.Score.ValueWeighted;
                    }

                    float avgScore = (float)CalculateAverageScore(sumScore, weatherForecastsDto);
                    float avgScoreWeighted = (float)CalculateAverageScore(sumScoreWeighted, weatherForecastsDto);

                    avgScoreList.Add(new ScoresAverageDto
                    {
                        AverageValue = avgScore,
                        AverageWeightedValue = avgScoreWeighted,
                        DataProvider = strategy.GetDataSource()
                    });

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return avgScoreList;
        }
    }
}
