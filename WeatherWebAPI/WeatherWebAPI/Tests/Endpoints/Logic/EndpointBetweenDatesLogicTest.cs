using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Fakes;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;

namespace Tests.Endpoints.Logic
{
    public class EndpointBetweenDatesLogicTest
    {
        private string? _cityName;
        private DateTime _fromDate;
        private DateTime _toDate;
        private int _weatherAdded;
        private int _weatherUpdated;
        private int _weatherDatabase;

        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? _dates;

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
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(-1) },
                new WeatherForecastDto { Date = DateTime.UtcNow },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(1) },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(3) },
                new WeatherForecastDto { Date = DateTime.UtcNow.AddDays(5) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 25, 12, 0, 0, DateTimeKind.Utc) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 28, 12, 0, 0, DateTimeKind.Utc) }
            };

            _weatherDatabase = 0;
            _weatherAdded = 0;
            _weatherUpdated = 0;

            _weatherDataStrategies = new();

            _weatherDataStrategies.Add(new FakeYrStrategy());
            _weatherDataStrategies.Add(new FakeOpenWeatherStrategy());
        }

        [Test]
        public async Task WeatherForecastBetweenDatesLogicEndpointTest_DatesInFutureSomeInDatabase()
        {
            // Arrange

            _cityName = "Stavanger";
            _fromDate = DateTime.UtcNow;
            _toDate = DateTime.UtcNow.AddDays(6);
            var datesQuery = new List<DateTime>();

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date >= DateTime.UtcNow.Date)
                {
                    foreach (var strategy in _weatherDataStrategies!)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                            // Add(WeatherForecastDto weatherData, CityDto city)
                            Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            var fakeUpdateWeather = await FakeUpdateWeatherToDatabaseStrategy.Update(weatherData, city, date);


                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                            _weatherUpdated++;
                        }
                    }
                }

                if (_dates!.ToList().Any(d => d.Date.Date.Equals(date.Date)))
                    _weatherDatabase++;
            }

            if (_weatherAdded + _weatherUpdated == datesQuery.Count * _weatherDataStrategies?.Count)
                result = datesQuery.Count;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase * _weatherDataStrategies?.Count}/{datesQuery.Count * _weatherDataStrategies?.Count} forecasts in database that were asked for.");

            result.Should().Be(datesQuery.Count);
        }

        [Test]
        public async Task WeatherForecastBetweenDatesLogicEndpointTest_DatesInDatabaseHistoricalAsync()
        {
            // Arrange

            _cityName = "Stavanger";
            _fromDate = new DateTime(2022, 05, 25, 0, 0, 0, DateTimeKind.Utc);
            _toDate = DateTime.UtcNow;
            var datesQuery = new List<DateTime>();


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = 0;


            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");

                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date >= DateTime.UtcNow.Date)
                {
                    foreach (var strategy in _weatherDataStrategies!)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                            // Add(WeatherForecastDto weatherData, CityDto city)
                            Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            var fakeUpdateWeather = await FakeUpdateWeatherToDatabaseStrategy.Update(weatherData, city, date);


                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                            _weatherUpdated++;
                        }
                    }
                }

                if (_dates!.ToList().Any(d => d.Date.Date.Equals(date.Date)))
                    _weatherDatabase++;
            }

            if (_weatherAdded > 0)
                result = -1;
            else
                result = _weatherDatabase;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase}/{datesQuery.Count * _weatherDataStrategies?.Count} forecasts in database that were asked for.");

            result.Should().Be(_weatherDatabase);
        }

        [Test]
        public async Task WeatherForecastBetweenDatesLogicEndpointTest_Everything_HistoricalWithDatasInFutureAndDatabase()
        {
            // Arrange

            _cityName = "Stavanger";
            _fromDate = DateTime.UtcNow.AddDays(-1);
            _toDate = DateTime.UtcNow.AddDays(6);
            var datesQuery = new List<DateTime>();

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date >= DateTime.UtcNow.Date)
                {
                    foreach (var strategy in _weatherDataStrategies!)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                            // Add(WeatherForecastDto weatherData, CityDto city)
                            Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            var fakeUpdateWeather = await FakeUpdateWeatherToDatabaseStrategy.Update(weatherData, city, date);


                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                            _weatherUpdated++;
                        }
                    }
                }
                if (_dates!.ToList().Any(d => d.Date.Date.Equals(date.Date)))
                    _weatherDatabase++;
            }


            if (_weatherAdded + _weatherDatabase * _weatherDataStrategies?.Count == datesQuery.Count * _weatherDataStrategies?.Count())
                result = datesQuery.Count;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase * _weatherDataStrategies?.Count}/{datesQuery.Count * _weatherDataStrategies?.Count} forecasts in database that were asked for.");

            result.Should().Be(datesQuery.Count);
        }


        [Test]
        public async Task WeatherForecastBetweenDatesLogicEndpointTest_DatesIsTheSame()
        {
            // Arrange

            _cityName = "Stavanger";
            _fromDate = DateTime.UtcNow;
            _toDate = DateTime.UtcNow;
            var datesQuery = new List<DateTime>();

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = 0;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date >= DateTime.UtcNow.Date)
                {
                    foreach (var strategy in _weatherDataStrategies!)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            var fakeAddWeather = await fakeAddWeatherDataToDatabaseStrategy.Add(weatherData, city);

                            // Add(WeatherForecastDto weatherData, CityDto city)
                            Console.WriteLine($"{weatherData.Date} -> {fakeAddWeather.Date} -- ADDED");
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date).Result;

                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            var fakeUpdateWeather = await FakeUpdateWeatherToDatabaseStrategy.Update(weatherData, city, date);


                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine($"{weatherData.Date} -> {fakeUpdateWeather.Date} -- UPDATED");
                            _weatherUpdated++;
                        }
                    }
                }

                if (_dates!.ToList().Any(d => d.Date.Date.Equals(date.Date)))
                    _weatherDatabase++;
            }

            if (datesQuery.Count == 1)
                result = datesQuery.Count;
            else
                result = -1;

            Console.WriteLine($"\nAdded {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database. " +
                $"\n{_weatherDatabase * _weatherDataStrategies?.Count}/{datesQuery.Count * _weatherDataStrategies?.Count} forecasts in database that were asked for.");

            result.Should().Be(datesQuery.Count());
        }

        protected static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
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
    }
}
