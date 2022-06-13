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
        private string? _cityName;
        private DateTime _date;
        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? _dates;
        private IFactory? _factory;

        List<IGetWeatherDataStrategy<WeatherForecastDto>>? weatherDataStrategies;
        

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
                new WeatherForecastDto { Date = DateTime.Now },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(1) }
            };

            _factory = new StrategyBuilderFactory(null);

            weatherDataStrategies = new();
            weatherDataStrategies.Add(_factory.Build<IYrStrategy>());
            weatherDataStrategies.Add(_factory.Build<IOpenWeatherStrategy>());
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseButInFuture()
        {
            // Arrange
            _date = DateTime.Now.AddDays(2); // Checking for 
            _cityName = "Stavanger";

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName);
                //_cities = await getCitiesQuery.GetAllCities();
                Console.WriteLine($"Adding new city: {_cityName} to database");
                result = "Entering logic for City missing from database. Adding weather forecast for this city.";

                // Updateing cityquery
                Console.WriteLine("Updating cityquery");
            }

            if (_date > DateTime.Now)
            {
                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                            $" and not in the database." +
                                                $" Adding weather forecast for given date: {_date}");

                        result = "Entering the logic for date not in database but in the future.";

                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {_date}");
                        result = "Entering the logic for updating weather.";
                    }
                }
            }

            Console.WriteLine($"\nGetting weather forecast for {_cityName} from database.");

            result.Should().Be("Entering the logic for date not in database but in the future.");
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateNotInDatabaseHistorical()
        {
            // Arrange
            _date = new DateTime(2022, 06, 07); // Set custom date <- should not be able to get data from back in time
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName);
                //_cities = await getCitiesQuery.GetAllCities();
                Console.WriteLine($"Adding new city: {_cityName} to database");
                result = "Entering logic for City missing from database. Adding weather forecast for this city.";

                // Updateing cityquery
                Console.WriteLine("Updating cityquery");
            }

            if (_date > DateTime.Now)
            {
                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                            $" and not in the database." +
                                                $" Adding weather forecast for given date: {_date}");

                        result = "Entering the logic for date not in database but in the future.";

                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {_date}");
                        result = "Entering the logic for updating weather.";
                    }
                }
            }

            Console.WriteLine($"\nGetting weather forecast for {_cityName} from database.");

            result.Should().BeEmpty();
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateInDatabaseFuture()
        {
            // Arrange
            _date = DateTime.Now.AddDays(1);
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName);
                //_cities = await getCitiesQuery.GetAllCities();
                Console.WriteLine($"Adding new city: {_cityName} to database");
                result = "Entering logic for City missing from database. Adding weather forecast for this city.";

                // Updateing cityquery
                Console.WriteLine("Updating cityquery");
            }

            if (_date > DateTime.Now)
            {
                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                            $" and not in the database." +
                                                $" Adding weather forecast for given date: {_date}");

                        result = "Entering the logic for date not in database but in the future.";

                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {_date}");
                        result = "Entering the logic for updating weather.";
                    }
                }
            }

            Console.WriteLine($"\nGetting weather forecast for {_cityName} from database.");

            result.Should().Be("Entering the logic for date not in database but in the future.");
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_DateInDatabaseHistorical()
        {
            // Arrange
            _date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            _cityName = "Stavanger";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName);
                //_cities = await getCitiesQuery.GetAllCities();
                Console.WriteLine($"Adding new city: {_cityName} to database");
                result = "Entering logic for City missing from database. Adding weather forecast for this city.";

                // Updateing cityquery
                Console.WriteLine("Updating cityquery");
            }

            if (_date > DateTime.Now)
            {
                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    if (GetWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
                                            $" and not in the database." +
                                                $" Adding weather forecast for given date: {_date}");

                        result = "Entering the logic for date not in database but in the future.";

                    }
                    if (UpdateWeatherDataBy(_date))
                    {
                        //var city = GetCityDtoBy(cityName);
                        //await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);

                        var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

                        // Assert
                        Console.WriteLine($"{weatherStrategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {_date}");
                        result = "Entering the logic for updating weather.";
                    }
                }
            }

            Console.WriteLine($"\nGetting weather forecast for {_cityName} from database.");

            result.Should().BeEmpty();
        }

        [Test]
        public void WeatherForecastByDateLogicEndpointTest_CityNotInDatabase()
        {
            // Arrange
            _date = DateTime.Now; // The check is for DateTime.Now < Date.Date, this will give the same outcome as if the date were from back in time.
            _cityName = "Bergen";


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);

            // Act
            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName);
                //_cities = await getCitiesQuery.GetAllCities();
                Console.WriteLine($"Adding new city: {_cityName} to database");
                result = "Entering logic for City missing from database. Adding weather forecast for this city.";
                
                // Updateing cityquery
                Console.WriteLine("Updating cityquery");
            }

            //if (_date > DateTime.Now)
            //{
            //    foreach (var weatherStrategy in weatherDataStrategies)
            //    {
            //        if (GetWeatherDataBy(_date))
            //        {
            //            //var city = GetCityDtoBy(cityName);
            //            //await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

            //            var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

            //            // Assert
            //            Console.WriteLine($"{weatherStrategy.GetType().Name} -- If the date is in the future (bigger than DateTime.Now)" +
            //                                $" and not in the database." +
            //                                    $" Adding weather forecast for given date: {_date}");

            //            result = "Entering the logic for date not in database but in the future.";

            //        }
            //        if (UpdateWeatherDataBy(_date))
            //        {
            //            //var city = GetCityDtoBy(cityName);
            //            //await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);

            //            var city = _cities.ToList().Where(c => c.Name.Equals(_cityName)).First();

            //            // Assert
            //            Console.WriteLine($"{weatherStrategy.GetType().Name} -- Updating {city.Name} with new WeatherForecast for given date: {_date}");
            //            result = "Entering the logic for updating weather.";
            //        }
            //    }
            //}

            Console.WriteLine($"\nGetting weather forecast for {_cityName} from database.");

            result.Should().Be("Entering logic for City missing from database. Adding weather forecast for this city.");
        }

        private bool CityExists(string cityName)
        {
            return _cities.ToList().Any(c => c.Name.Equals(cityName));
        }

        private CityDto GetCityDtoBy(string cityName)
        {
            return _cities.Where(c => c.Name.Equals(cityName)).First();
        }

        private bool UpdateWeatherDataBy(DateTime date)
        {
            return _dates.ToList().Any(d => d.Date.Date.Equals(date));
        }

        private bool GetWeatherDataBy(DateTime date)
        {
            return !_dates.ToList().Any(d => d.Date.Date.Equals(date));
        }
    }
}