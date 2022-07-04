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
    public class EndpointByDateLogicTest
    {
        private string? _cityName;
        private DateTime _date;
        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? _dates;
        private IFactory? _factory;
        private int _weatherAdded;
        private int _weatherUpdated;
        private int _weatherDatabase;

        private List<IGetWeatherDataStrategy<WeatherForecastDto>>? _weatherDataStrategies;

        [SetUp]
        public void Setup()
        {
            _cities = new()
            {
                new CityDto { Id = 1, Name = "Stavanger", Country = "Norway", Latitude = 59.1020129, Longitude = 5.712611357275702 },
                new CityDto { Id = 2, Name = "Oslo", Country = "Norway", Latitude = 59.9133301, Longitude = 10.7389701 }
            };

            _dates = new()
            {
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(-1), Source = new WeatherSourceDto { DataProvider = "Yr" } },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(-1), Source = new WeatherSourceDto { DataProvider = "OpenWeather" } },
                new WeatherForecastDto { Date = DateTime.UtcNow, Source = new WeatherSourceDto { DataProvider = "Yr" } },
                new WeatherForecastDto { Date = DateTime.UtcNow, Source = new WeatherSourceDto { DataProvider = "OpenWeather" } },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(1), Source = new WeatherSourceDto { DataProvider = "Yr" } },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(1), Source = new WeatherSourceDto { DataProvider = "OpenWeather" } },
            };

            _weatherDatabase = 0;
            _weatherAdded = 0;
            _weatherUpdated = 0;

            _factory = new StrategyBuilderFactory(null!);
            _weatherDataStrategies = new();

            _weatherDataStrategies.Add(new FakeYrStrategy());
            _weatherDataStrategies.Add(new FakeOpenWeatherStrategy());
        }

        [Test]
        public async Task WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseButInFutureAsync()
        {
            // Arrange
            _date = DateTime.UtcNow.AddDays(2); // Checking for 
            _cityName = "Stavanger";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");
                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            if (_date >= DateTime.UtcNow.Date)
            {
                var city = GetCityDtoBy(_cityName);

                foreach (var strategy in _weatherDataStrategies!)
                {
                    //_dates = GetDatesForCity(city.Name, strategy);

                    if (GetWeatherDataBy(_date))
                    { 
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                        var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                        // Add(WeatherForecastDto weatherData, CityDto city)
                        Console.WriteLine($"{weatherData.Source.DataProvider} {weatherData.Date} -> {fakeAddWeather.Source.DataProvider} {fakeAddWeather.Date} -- ADDED");
                        _weatherAdded++;
                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                        var fakeUpdateWeather = await fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData, city, _date);


                        // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                        Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                        _weatherUpdated++;
                    }

                    //if (_dates.ToList().Any(d => d.Date.Date.Equals(_date.Date)))
                    //    _weatherDatabase++;
                }
            }
            else
            {
                foreach (var strategy in _weatherDataStrategies!)
                    if (_dates!.ToList().Any(d => d.Date.Date.Equals(_date.Date)))
                        _weatherDatabase++;
            }

            if (_weatherAdded > 0)
                result = _weatherAdded;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase}/{_dates!.Count} forecasts in database that were asked for.");

            result.Should().Be(_weatherAdded);
        }

        [Test]
        public async Task WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseHistoricalAsync()
        {
            // Arrange
            _date = DateTime.UtcNow.AddDays(-2);
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }

            if (_date >= DateTime.UtcNow.Date)
            {
                var city = GetCityDtoBy(_cityName);

                foreach (var strategy in _weatherDataStrategies!)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                        var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                        // Add(WeatherForecastDto weatherData, CityDto city)
                        Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                        _weatherAdded++;
                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                        var fakeUpdateWeather = await fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData, city, _date);


                        // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                        Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                        _weatherUpdated++;
                    }
                    
                    //if (_dates.ToList().Any(d => d.Date.Date.Equals(_date.Date)))
                    //    _weatherDatabase++;
                }
            }
            else
            {
                foreach (var strategy in _weatherDataStrategies!)
                    if (_dates!.ToList().Any(d => d.Date.Date.Equals(_date.Date)))
                        _weatherDatabase++;
            }

            if (_weatherDatabase == 0)
                result = _weatherDatabase;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase}/{_dates!.Count} forecasts in database that were asked for.");

            result.Should().Be(_weatherDatabase);
        }

        [Test]
        public async Task WeatherForecastByDateLogicEndpointTest_DateInDatabaseFutureAsync()
        {
            // Arrange
            _date = DateTime.UtcNow.AddDays(1);
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }

            if (_date >= DateTime.UtcNow.Date)
            {
                var city = GetCityDtoBy(_cityName);

                foreach (var strategy in _weatherDataStrategies!)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                        var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                        // Add(WeatherForecastDto weatherData, CityDto city)
                        Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                        _weatherAdded++;
                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                        var fakeUpdateWeather = await fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData, city, _date);


                        // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                        Console.WriteLine($"{weatherData.Source.DataProvider} {weatherData.Date} -> {fakeUpdateWeather.Source.DataProvider} {fakeUpdateWeather.Date} -- UPDATED");
                        _weatherUpdated++;
                        _weatherDatabase++;
                    }
                }
            }

            if (_weatherUpdated > 0)
                result = _weatherUpdated;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase}/{_dates!.Count} forecasts in database that were asked for.");

            result.Should().Be(_weatherUpdated);
        }

        [Test]
        public async Task WeatherForecastByDateLogicEndpointTest_DateInDatabaseHistoricalAsync()
        {
            // Arrange
            _date = DateTime.UtcNow.AddDays(-1);
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }

            if (_date >= DateTime.UtcNow.Date)
            {
                var city = GetCityDtoBy(_cityName);

                foreach (var strategy in _weatherDataStrategies!)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                        var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                        // Add(WeatherForecastDto weatherData, CityDto city)
                        Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                        _weatherAdded++;
                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        var weatherData = strategy.GetWeatherDataFrom(city, _date).Result;

                        var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                        var fakeUpdateWeather = await fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData, city, _date);


                        // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                        Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                        _weatherUpdated++;
                    }
                }
            }
            else
            {
                foreach (var strategy in _weatherDataStrategies!)
                    if (_dates!.ToList().Any(d => d.Date.Date.Equals(_date.Date)))
                        _weatherDatabase++;
            }

            if (_weatherDatabase == 2)
                result = _weatherDatabase;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase}/{_dates!.Count} forecasts in database that were asked for.");

            result.Should().Be(_weatherDatabase);
        }

        private bool CityExists(string cityName)
        {
            return _cities!.ToList().Any(c => c.Name!.Equals(cityName));
        }

        private CityDto GetCityDtoBy(string cityName)
        {
            return _cities!.Where(c => c.Name!.Equals(cityName)).First();
        }

        private bool UpdateWeatherDataBy(DateTime date) // DateExists(DateTime date)
        {
            return _dates!.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        private bool GetWeatherDataBy(DateTime date) // !DateExists(DateTime date)
        {
            return !_dates!.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        public List<WeatherForecastDto>? GetDatesForCity(string cityName, IGetWeatherDataStrategy<WeatherForecastDto> strategy)
        {
            return (List<WeatherForecastDto>?)_dates!.ToList().Where(d => d.City!.Equals(cityName) && d.Source.Equals(strategy.GetDataSource()));
        }
    }
}