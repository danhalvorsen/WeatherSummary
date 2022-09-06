using System.Diagnostics;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class BackgroundServiceCalculateScoreQuery : IBackgroundServiceCalculateScoreQuery
    {
        private const double WEIGHT_TEMPERATURE = 0.3;
        private const double WEIGHT_PRESSURE = 0.2;
        private const double WEIGHT_HUMIDITY = 0.15;
        private const double WEIGHT_AMOUNT_RAIN = 0.1;
        private const double WEIGHT_PROB_OF_RAIN = 0.05;
        private const double WEIGHT_WIND_SPEED = 0.1;
        private const double WEIGHT_WIND_DIRECTION = 0.05;
        private const double WEIGHT_CLOUD_AREA_FRACTION = 0.05;
        private const double WEIGHT_SUM = WEIGHT_TEMPERATURE + WEIGHT_PRESSURE + WEIGHT_HUMIDITY + WEIGHT_AMOUNT_RAIN + WEIGHT_PROB_OF_RAIN +
            WEIGHT_WIND_SPEED + WEIGHT_WIND_DIRECTION + WEIGHT_CLOUD_AREA_FRACTION;
        
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IGetWeatherDataForRatingQuery _getWeatherDataForRating;
        private readonly ILogger<BackgroundServiceCalculateScoreQuery> _logger;

        public BackgroundServiceCalculateScoreQuery(
            IGetCitiesQuery getCitiesQuery,
            IGetWeatherDataForRatingQuery getWeatherDataForRating,
            ILogger<BackgroundServiceCalculateScoreQuery> logger)
        {
            _getCitiesQuery = getCitiesQuery;
            _getWeatherDataForRating = getWeatherDataForRating;
            _logger = logger;
        }

        public async Task<List<Scores>> CalculateScore()
        {
            var scoresList = new List<Scores>();

            try
            {
                var cities = await _getCitiesQuery.GetAllCities();

                foreach (var city in cities)
                {

                    var getWeatherToScoreForCity = await _getWeatherDataForRating.Get(city);

                    foreach (var actual in getWeatherToScoreForCity.ActualWeather)
                    {
                        foreach (var predicted in getWeatherToScoreForCity.PredictedWeather)
                        {
                            if (actual.Date.Date == predicted.DateForecast.Date && actual.Source?.DataProvider == predicted.Source?.DataProvider && actual.City == predicted.City)
                            {
                                var temperatureDifference = Math.Abs(actual.Temperature - predicted.Temperature);
                                var pressureDifference = Math.Abs(actual.Pressure - predicted.Pressure);
                                var humidityDifference = Math.Abs(actual.Humidity - predicted.Humidity);
                                var amountRainDifference = Math.Abs(actual.AmountRain - predicted.AmountRain);
                                var probOfRainDifference = Math.Abs(actual.ProbOfRain - predicted.ProbOfRain);
                                var windSpeedDifference = Math.Abs(actual.Windspeed - predicted.Windspeed);
                                var windDirectionDifference = Math.Abs(actual.WindDirection - predicted.WindDirection);
                                var cloudAreaFractionDifference = Math.Abs(actual.CloudAreaFraction - predicted.CloudAreaFraction);

                                var sumActual = SumWeatherScoreVariables(actual);
                                var sumPredicted = SumWeatherScoreVariables(predicted);
                                var difference = Math.Abs(sumActual - sumPredicted);

                                var score = Math.Round(CalculatePercentage(sumActual, difference), 2);
                                _logger.LogInformation("Score: {Score}", score);

                                var weightedDifferencePercentage = Math.Round(CalculateWeightedScore(temperatureDifference, pressureDifference, humidityDifference, amountRainDifference,
                                    probOfRainDifference, windSpeedDifference, windDirectionDifference, cloudAreaFractionDifference), 2);

                                var weightedScore = 100 - weightedDifferencePercentage;
                                _logger.LogInformation("Weighted Score: {Weighted Score}", weightedScore);

                                scoresList.Add(new Scores
                                {
                                    Value = (float)score,
                                    ValueWeighted = (float)weightedScore,
                                    WeatherDataId = predicted.WeatherForecastId
                                });
                                //await AddScoreToDatabase(score, weightedScore, predicted.WeatherForecastId);
                            }
                        }
                    }
                }
                return scoresList;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError("{Error}", ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("{Error}", e.Message);
            }

            throw new Exception("Error fetching data from database");
        }

        private static double CalculatePercentage(double sumActualWeather, double difference)
        {
            return (sumActualWeather - difference) / sumActualWeather * 100;
        }

        private static double SumWeatherScoreVariables(WeatherForecast.WeatherData forecast)
        {
            return Math.Abs(forecast.Temperature + forecast.Windspeed + forecast.WindDirection +
                                forecast.Pressure + forecast.Humidity + forecast.ProbOfRain + forecast.AmountRain +
                                    forecast.CloudAreaFraction);
        }

        private static double CalculateWeightedScore(double tempDiff, double pressureDiff,
            double humidityDiff, double amountRainDiff, double probOfRainDiff,
                double windSpdDiff, double windDirDiff, double cloudAFDiff)
        {
            Debug.Assert(WEIGHT_SUM <= 100 && WEIGHT_SUM >= 0);
            var weightedScore = ((tempDiff * WEIGHT_TEMPERATURE) + (pressureDiff * WEIGHT_PRESSURE) + (humidityDiff * WEIGHT_HUMIDITY) +
                        (amountRainDiff * WEIGHT_AMOUNT_RAIN) + (probOfRainDiff * WEIGHT_PROB_OF_RAIN) + (windSpdDiff * WEIGHT_WIND_SPEED) +
                            (windDirDiff * WEIGHT_WIND_DIRECTION) + (cloudAFDiff * WEIGHT_CLOUD_AREA_FRACTION))
                                / (WEIGHT_SUM);

            return weightedScore;
        }
    }
}
