using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherCityDataMapperTest // Not getting used because of GetSteamAsync.
    {
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;

        [SetUp]
        public void Setup()
        {
            IGetCityDataStrategy strategy = new OpenWeatherFetchCityStrategy(new OpenWeatherConfig(), new HttpClient());

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") });
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Test]
        public void ShouldMapName()
        {
            var name = "Stavanger";

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    name = name
                }
            };

            // Act
            var result = _mapper?.Map<CityDto>(application);


            //Assert
            Console.WriteLine(result?.Name);
            result?.Name.Should().Be(name);
        }

        [Test]
        public void ShouldMapCountry()
        {
            var country = "Norway";

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    country = country
                }
            };

            //Act
            var result = _mapper?.Map<CityDto>(application);

            // Assert
            Console.WriteLine(result?.Country);
            result?.Country.Should().Be(country);
        }

        [Test]
        public void ShouldMapLongitude()
        {
            var lon = 12.121212;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    lon = lon
                }
            };

            //Act
            var result = _mapper?.Map<CityDto>(application);

            // Assert
            Console.WriteLine(result?.Longitude);
            result?.Longitude.Should().Be(lon);
        }


        [Test]
        public void ShouldMapLatitude()
        {
            var lat = 12.121212;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    lat = lat
                }
            };

            //Act
            var result = _mapper?.Map<CityDto>(application);

            // Assert
            Console.WriteLine(result?.Latitude);
            result?.Latitude.Should().Be(lat);
        }
    }
}
