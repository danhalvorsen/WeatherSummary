﻿using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherGetCityDataStrategyTest
    {
        
        private IGetCityDataStrategy<CityDto>? _strategy;
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;
        private readonly string _city = "Oslo";

        [SetUp]
        public void SetUp()
        {
            IServiceCollection servicecollection = new ServiceCollection();
            var assembly = new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") /*Assembly.GetExecutingAssembly() */}; //Assembly.LoadFrom("WeatherWebAPI.dll")
            servicecollection.AddAutoMapper(assembly);
            _serviceProvider = servicecollection.BuildServiceProvider();


            _mapper = _serviceProvider.GetService<IMapper>();

            _strategy = new OpenWeatherStrategy(_mapper!, new OpenWeatherConfig(), new HttpClient());
        }


        [Test]
        public async Task ShouldGetName()
        {
            var result = await _strategy!.GetCityDataFor(_city);

            Console.WriteLine(result[0].Name);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Name
                .Should()
                    .NotBeNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetCountry()
        {
            var result = await _strategy!.GetCityDataFor(_city);

            Console.WriteLine(result[0].Country);


            // Test for formatering av landskode
            Console.WriteLine(result[0].Country);
            var c = new CultureInfo(result[0].Country!);
            var r = new RegionInfo(c.Name);
            result[0].Country = r.EnglishName;

            Console.WriteLine(result[0].Country);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Country
                .Should()
                    .NotBeNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetLatitude()
        {
            var result = await _strategy!.GetCityDataFor(_city);

            Console.WriteLine(result[0].Latitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Latitude
                .Should()
                    .BeGreaterThanOrEqualTo(-90)
                    .And
                    .BeLessThanOrEqualTo(90);
        }

        [Test]
        public async Task ShouldGetLongitude()
        {
            var result = await _strategy!.GetCityDataFor(_city);

            Console.WriteLine(result[0].Longitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Longitude
                .Should()
                    .BeGreaterThanOrEqualTo(-180)
                    .And
                    .BeLessThanOrEqualTo(180);
        }

        [Test]
        public async Task ShouldGetAltitude()
        {
            var result = await _strategy!.GetCityDataFor(_city);

            Console.WriteLine(result[0].Altitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Altitude
                .Should()
                    .Be(0);
        }
    }
}
