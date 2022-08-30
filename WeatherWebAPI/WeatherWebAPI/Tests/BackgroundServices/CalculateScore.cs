using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;

namespace Tests.BackgroundServices
{
    public class CalculateScoreTest
    {
        private double tempDiff;
        private double windSpdDiff;
        private double windDirDiff;
        private double pressureDiff;
        private double humidityDiff;
        private double probOfRainDiff;
        private double amountRainDiff;
        private double cloudAFDiff;

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

        //private readonly Microsoft.Extensions.Configuration.IConfiguration? _config;
        //private readonly IFactory? _factory;

        private readonly List<WeatherForecast.WeatherData> _actual = new()
        {
            new WeatherForecast.WeatherData
            {
                WeatherForecastId = 5,
                Date = DateTime.UtcNow.AddDays(1),
                DateForecast = DateTime.UtcNow.AddDays(1),
                City = "Stavanger",
                WeatherType = "sunny",
                Temperature = 15,
                WindDirection = 200,
                Windspeed = 5,
                WindspeedGust = 0,
                Pressure = 1001,
                Humidity = 40,
                ProbOfRain = 0,
                AmountRain = 0,
                CloudAreaFraction = 10,
                FogAreaFraction = 0,
                ProbOfThunder = 0,
                Source = new WeatherSourceDto
                {
                    DataProvider = "Yr"
                }
            }
        };
        private readonly List<WeatherForecast.WeatherData> _predicted = new()
        {
            new WeatherForecast.WeatherData
            {
                WeatherForecastId = 3,
                Date = DateTime.UtcNow,
                DateForecast = DateTime.UtcNow.AddDays(1),
                City = "Stavanger",
                WeatherType = "cloudy",
                Temperature = 10,
                WindDirection = 240,
                Windspeed = 15,
                WindspeedGust = 5,
                Pressure = 1003,
                Humidity = 60,
                ProbOfRain = 50,
                AmountRain = 10,
                CloudAreaFraction = 11,
                FogAreaFraction = 5,
                ProbOfThunder = 1,
                Source = new WeatherSourceDto
                {
                    DataProvider = "Yr"
                }
            },
            new WeatherForecast.WeatherData
            {
                WeatherForecastId = 2,
                Date = DateTime.UtcNow,
                DateForecast = DateTime.UtcNow.AddDays(2),
                City = "Stavanger",
                WeatherType = "cloudy",
                Temperature = 10,
                WindDirection = 240,
                Windspeed = 15,
                WindspeedGust = 5,
                Pressure = 1003,
                Humidity = 60,
                ProbOfRain = 50,
                AmountRain = 10,
                CloudAreaFraction = 11,
                FogAreaFraction = 5,
                ProbOfThunder = 1,
                Source = new WeatherSourceDto
                {
                    DataProvider = "Yr"
                }
            }
        };
        private readonly List<Scores> _scores = new()
        {
            new Scores
            {
                Value = 50,
                ValueWeighted = 49,
                WeatherDataId = 0
            },
            new Scores
            {
                Value = 30,
                ValueWeighted = 29,
                WeatherDataId = 1
            },
            new Scores
            {
                Value = 40,
                ValueWeighted = 39,
                WeatherDataId = 2
            }
        };


        [SetUp]
        public void SetUp()
        {
            tempDiff = 0;
            windSpdDiff = 0;
            windDirDiff = 0;
            pressureDiff = 0;
            humidityDiff = 0;
            probOfRainDiff = 0;
            amountRainDiff = 0;
            cloudAFDiff = 0;
        }

        [Test]
        public void CalculateWeatherScore()
        {
            foreach (var actual in _actual)
            {
                foreach (var predicted in _predicted)
                {
                    if (WeatherIdNotRated(_scores, predicted))
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
                            Console.WriteLine($"Score: {score}");

                            var weightedDifferenceProcentage = Math.Round(CalculateWeightedScore(temperatureDifference, pressureDifference, humidityDifference, amountRainDifference,
                                probOfRainDifference, windSpeedDifference, windDirectionDifference, cloudAreaFractionDifference), 2);

                            var weightedScore = 100 - weightedDifferenceProcentage;
                            Console.WriteLine($"Weighted Score: {weightedScore}");

                        }
                    }
                }
            }
        }
        //[Test]
        //public async Task CalculateWeatherScoreWithObject()
        //{
        //    //var sut = new BackGroundServiceCalculateScore(_config, _factory);
        //    //await sut.CalculateScore();
        //}

        [Test]
        public void GetTemperatureDifference()
        {
            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.Temperature} - Predicted: {predicted.Temperature}");
                        tempDiff = CalculateDifference(actual.Temperature, predicted.Temperature);

                        tempDiff.Should().Be(Math.Abs(5));
                        Console.WriteLine($"Diff = {tempDiff} \n% Of OriginalValue: {CalculateDifferencePercentage(actual.Temperature, predicted.Temperature)}"); // % = 66,66% -> 0.6666
                    }
                }
            }
        }

        [Test]
        public void GetWindSpeedDifference()
        {
            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.Windspeed} - Predicted: {predicted.Windspeed}");
                        windSpdDiff = CalculateDifference(actual.Windspeed, predicted.Windspeed);

                        windSpdDiff.Should().Be(Math.Abs(10));
                        Console.WriteLine($"Diff = {windSpdDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.Windspeed, predicted.Windspeed)}"); // % = 50% -> 0.5
                    }
                }
            }
        }

        [Test]
        public void GetWindDirectionDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.WindDirection} - Predicted: {predicted.WindDirection}");
                        windDirDiff = CalculateDifference(actual.WindDirection, predicted.WindDirection);

                        windDirDiff.Should().Be(Math.Abs(40));
                        Console.WriteLine($"Diff = {windDirDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.WindDirection, predicted.WindDirection)}");
                    }
                }
            }
        }

        [Test]
        public void GetPressureDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.Pressure} - Predicted: {predicted.Pressure}");
                        pressureDiff = CalculateDifference(actual.Pressure, predicted.Pressure);

                        pressureDiff.Should().Be(Math.Abs(2));
                        Console.WriteLine($"Diff = {pressureDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.Pressure, predicted.Pressure)}");
                    }
                }
            }
        }

        [Test]
        public void GetHumidityDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.Humidity} - Predicted: {predicted.Humidity}");
                        humidityDiff = CalculateDifference(actual.Humidity, predicted.Humidity);

                        humidityDiff.Should().Be(Math.Abs(20));
                        Console.WriteLine($"Diff = {humidityDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.Humidity, predicted.Humidity)}");
                    }
                }
            }
        }

        [Test]
        public void GetProbOfRainDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.ProbOfRain} - Predicted: {predicted.ProbOfRain}");
                        probOfRainDiff = CalculateDifference(actual.ProbOfRain, predicted.ProbOfRain);

                        probOfRainDiff.Should().Be(Math.Abs(50));
                        Console.WriteLine($"Diff = {probOfRainDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.ProbOfRain, predicted.ProbOfRain)}");
                    }
                }
            }
        }

        [Test]
        public void GetAmountOfRainDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.AmountRain} - Predicted: {predicted.AmountRain}");
                        amountRainDiff = CalculateDifference(actual.AmountRain, predicted.AmountRain);

                        amountRainDiff.Should().Be(Math.Abs(10));
                        Console.WriteLine($"Diff = {amountRainDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.AmountRain, predicted.AmountRain)}");
                    }
                }
            }
        }

        [Test]
        public void GetCloudAreaFractionDifference()
        {

            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source?.DataProvider == predicted.Source?.DataProvider))
                    {
                        Console.WriteLine($"Actual: {actual.CloudAreaFraction} - Predicted: {predicted.CloudAreaFraction}");
                        cloudAFDiff = CalculateDifference(actual.CloudAreaFraction, predicted.CloudAreaFraction);

                        cloudAFDiff.Should().Be(Math.Abs(1));
                        Console.WriteLine($"Diff = {cloudAFDiff} \n" +
                            $"% Of OriginalValue: {CalculateDifferencePercentage(actual.CloudAreaFraction, predicted.CloudAreaFraction)}");

                    }
                }
            }
        }

        // METHODS
        public static double CalculateDifference(double actual, double predicted)
        {
            return Math.Abs(actual - predicted);
        }

        public static double CalculateDifferencePercentage(double actual, double predicted)
        {
            var dividedBy = (actual > predicted) ? actual : predicted;

            return (Math.Abs(actual - predicted) / dividedBy) * 100; /*return (Math.Abs(actual - predicted) / actual) * 100;*/
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

        private static bool WeatherIdNotRated(List<Scores> scores, WeatherForecast.WeatherData predicted)
        {
            return !scores.ToList().Any(i => i.WeatherDataId.Equals(predicted.WeatherForecastId));
        }
    }
}
