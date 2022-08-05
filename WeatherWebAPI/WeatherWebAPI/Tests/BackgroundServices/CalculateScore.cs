using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;

namespace Tests.BackgroundServices
{
    public class CalculateScore
    {
        private double tempDiff;
        private double windSpdDiff;
        private double windDirDiff;
        private double pressureDiff;
        private double humidityDiff;
        private double probOfRainDiff;
        private double amountRainDiff;
        private double cloudAFDiff;

        private readonly List<WeatherForecastDto>? _actual = new()
        {
            new WeatherForecastDto
            {
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
        private readonly List<WeatherForecastDto> _predicted = new()
        {
            new WeatherForecastDto
            {
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
            }
        };

        [SetUp]
        public void SetUp()
        {
            //  Originalt tall -nytt tall = differanse
            //  (Differanse / originalt tall) * 100 = Prosentnedgang(%)

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
            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
                    {
                        tempDiff = Math.Abs(actual.Temperature - predicted.Temperature);
                        windSpdDiff = Math.Abs(actual.Windspeed - predicted.Windspeed);
                        windDirDiff = Math.Abs(actual.WindDirection - predicted.WindDirection);
                        pressureDiff = Math.Abs(actual.Pressure - predicted.Pressure);
                        humidityDiff = Math.Abs(actual.Humidity - predicted.Humidity);
                        probOfRainDiff = Math.Abs(actual.ProbOfRain - predicted.ProbOfRain);
                        amountRainDiff = Math.Abs(actual.AmountRain - predicted.AmountRain);
                        cloudAFDiff = Math.Abs(actual.CloudAreaFraction - predicted.CloudAreaFraction);

                        var sumActual = SumWeatherScoreVariables(actual);
                        var sumPredicted = SumWeatherScoreVariables(predicted);

                        Console.WriteLine($"Actual Weather Sum: {sumActual}");
                        Console.WriteLine($"Predicted Weather Sum: {sumPredicted}");

                        var difference = Math.Abs(sumActual - sumPredicted);
                        Console.WriteLine($"Difference: {difference}");


                        var score = Math.Round((sumActual - difference) / sumActual * 100, 2);
                        Console.WriteLine("Score: " + score);

                        var sumDiff = tempDiff + pressureDiff + humidityDiff + cloudAFDiff + amountRainDiff + probOfRainDiff + windSpdDiff + windDirDiff;
                        Console.WriteLine($"All value differences added together: {sumDiff}");

                        var scoreWeight = (tempDiff * 0.3) + (pressureDiff * 0.2) + (humidityDiff * 0.15) + (cloudAFDiff * 0.05)
                            + (amountRainDiff * 0.1) + (probOfRainDiff * 0.05) + (windSpdDiff * 0.1) + (windDirDiff * 0.05);
                        Console.WriteLine($"{scoreWeight}");

                        score.Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(100);
                    }
                }
            }
        }

        [Test]
        public void GetTemperatureDifference()
        {
            foreach (var actual in _actual!)
            {
                foreach (var predicted in _predicted!)
                {
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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
                    if ((actual.Date.Date == predicted.DateForecast.Date) && (actual.Source.DataProvider == predicted.Source.DataProvider))
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

            return (Math.Abs(actual - predicted) / actual) * 100;
        }

        private static double CalculatePercentage(double sumActualWeather, double difference)
        {
            return (sumActualWeather - difference) / sumActualWeather * 100;
        }

        private static double SumWeatherScoreVariables(WeatherForecastDto forecast)
        {
            return Math.Abs(forecast.Temperature + forecast.Windspeed + forecast.WindDirection +
                                forecast.Pressure + forecast.Humidity + forecast.ProbOfRain + forecast.AmountRain +
                                    forecast.CloudAreaFraction);
        }
    }
}
