using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;

namespace WeatherWebAPI.DAL
{
    public class GetAverageScoresCommand : BaseGetWeatherForecastCommands
    {
        public GetAverageScoresCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<ScoresAverageDto>> CalculateAverageScores(List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
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

                    double? sumScore = 0.0;
                    double? sumScoreWeighted = 0.0;

                    foreach (var score in scoreData)
                    {
                        sumScore += score.Score?.Score;
                        sumScoreWeighted += score.Score?.ScoreWeighted;
                    }

                    var avgScore = CalculateAverage(sumScore, scoreData);
                    var avgScoreWeighted = CalculateAverage(sumScoreWeighted, scoreData);

                    avgScoreList.Add(new ScoresAverageDto
                    {
                        AverageScore = avgScore,
                        AveragecoreWeighted = avgScoreWeighted,
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

        //public async Task CalculateAverageScoresFor(CityDto city, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        //{
        //    try
        //    {
        //        foreach (var strategy in weatherDataStrategies)
        //        {
        //            string queryString = $"SELECT WeatherData.Id, City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
        //                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
        //                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
        //                        $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
        //                            $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
        //                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND City.Name = '{city.Name}' " +
        //                                $"AND Score.Score = ISNULL(Score.Score, 0) AND Score.ScoreWeighted = ISNULL(Score.ScoreWeighted, 0)";
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        Console.WriteLine(e.Message);
        //    }
        //}

        public static double? CalculateAverage(double? sum, List<WeatherForecastDto> data)
        {
            return sum / data.Count;
        }
    }
}
