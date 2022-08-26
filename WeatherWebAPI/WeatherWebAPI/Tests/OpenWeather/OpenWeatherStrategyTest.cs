using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherStrategyTest
    {
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;
        private DateTime _date;
        private IGetWeatherDataStrategy<WeatherForecast>? _strategy;

        private CityDto _city = new CityDto
        {
            Name = "Stavanger",
            Country = "Norway",
            Altitude = 0,
            Latitude = 59.1020129,
            Longitude = 5.712611357275702,
        };

        [SetUp]
        public void SetUp()
        {
            IServiceCollection servicecollection = new ServiceCollection();
            var whatafouck = new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") /*Assembly.GetExecutingAssembly() */}; //Assembly.LoadFrom("WeatherWebAPI.dll")
            servicecollection.AddAutoMapper(whatafouck);
            _serviceProvider = servicecollection.BuildServiceProvider();

            _mapper = _serviceProvider.GetService<IMapper>();
            _strategy = new OpenWeatherStrategy(_mapper!, new OpenWeatherConfig(), new HttpClient());
        }

        [Test]
        public async Task ShouldGetDateForecast()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.DateForecast);
            }

            result.Forecast.ToList().Select(i => i.DateForecast).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetDate()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.Date);
            }

            result.Forecast.ToList().Select(i => i.Date).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetTemperature()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.Temperature);
            }

            result.Forecast.ToList().Select(i => i.Temperature).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetWindspeed()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.Windspeed);
            }

            result.Forecast.ToList().Select(i => i.Windspeed).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetWindDirection()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.WindDirection);
            }

            result.Forecast.ToList().Select(i => i.WindDirection).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetWindspeedGust()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.WindspeedGust);
            }

            result.Forecast.ToList().Select(i => i.WindspeedGust).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetPressure()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.Pressure);
            }

            result.Forecast.ToList().Select(i => i.Pressure).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetHumidity()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.Humidity);
            }

            result.Forecast.ToList().Select(i => i.Humidity).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetProbabilityOfRain()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.ProbOfRain);
            }

            result.Forecast.ToList().Select(i => i.ProbOfRain).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetAmountOfRain()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.AmountRain);
            }

            result.Forecast.ToList().Select(i => i.AmountRain).Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetCloudAreaFraction()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.CloudAreaFraction);
            }

            result.Forecast.ToList().Select(i => i.CloudAreaFraction).Should().NotBeEmpty();
        }

        //[Test]
        //public async Task ShouldGetFogAreaFraction()
        //{
        //    //var result = await _strategy.GetWeatherDataFrom(_city, _date);

        //    //Console.WriteLine(result.FogAreaFraction);

        //    //result.FogAreaFraction
        //    //    .Should()
        //    //        .BeGreaterThanOrEqualTo(0)
        //    //        .And
        //    //        .BeLessThanOrEqualTo(100);
        //}

        //[Test]
        //public async Task ShouldGetProbabilityOfThunder()
        //{
        //    //var result = await _factory.GetWeatherDataFrom(_strategy);

        //    //Console.WriteLine(result.ProbOfRain);

        //    //result.ProbOfThunder
        //    //    .Should()
        //    //        .BeGreaterThanOrEqualTo(0)
        //    //        .And
        //    //        .BeLessThanOrEqualTo(100);
        //}

        [Test]
        public async Task ShouldGetWeatherType()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            var item = result!.Forecast.ToList();

            foreach (var i in item)
            {
                Console.WriteLine(i.WeatherType);
            }

            result.Forecast.ToList().Select(i => i.WeatherType).Should().NotBeEmpty();
        }
    }
}
