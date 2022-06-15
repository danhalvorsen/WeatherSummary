using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tests.Fakes;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;

namespace Tests.Endpoints
{
    public class EndpointAddCityToDatabaseTest
    {
        private DateTime _date;

        private List<CityDto>? _cities;
        List<IGetWeatherDataStrategy<WeatherForecastDto>>? weatherDataStrategies;

        [SetUp]
        public void Setup()
        {
            _cities = new()
            {
                new CityDto { Id = 1, Name = "Stavanger", Country = "Norway", Latitude = 59.1020129, Longitude = 5.712611357275702 },
                new CityDto { Id = 2, Name = "Oslo", Country = "Norway", Latitude = 59.9133301, Longitude = 10.7389701 }
            };
            weatherDataStrategies = new();

            weatherDataStrategies.Add(new FakeYrStrategy());
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityNotInDatabaseAsync()
        {
            // Arrange
            _date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "Bergen";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            string cityName = textInfo.ToTitleCase(cityInput);

            // Act
            var result = 0;


            if (!CityExists(cityName))
            {
                //await GetCityAndAddToDatabase(cityName);
                //_citiesDatabase = await getCitiesQuery.GetAllCities();

                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityinfo = strategy.GetCityDataFor(cityName).Result;

                FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                var city = await addCityToDatabaseStrategy.Add(cityinfo);

                _cities.Add(city);
            }

            if (_cities.Count() > 2)
                result = 0;
            else
                result = -1;

            Console.WriteLine($"{_cities[2].Name} added to database.");
            Console.WriteLine($"\nGetting weather forecast for {_cities[2].Name} from database.");

            result.Should().Be(0);
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityInDatabaseAsync()
        {
            // Arrange
            _date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "Stavanger";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            string cityName = textInfo.ToTitleCase(cityInput);

            // Act
            var result = "";


            if (!CityExists(cityName))
            {
                //await GetCityAndAddToDatabase(cityName);
                //_citiesDatabase = await getCitiesQuery.GetAllCities();

                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityinfo = strategy.GetCityDataFor(cityName).Result;

                FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                var city = await addCityToDatabaseStrategy.Add(cityinfo);

                _cities.Add(city);
            }

            if (_cities.Count() == 2)
                result = $"City {cityName} in database";
            else
                result = $"City {cityName} not in database"; ;

            Console.WriteLine(result);

            result.Should().Be($"City {cityName} in database");
        }

        private bool CityExists(string cityName)
        {
            return _cities.ToList().Any(c => c.Name.Equals(cityName));
        }
    }
}
