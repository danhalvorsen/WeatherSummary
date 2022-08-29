using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetAvgScoresForSelectedPredictionLengthForCityQuery : BaseFunctionsForQueriesAndCommands
    {
        public GetAvgScoresForSelectedPredictionLengthForCityQuery(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<ScoresAverageForCityDto>> CalculateAverageScoresForSelectedPredictionLengthAndCity(DaysQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
            var avgScoreForCityList = new List<ScoresAverageForCityDto>();
            string? citySearchedFor = query.CityQuery?.City;
            int? days = query.DaysQuery?.Days;
            var getCitiesQueryDatabase = new GetCitiesQuery(_config);

            try
            {
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                string? cityName;

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;
                }
                else
                {
                    cityName = citySearchedFor;
                }

                foreach (var strategy in weatherDataStrategies)
                {
                    string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND City.[Name] = '{cityName}' AND Score.FK_WeatherDataId IS NOT null AND CAST([DateForecast] as Date) = [Date] + {days}";

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

                    avgScoreForCityList.Add(new ScoresAverageForCityDto
                    {
                        City = cityName,
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
            return avgScoreForCityList;
        }
    }
}
