using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests
{
    public class EndpointByDateLogicTest
    {
        private string? cityName;
        private DateTime date;
        private List<CityDto>? cities;
        private List<WeatherForecastDto>? dates;
        List<IGetWeatherDataStrategy<WeatherForecastDto>>? strategies;
        private IFactory? factory;

        [SetUp]
        public void Setup()
        {
            cities = new()
            {
                new CityDto { Id = 1, Name = "Stavanger", Country = "Norway", Latitude = 59.1020129, Longitude = 5.712611357275702 },
                new CityDto { Id = 2, Name = "Oslo", Country = "Norway", Latitude = 59.9133301, Longitude = 10.7389701 }
            };

            dates = new()
            {
                new WeatherForecastDto { Date = DateTime.Now },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(1) }
            };

            factory = new StrategyBuilderFactory(null);

            strategies = new();
            strategies.Add(factory.Build<IYrStrategy>());
            strategies.Add(factory.Build<IOpenWeatherStrategy>());
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseFuture()
        {
            // Arrange
            date = DateTime.Now.AddDays(2); // Checking for 
            cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);

            // Act
            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                Console.WriteLine("Adding new City to database");

                // Updateing cityquery
                Console.WriteLine("Updating cityquery");

                foreach (var strategy in strategies)
                {
                    // Assert
                    Console.WriteLine("Adding WeatherData to the city added");
                }

                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
            }

            if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                        $" and not in the database." +
                                            $" Adding weather forecast for given date: {date}");
                }
                result = "Entering the logic for date not in database but in the future.";
            }

            if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {date}");


                }
                result = $"Entering the logic for updating weather.";
            }

            Console.WriteLine($"\nGetting weather forecast for {cityName} from database.");

            result.Should().Be("Entering the logic for date not in database but in the future.");
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseHistorical()
        {
            // Arrange
            date = new DateTime(2022, 06, 07); // Set custom date <- should not be able to get data from back in time
            cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);

            // Act
            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                Console.WriteLine("Adding new City to database");

                // Updateing cityquery
                Console.WriteLine("Updateing cityquery");

                foreach (var strategy in strategies)
                {
                    // Assert
                    Console.WriteLine("Adding WeatherData to the city added");
                }

                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
            }

            if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($" {strategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                        $" and not in the database." +
                                            $" Adding weather forecast for given date: {date}");
                }
                result = "Entering the logic for date not in database but in the future.";
            }

            if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {date}");


                }
                result = $"Entering the logic for updating weather.";
            }

            Console.WriteLine($"\nGetting weather forecast for {cityName} from database.");

            result.Should().BeEmpty();
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateInDatabaseFuture()
        {
            // Arrange
            date = DateTime.Now.AddDays(1);
            cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);

            // Act
            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                Console.WriteLine("Adding new City to database");

                // Updateing cityquery
                Console.WriteLine("Updateing cityquery");

                foreach (var strategy in strategies)
                {
                    // Assert
                    Console.WriteLine("Adding WeatherData to the city added");
                }

                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
            }

            if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                        $" and not in the database." +
                                            $" Adding weather forecast for given date: {date}");
                }
                result = "Entering the logic for date not in database but in the future.";
            }

            if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
            {
                foreach (var strategy in strategies)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {date}");


                }
                result = $"Entering the logic for updating weather.";
            }

            Console.WriteLine($"\nGetting weather forecast for {cityName} from database.");

            result.Should().Be("Entering the logic for updating weather.");
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateInDatabaseHistorical()
        {
            // Arrange
            date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);

            // Act
            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                Console.WriteLine("Adding new City to database");

                // Updateing cityquery
                Console.WriteLine("Updateing cityquery");

                foreach (var strategy in strategies!)
                {
                    // Assert
                    Console.WriteLine("Adding WeatherData to the city added");
                }

                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
            }

            if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
            {
                foreach (var strategy in strategies!)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                        $" and not in the database." +
                                            $" Adding weather forecast for given date: {date}");
                }
                result = "Entering the logic for date not in database but in the future.";
            }

            if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
            {
                foreach (var strategy in strategies!)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {date}");


                }
                result = $"Entering the logic for updating weather.";
            }

            Console.WriteLine($"\nGetting weather forecast for {cityName} from database.");

            result.Should().BeEmpty();
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_CityNotInDatabase()
        {
            // Arrange
            date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            cityName = "Bergen";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            cityName = textInfo.ToTitleCase(cityName);

            // Act
            var result = string.Empty;


            // Checking if the city is in our database, if not it's getting added.
            if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
            {
                Console.WriteLine("Adding new City to database");

                // Updateing cityquery
                Console.WriteLine("Updateing cityquery");

                foreach (var strategy in strategies!)
                {
                    // Assert
                    Console.WriteLine($"Adding WeatherData to the city added given date: {date}");
                }

                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
            }

            if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
            {
                foreach (var strategy in strategies!)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                        $" and not in the database." +
                                            $" Adding weather forecast for given date: {date}");
                }
                result = "Entering the logic for date not in database but in the future.";
            }

            if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
            {
                foreach (var strategy in strategies!)
                {
                    var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                    // Assert
                    Console.WriteLine($"{strategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {date}");


                }
                result = $"Entering the logic for updating weather.";
            }

            Console.WriteLine($"\nGetting weather forecast for {cityName} from database.");

            result.Should().Be("Entering logic for City missing from database. Adding weather forecast for this city.");
        }

    }
}