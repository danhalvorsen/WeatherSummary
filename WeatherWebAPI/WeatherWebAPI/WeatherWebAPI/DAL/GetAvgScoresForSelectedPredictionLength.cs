using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetAvgScoresForSelectedPredictionLength : BaseCommands
    {
        public GetAvgScoresForSelectedPredictionLength(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<ScoresAverageDto>> CalculateAverageScoresForSelectedPredictionLength(DaysQuery query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
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

                    IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

                    var scoreData = await getWeatherDataFromDatabaseStrategy.Get(queryString);

                    float sumScore = 0;
                    float sumScoreWeighted = 0;

                    foreach (var score in scoreData)
                    {
                        sumScore += score.Score.Score;
                        sumScoreWeighted += score.Score.ScoreWeighted;
                    }

                    float avgScore = (float)CalculateAverageScore(sumScore, scoreData);
                    float avgScoreWeighted = (float)CalculateAverageScore(sumScoreWeighted, scoreData);

                    avgScoreList.Add(new ScoresAverageDto
                    {
                        AverageScore = avgScore,
                        AverageScoreWeighted = avgScoreWeighted,
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
