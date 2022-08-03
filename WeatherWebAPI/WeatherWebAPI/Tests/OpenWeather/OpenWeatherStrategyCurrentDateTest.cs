using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherStrategyCurrentDateTest
    {
        private readonly IGetWeatherDataStrategy<WeatherForecastDto> _strategy = new OpenWeatherStrategy(new OpenWeatherConfig());

        private CityDto _city = new CityDto
        {
            Name = "Stavanger",
            Country = "Norway",
            Altitude = 0,
            Latitude = 59.1020129,
            Longitude = 5.712611357275702,
        };

        // Checking so it works without time part. 
        private DateTime _date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);

        [Test]
        public async Task ShouldGetDateForecast()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            //result.Should().NotBeEmpty(); <- Used when GetWeatherDataFrom returned List<WeatherForecastDto>
            Console.WriteLine(result.DateForecast);

            result.DateForecast.Date
                .Should()
                    .Be(_date);
        }

        [Test]
        public async Task ShouldGetDate()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            //result.Should().NotBeEmpty(); <- Used when GetWeatherDataFrom returned List<WeatherForecastDto>
            Console.WriteLine(result.Date);

            result.Date.Date
                .Should()
                    .Be(DateTime.UtcNow.Date);
        }

        [Test]
        public async Task ShouldGetTemperature()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

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
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Windspeed);

            result.Windspeed
                .Should()
                    .BeGreaterOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetWindDirection()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

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
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.WindspeedGust);

            result.WindspeedGust
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetPressure()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Pressure);

            result.Pressure
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetHumidity()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

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
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

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
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.AmountRain);

            result.AmountRain
                .Should()
                    .BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetCloudAreaFraction()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

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
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.FogAreaFraction);

            result.FogAreaFraction
                .Should()
                    .BeGreaterThanOrEqualTo(0)
                    .And
                    .BeLessThanOrEqualTo(100);
        }

        //[Test]
        //public async Task ShouldGetProbabilityOfThunder()
        //{
        //    var result = await _factory.GetWeatherDataFrom(_strategy);

        //    Console.WriteLine(result.ProbOfRain);

        //    result.ProbOfThunder
        //        .Should()
        //            .BeGreaterThanOrEqualTo(0)
        //            .And
        //            .BeLessThanOrEqualTo(100);
        //}

        [Test]
        public async Task ShouldGetWeatherType()
        {
            var result = await _strategy.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.WeatherType);

            result.WeatherType
                .Should()
                    .NotBeNullOrEmpty();
        }
    }
}
