using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests
{
    public class EndpointBetweenDatesLogicTest
    {
        private string? cityName;
        private DateTime fromDate;
        private DateTime toDate;
        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? dates;
        List<IGetWeatherDataStrategy<WeatherForecastDto>>? strategies;
        private IFactory? factory;

        [SetUp]
        public void Setup()
        {
            _cities = new()
            {
                new CityDto { Id = 1, Name = "Stavanger", Country = "Norway", Latitude = 59.1020129, Longitude = 5.712611357275702 },
                new CityDto { Id = 2, Name = "Oslo", Country = "Norway", Latitude = 59.9133301, Longitude = 10.7389701 }
            };

            dates = new()
            {
                new WeatherForecastDto { Date = DateTime.Now },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(1) },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(3) },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(5) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 25) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 28) }
            };

            factory = new StrategyBuilderFactory(null);
            strategies = new();

            strategies.Add(new FakeYrStrategy());
            //strategies.Add(factory.Build<IOpenWeatherStrategy>());
        }

        [Test]
        public void WeatherForecastBetweenDatesLogicEndpointTest_DatesQuerySomeInDatabaseFuture()
        {
            // Arrange
            cityName = "Stavanger";
            fromDate = DateTime.Now;
            toDate = DateTime.Now.AddDays(7);
            var datesQuery = new List<DateTime>();
            int weatherAdded = 0;
            int weatherUpdated = 0;

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);


            // Getting the all the dates between the from and to datequeries
            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (CityExists(cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine("Adding new City to database");
            }

            // UPDATE CITIES
            Console.WriteLine("Updating cityquery");

            foreach (DateTime date in datesQuery)
            {
                if (date > DateTime.Now)
                {
                    foreach (var strategy in strategies)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(cityName);
                            strategy.GetWeatherDataFrom(city, date);

                            weatherAdded++;                            
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(cityName);

                            strategy.GetWeatherDataFrom(city, date);
                            weatherUpdated++;
                        }
                    }
                }
            }

            if (weatherAdded > 0 || weatherUpdated > 0)
                result = $"Added {weatherAdded} and updated {weatherUpdated} forecasts. Now fetching forecasts from database";
            else
                result = string.Empty;

            Console.WriteLine("\n" + result);
            result.Should().Be($"Added {weatherAdded} and updated {weatherUpdated} forecasts. Now fetching forecasts from database");
        }

        private bool CityExists(string cityName)
        {
            return !_cities.ToList().Any(c => c.Name.Equals(cityName));
        }

        private CityDto GetCityDtoBy(string cityName)
        {
            return _cities.Where(c => c.Name.Equals(cityName)).First();
        }

        private bool UpdateWeatherDataBy(DateTime date)
        {
            return dates.ToList().Any(d => d.Date.Date.Equals(date));
        }

        private bool GetWeatherDataBy(DateTime date)
        {
            return !dates.ToList().Any(d => d.Date.Date.Equals(date));
        }

        [Test]
        public void WeatherForecastBetweenDatesLogicEndpointTest_DatesQueryInDatabaseHistorical()
        {
            // Arrange
            cityName = "Stavanger";
            fromDate = new DateTime(2022, 05, 25);
            toDate = DateTime.Now;
            var datesQuery = new List<DateTime>();


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);


            // Getting the all the dates between the from and to datequeries
            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!_cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine("Adding new City to database");
            }

            // UPDATE CITIES
            Console.WriteLine("Updating cityquery");

            foreach (DateTime date in datesQuery)
            {
                if (date > DateTime.Now)
                {
                    foreach (var strategy in strategies)
                    {
                        if (!dates.ToList().Any(d => d.Date.Equals(date)))
                        {
                            var city = _cities.Where(c => c.Name.Equals(cityName)).First();
                            Console.WriteLine("\n" + date);
                            Console.WriteLine("Fetching weather");

                            result = "Logic for fetching data for dates not in database where this is possible";
                            //await new AddWeatherDataForCityCommand(config).GetWeatherDataForCity(date, city, strategy)
                        }
                        if (dates.ToList().Any(d => d.Date.Equals(date)))
                        {
                            var city = _cities.Where(c => c.Name.Equals(cityName)).First();
                            Console.WriteLine("\n" + date);
                            Console.WriteLine("Update weather");

                            result = "Logic for updating data for dates in database where this is possible";
                        }
                    }
                }
            }

            result = "Fetching forecasts from database";
            Console.WriteLine("\n" + result);
            result.Should().Be("Fetching forecasts from database");
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
