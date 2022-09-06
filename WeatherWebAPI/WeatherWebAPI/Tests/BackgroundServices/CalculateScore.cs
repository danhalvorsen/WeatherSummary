using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherWebAPI;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

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

        private static readonly DateTime DAY_TOMORROW = DateTime.UtcNow.AddDays(1);
        private const float ACTUAL_VALUE = 3.0f;
        private const float PREDICTED_VALUE = 1.0f;

        private Dictionary<string, CityDto>? citiesDictionary;

        private readonly List<WeatherForecast.WeatherData> _actual = new()
        {
            new WeatherForecast.WeatherData
            {
                WeatherForecastId = 5,
                Date = DAY_TOMORROW,
                DateForecast = DAY_TOMORROW,
                City = "Stavanger",
                WeatherType = "sunny",
                Temperature = ACTUAL_VALUE,
                WindDirection = ACTUAL_VALUE,
                Windspeed = ACTUAL_VALUE,
                WindspeedGust = 0,
                Pressure = ACTUAL_VALUE,
                Humidity = ACTUAL_VALUE,
                ProbOfRain = ACTUAL_VALUE,
                AmountRain = ACTUAL_VALUE,
                CloudAreaFraction = ACTUAL_VALUE,
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
                DateForecast = DAY_TOMORROW,
                City = "Stavanger",
                WeatherType = "cloudy",
                Temperature = PREDICTED_VALUE,
                WindDirection = PREDICTED_VALUE,
                Windspeed = PREDICTED_VALUE,
                WindspeedGust = 5,
                Pressure = PREDICTED_VALUE,
                Humidity = PREDICTED_VALUE,
                ProbOfRain = PREDICTED_VALUE,
                AmountRain = PREDICTED_VALUE,
                CloudAreaFraction = PREDICTED_VALUE,
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
                DateForecast = DAY_TOMORROW.AddDays(1),
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
                WeatherForecastId = 1,
                Date = DateTime.UtcNow,
                DateForecast = DAY_TOMORROW,
                City = "Stavanger",
                WeatherType = "sunny",
                Temperature = ACTUAL_VALUE,
                WindDirection = ACTUAL_VALUE,
                Windspeed = ACTUAL_VALUE,
                WindspeedGust = 0,
                Pressure = ACTUAL_VALUE,
                Humidity = ACTUAL_VALUE,
                ProbOfRain = ACTUAL_VALUE,
                AmountRain = ACTUAL_VALUE,
                CloudAreaFraction = ACTUAL_VALUE,
                FogAreaFraction = 0,
                ProbOfThunder = 0,
                Source = new WeatherSourceDto
                {
                    DataProvider = "Yr"
                }
            }
        };
        //private readonly List<Scores> _scores = new()
        //{
        //    new Scores
        //    {
        //        Value = 50,
        //        ValueWeighted = 49,
        //        WeatherDataId = 0
        //    },
        //    new Scores
        //    {
        //        Value = 30,
        //        ValueWeighted = 29,
        //        WeatherDataId = 1
        //    },
        //    new Scores
        //    {
        //        Value = 40,
        //        ValueWeighted = 39,
        //        WeatherDataId = 2
        //    }
        //};

        private readonly List<CityDto> _cities = new()
        {
            new CityDto
            {
                Name = "Stavanger",
                Country = "Norway",
                Altitude = 0,
                Latitude = 59.1020129,
                Longitude = 5.712611357275702,
            },
            //new CityDto
            //{
            //    Name = "Bergen",
            //    Country = "Norway",
            //    Altitude = 0,
            //    Latitude = 62.1234567,
            //    Longitude = 5.87654321,
            //},
            //new CityDto
            //{
            //    Name = "Trondheim",
            //    Country = "Norway",
            //    Altitude = 0,
            //    Latitude = 64.1234567,
            //    Longitude = 11.234567,
            //}
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

            citiesDictionary = new();
            citiesDictionary.Add("Stavanger", new CityDto
            {
                Name = "Stavanger",
                Country = "Norway",
                Altitude = 0,
                Latitude = 59.1020129,
                Longitude = 5.712611357275702,
            });
        }

        [Test]
        public async Task Should_Calculate_Score_For_City_Stavanger()
        {
            //AAA - Arrange
            var configMock = new Mock<IConfiguration>();
            var factoryMock = new Mock<IFactory<IStrategy, StrategyType>>();
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<BackgroundServiceCalculateScoreQuery>>();

            var mockTuple = new Mock<WeatherTuple>(_actual, _predicted);

            var getCitiesMock = new Mock<IGetCitiesQuery>();
            getCitiesMock.Setup(M => M.GetAllCities()).ReturnsAsync(_cities);

            var getDataForRatingMock = new Mock<IGetWeatherDataForRatingQuery>();
            getDataForRatingMock.Setup(M => M.Get(_cities[0])).ReturnsAsync(mockTuple.Object); // _cities[0] = Stavanger

            //Act 
            var sut = new BackgroundServiceCalculateScoreQuery(getCitiesMock.Object, getDataForRatingMock.Object, loggerMock.Object);
            var resultScores = await sut.CalculateScore();

            foreach (var s in resultScores)
            {
                Console.WriteLine(s.Value);
                Console.WriteLine(s.ValueWeighted);
            }

            //Assert
            resultScores.Should().NotBeNull().And.NotBeEmpty();
            resultScores.First().ValueWeighted.Should().Be(98f);
            resultScores.First().Value.Should().Be(33.33f);
        }

        [Test]
        public async Task Should_Calculate_Score_For_City_Stavanger_When_Weather_Is_100_Percent_Correct()
        {
            //AAA - Arrange
            var configMock = new Mock<IConfiguration>();
            var factoryMock = new Mock<IFactory<IStrategy, StrategyType>>();
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<BackgroundServiceCalculateScoreQuery>>();

            var mockTuple = new Mock<WeatherTuple>(_actual, _predicted);

            var getCitiesMock = new Mock<IGetCitiesQuery>();
            getCitiesMock.Setup(M => M.GetAllCities()).ReturnsAsync(_cities);

            var getDataForRatingMock = new Mock<IGetWeatherDataForRatingQuery>();
            getDataForRatingMock.Setup(M => M.Get(_cities[0])).ReturnsAsync(mockTuple.Object); // _cities[0] = Stavanger

            //Act 
            var sut = new BackgroundServiceCalculateScoreQuery(getCitiesMock.Object, getDataForRatingMock.Object, loggerMock.Object);
            var resultScores = await sut.CalculateScore();

            foreach (var s in resultScores)
            {
                Console.WriteLine(s.Value);
                Console.WriteLine(s.ValueWeighted);
            }

            //Assert
            resultScores.Should().NotBeNull().And.NotBeEmpty();
            resultScores.Should().Contain(resultScores.Find(i => i.Value.Equals(100) && i.ValueWeighted.Equals(100))!);
        }

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

                        tempDiff.Should().Be(Math.Abs(actual.Temperature - predicted.Temperature));
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

                        windSpdDiff.Should().Be(Math.Abs(actual.Windspeed - predicted.Windspeed));
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

                        windDirDiff.Should().Be(Math.Abs(actual.WindDirection - predicted.WindDirection));
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

                        pressureDiff.Should().Be(Math.Abs(actual.Pressure - predicted.Pressure));
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

                        humidityDiff.Should().Be(Math.Abs(actual.Humidity - predicted.Humidity));
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

                        probOfRainDiff.Should().Be(Math.Abs(actual.ProbOfRain - predicted.ProbOfRain));
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

                        amountRainDiff.Should().Be(Math.Abs(actual.AmountRain - predicted.AmountRain));
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

                        cloudAFDiff.Should().Be(Math.Abs(actual.CloudAreaFraction - predicted.CloudAreaFraction));
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
