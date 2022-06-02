using AutoMapper;
using BasicWebAPI.Factory;
using BasicWebAPI.YR;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class YrStrategyTest
    {
        private GetWeatherDataFactory _factory = new GetWeatherDataFactory();
        private IWeatherDataStrategy _strategy = new YrStrategy();
        private double _latitude = 59.1020129; // Coordinates from OpenWeather for Stavanger
        private double _longitude = 5.712611357275702;

        [Test]
        public async Task ShouldGetDate()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            //result.Should().NotBeEmpty(); <- Used when GetWeatherDataFrom returned List<WeatherForecastDto>
            Console.WriteLine(result.Date);
            
            result.Date
                .Date
                    .Should()
                        .Be(DateTime.Now.Date);
        }

        [Test]
        public async Task ShouldGetTemperature()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.Temperature);
            
            result.Temperature
            .Should()
                .BeGreaterThan(-20)
                .And
                .BeLessThan(100);
        }

        [Test]
        public async Task ShouldGetWindspeed()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.Windspeed);

            result.Windspeed
                .Should()
                    .BeGreaterOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetWindDirection()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.WindDirection);

            result.WindDirection
                .Should()
                    .BeGreaterOrEqualTo(0) // 0 = North, 90 = East, 180 = South, 270 = West
                    .And
                    .BeLessThanOrEqualTo(360);
        }

        [Test]
        public async Task ShouldGetWindspeedGust()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.WindspeedGust);

            result.WindspeedGust
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetPressure()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.Pressure);

            result.Pressure
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetHumidity()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.Humidity);

            result.Humidity
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetProbabilityOfRain()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.ProbOfRain);

            result.ProbOfRain
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetAmountOfRain()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.AmountRain);

            result.AmountRain
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetCloudAreaFraction()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.CloudAreaFraction);

            result.CloudAreaFraction
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetFogAreaFraction()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.FogAreaFraction);

            result.FogAreaFraction
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetProbabilityOfThunder()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.ProbOfRain);

            result.ProbOfThunder
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetWeatherType()
        {
            var result = await _factory.GetWeatherDataFrom(_latitude, _longitude, _strategy);

            Console.WriteLine(result.WeatherType);

            result.WeatherType
                .Should()
                    .NotBeNullOrEmpty();
        }
    }
}
