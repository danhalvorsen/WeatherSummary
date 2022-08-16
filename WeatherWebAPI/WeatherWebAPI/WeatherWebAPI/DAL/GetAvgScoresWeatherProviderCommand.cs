using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;

namespace WeatherWebAPI.DAL
{
    public class GetAvgScoresWeatherProviderCommand : BaseCommands
    {
        public GetAvgScoresWeatherProviderCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<ScoresAverageDto>> CalculateAverageScores(List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
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
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND Score.FK_WeatherDataId IS NOT null";

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
