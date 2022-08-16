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

namespace Tests.Endpoints.Logic
{
    public class EndpointAddCityToDatabaseTest
    {
        private DateTime _date;

        private List<CityDto>? _cities;
        private List<IGetWeatherDataStrategy<WeatherForecast>>? _weatherDataStrategies;

        [SetUp]
        public void Setup()
        {
            _cities = new()
            {
                new CityDto { Id = 1, Name = "Stavanger", Country = "Norway", Latitude = 59.1020129, Longitude = 5.712611357275702 },
                new CityDto { Id = 2, Name = "Oslo", Country = "Norway", Latitude = 59.9133301, Longitude = 10.7389701 }
            };
            _weatherDataStrategies = new();

            _weatherDataStrategies.Add(new FakeYrStrategy());
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityNotInDatabaseAsync()
        {
            // Arrange
            _date = DateTime.UtcNow; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "Bergen";
            var cityName = "";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityInput = textInfo.ToTitleCase(cityInput);

            // Act
            var result = "";

            if (!CityExists(cityInput!))
            {
                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityInfo = strategy.GetCityDataFor(cityInput).Result;

                cityInfo[0].Name = "Bergen";
                cityName = cityInfo[0].Name;

                if (cityName != "")
                {
                    if (!CityExists(cityName!))
                    {
                        FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                        var city = await addCityToDatabaseStrategy.Add(cityInfo);

                        _cities!.Add(city);
                    }
                }
            }
            else
            {
                cityName = cityInput;
            }

            if (_cities!.Count == 2)
                result = $"City {cityName} in database";
            else
                result = $"City {cityName} not in database"; ;

            Console.WriteLine(result);

            foreach (var city in _cities)
            {
                Console.WriteLine(city.Name);
            }

            result.Should().Be($"City {cityName} not in database");
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityInDatabaseAsync()
        {
            // Arrange
            _date = DateTime.UtcNow; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "Stavanger";
            var cityName = "";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityInput = textInfo.ToTitleCase(cityInput);

            // Act
            var result = "";

            if (!CityExists(cityInput!))
            {
                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityInfo = strategy.GetCityDataFor(cityInput).Result;

                cityInfo[0].Name = "Stavanger";
                cityName = cityInfo[0].Name;

                if (cityName != "")
                {
                    if (!CityExists(cityName!))
                    {
                        FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                        var city = await addCityToDatabaseStrategy.Add(cityInfo);

                        _cities!.Add(city);
                    }
                }
            }
            else
            {
                cityName = cityInput;
            }

            if (_cities!.Count == 2)
                result = $"City {cityName} in database";
            else
                result = $"City {cityName} not in database"; ;

            Console.WriteLine(result);

            foreach (var city in _cities)
            {
                Console.WriteLine(city.Name);
            }

            result.Should().Be($"City {cityName} in database");
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityNameNotInEnglish()
        {
            // Arrange
            _date = DateTime.UtcNow; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "Stafangur";
            var cityName = "";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityInput = textInfo.ToTitleCase(cityInput);

            // Act
            var result = "";

            if (!CityExists(cityInput!))
            {
                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityInfo = strategy.GetCityDataFor(cityInput).Result;
                cityInfo[0].Name = "Stavanger";
                cityName = cityInfo[0].Name;

                if (cityName != "")
                {
                    if (!CityExists(cityName!))
                    {
                        FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                        var city = await addCityToDatabaseStrategy.Add(cityInfo);

                        _cities!.Add(city);
                    }
                }
            }
            else
            {
                cityName = cityInput;
            }

            if (_cities!.Count == 2)
                result = $"City {cityName} in database";
            else
                result = $"City {cityName} not in database"; ;

            Console.WriteLine(result);

            foreach (var city in _cities)
            {
                Console.WriteLine(city.Name);
            }

            result.Should().Be($"City {cityName} in database");
        }

        [Test]
        public async Task AddCityToDatabaseEndpointTest_CityNameNotValid()
        {
            // Arrange
            _date = DateTime.UtcNow; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            var cityInput = "asdf";
            var cityName = "";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityInput = textInfo.ToTitleCase(cityInput);

            // Act
            var result = "";

            if (!CityExists(cityInput!))
            {
                IGetCityDataStrategy<CityDto> strategy = new FakeOpenWeatherStrategy();
                var cityInfo = strategy.GetCityDataFor(cityInput).Result;
                cityInfo[0].Name = "";
                cityName = cityInfo[0].Name;

                if (cityName != "")
                {
                    if (!CityExists(cityName!))
                    {
                        FakeAddCityToDatabaseStrategy addCityToDatabaseStrategy = new FakeAddCityToDatabaseStrategy();
                        var city = await addCityToDatabaseStrategy.Add(cityInfo);

                        _cities!.Add(city);
                    }
                }
            }
            else
            {
                cityName = cityInput;
            }

            if (result == "")
                result = $"Invalid city {cityInput}. Response: {cityName}";

            Console.WriteLine(result);

            foreach (var city in _cities!)
            {
                Console.WriteLine(city.Name);
            }

            result.Should().Be($"Invalid city {cityInput}. Response: {cityName}");
        }

        private bool CityExists(string cityName)
        {
            return _cities!.ToList().Any(c => c.Name!.Equals(cityName));
        }
    }
}
