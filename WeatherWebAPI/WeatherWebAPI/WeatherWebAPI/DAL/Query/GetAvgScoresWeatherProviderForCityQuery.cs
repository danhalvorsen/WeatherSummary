using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetAvgScoresWeatherProviderForCityQuery : BaseFunctionsForQueriesAndCommands
    {

        public GetAvgScoresWeatherProviderForCityQuery() : base()
        {

        }

        public async Task<List<ScoresAverageForCityDto>> CalculateAverageScoresFor(CityQuery query, IEnumerable<IStrategy> weatherDataStrategies)
        {
            var avgScoreForCityList = new List<ScoresAverageForCityDto>();
            string? citySearchedFor = query?.City;
            var getCitiesQueryDatabase = new GetCitiesQuery(_commonArgs!.Config!);

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

                foreach (IGetWeatherDataStrategy strategy in weatherDataStrategies)
                {
                    string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource}' AND City.[Name] = '{cityName}' AND Score.FK_WeatherDataId IS NOT null";

                    var getWeatherDataFromDatabaseStrategy = (IGetWeatherDataFromDatabaseStrategy)_commonArgs!.Factory!.Build(StrategyType.GetWeatherDataFromDatabase);
                    var weatherForecasts = await getWeatherDataFromDatabaseStrategy.Get(queryString);
                    var weatherForecastsDto = _commonArgs.Mapper!.Map<List<WeatherForecastDto>>(weatherForecasts);

                    float sumScore = 0;
                    float sumScoreWeighted = 0;

                    foreach (var forecast in weatherForecastsDto)
                    {
                        sumScore += forecast.Score.Value;
                        sumScoreWeighted += forecast.Score.ValueWeighted;
                    }

                    float avgScore = (float)CalculateAverageScore(sumScore, weatherForecastsDto);
                    float avgScoreWeighted = (float)CalculateAverageScore(sumScoreWeighted, weatherForecastsDto);

                    avgScoreForCityList.Add(new ScoresAverageForCityDto
                    {
                        City = cityName,
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
            return avgScoreForCityList;
        }
    }
}
