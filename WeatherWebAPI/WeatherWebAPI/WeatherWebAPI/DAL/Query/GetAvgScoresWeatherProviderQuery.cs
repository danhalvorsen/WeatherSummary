﻿using AutoMapper;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI.DAL.Query
{
    public class GetAvgScoresWeatherProviderQuery : BaseFunctionsForQueriesAndCommands
    {
        private readonly IMapper _mapper;
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;

        public GetAvgScoresWeatherProviderQuery(
            IMapper mapper, 
            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy
            ) : base()
        {
            _mapper = mapper;
            _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
        }

        public async Task<List<ScoresAverageDto>> CalculateAverageScores(IEnumerable<IGetWeatherDataStrategy> weatherDataStrategies)
        {
            var avgScoreList = new List<ScoresAverageDto>();
            try
            {
                foreach (var strategy in weatherDataStrategies)
                {
                    string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                            $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                        $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                            $"LEFT JOIN Score ON WeatherData.Id = Score.FK_WeatherDataId " +
                                                                $"WHERE[Source].Name = '{strategy.GetDataSource()}' AND Score.FK_WeatherDataId IS NOT null";

                    var weatherForecasts = await _getWeatherDataFromDatabaseStrategy.Get(queryString);
                    var weatherForecastsDto = _mapper.Map<List<WeatherForecastDto>>(weatherForecasts);

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
