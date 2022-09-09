using AutoMapper;
using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetAvgScoresForSelectedPredictionLengthForCityQuery : BaseFunctionsForQueriesAndCommands
    {
        private readonly IMapper _mapper;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;
        private readonly IOpenWeatherFetchCityStrategy _openWeatherFetchCityStrategy;
        
        public GetAvgScoresForSelectedPredictionLengthForCityQuery(
            IMapper mapper,
            IGetCitiesQuery getCitiesQuery,
            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy,
            IOpenWeatherFetchCityStrategy openWeatherFetchCityStrategy
            ) : base()
        {
            _mapper = mapper;
            _getCitiesQuery = getCitiesQuery;
            _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
            _openWeatherFetchCityStrategy = openWeatherFetchCityStrategy;
        }

        public async Task<List<ScoresAverageForCityDto>> CalculateAverageScoresForSelectedPredictionLengthAndCity(DaysQueryAndCity query, List<IGetWeatherDataStrategy> weatherDataStrategies)
        {
            var avgScoreForCityList = new List<ScoresAverageForCityDto>();
            string? citySearchedFor = query.CityQuery?.City;
            int? days = query.DaysQuery?.Days;

            try
            {
                var cities = await _getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                var textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                string? cityName;

                if (!CityExists(citySearchedFor, cities))
                {
                    var cityData = await GetCityData(citySearchedFor, _openWeatherFetchCityStrategy);
                    cityName = cityData[0].Name;
                }
                else
                {
                    cityName = citySearchedFor;
                }

                foreach (var strategy in weatherDataStrategies)
                {
                    string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND City.[Name] = '{cityName}' AND Score.FK_WeatherDataId IS NOT null AND CAST([DateForecast] as Date) = [Date] + {days}";

                    var weatherForecasts = await _getWeatherDataFromDatabaseStrategy.Get(queryString);
                    var weatherForecastsDto = _mapper!.Map<List<WeatherForecastDto>>(weatherForecasts);

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
            catch (Exception)
            {
                throw;
            }
            return avgScoreForCityList;
        }
    }
}
