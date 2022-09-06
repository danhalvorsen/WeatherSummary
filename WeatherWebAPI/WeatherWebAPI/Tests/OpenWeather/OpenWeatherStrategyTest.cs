﻿using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using static WeatherWebAPI.Contracts.BaseContract.WeatherForecast;

namespace Tests.OpenWeather
{
    public class OpenWeatherStrategyTest
    {
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;
        private DateTime _date;
        private IGetWeatherDataStrategy? _strategy;

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
            _date = DateTime.UtcNow;

            _mapper = _serviceProvider.GetService<IMapper>();
            _strategy = new OpenWeatherStrategy(_mapper!, new OpenWeatherConfig(), new HttpClient());
        }

        [Test]
        public async Task ShouldGetDateForecast()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.DateForecast);
            result.DateForecast.Should().Be(_date.Date + new TimeSpan(11, 0, 0));
        }

        [Test]
        public async Task ShouldGetDate()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Date);
            result.Date.Should().Be(_date.Date);
        }

        [Test]
        public async Task ShouldGetTemperature()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Temperature);
            result.Temperature.Should()
                .BeGreaterThan(-100)
                .And
                .BeLessThan(100);
        }

        [Test]
        public async Task ShouldGetWindspeed()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Windspeed);
            result.Windspeed.Should().BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetWindDirection()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.WindDirection);
            result.WindDirection.Should()
                .BeGreaterThanOrEqualTo(0) // 0 = North, 90 = East, 180 = South, 270 = West
                .And
                .BeLessThanOrEqualTo(360);
        }

        [Test]
        public async Task ShouldGetWindspeedGust()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.WindspeedGust);
            result.WindspeedGust.Should().BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetPressure()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Pressure);
            result.Pressure.Should().BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetHumidity()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Humidity);
            result.Humidity.Should()
                .BeGreaterThanOrEqualTo(0)
                .And
                .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetProbabilityOfRain()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.ProbOfRain);
            result.ProbOfRain.Should()
                .BeGreaterThanOrEqualTo(0)
                .And
                .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetAmountOfRain()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.AmountRain);
            result.AmountRain.Should().BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public async Task ShouldGetCloudAreaFraction()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.CloudAreaFraction);
            result.CloudAreaFraction.Should()
                .BeGreaterThanOrEqualTo(0)
                .And
                .BeLessThanOrEqualTo(100);
        }

        [Test]
        public async Task ShouldGetWeatherType()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.WeatherType);
            result.WeatherType.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetWeatherSource()
        {
            var result = await _strategy!.GetWeatherDataFrom(_city, _date);

            Console.WriteLine(result.Source.DataProvider);
            result.Source!.DataProvider.Should().NotBeNullOrEmpty();
        }
    }
}
