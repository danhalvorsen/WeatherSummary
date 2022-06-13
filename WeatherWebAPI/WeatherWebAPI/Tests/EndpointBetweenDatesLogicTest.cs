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

namespace Tests
{
    public class EndpointBetweenDatesLogicTest
    {
        private string? _cityName;
        private DateTime _fromDate;
        private DateTime _toDate;
        private int _weatherAdded = 0;
        private int _weatherUpdated = 0;
        private IFactory? _factory;
        private TimeSpan ts = new TimeSpan(DateTime.Now.Hour - 1, 0, 0);
        
        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? _dates;

        List<IGetWeatherDataStrategy<WeatherForecastDto>>? strategies;
        
        

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
                new WeatherForecastDto { Date = DateTime.Now - ts }, // <- Skal være en time bak DateTime.Now
                new WeatherForecastDto { Date = DateTime.Now },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(1) },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(3) },
                new WeatherForecastDto { Date = DateTime.Now.AddDays(5) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 25) },
                new WeatherForecastDto { Date = new DateTime(2022, 05, 28) }
            };

            _factory = new StrategyBuilderFactory(null);
            strategies = new();

            strategies.Add(new FakeYrStrategy());
            //strategies.Add(factory.Build<IOpenWeatherStrategy>());
        }

        [Test]
        public void WeatherForecastBetweenDatesLogicEndpointTest_DatesQuerySomeInDatabaseFutureAsync()
        {
            // Arrange
            
            _cityName = "Stavanger";
            _fromDate = DateTime.Now;
            _toDate = DateTime.Now.AddDays(6);
            var datesQuery = new List<DateTime>();

            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = string.Empty;

            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");


                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date > DateTime.Now)
                {
                    foreach (var strategy in strategies)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date);

                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            var fakeAddWeather = fakeAddWeatherDataToDatabaseStrategy.Add(weatherData.Result, city);

                            // Add(WeatherForecastDto weatherData, CityDto city)
                            Console.WriteLine(weatherData.Result);
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date);
                            
                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            var fakeUpdateWeather = fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData.Result, city, date.Date);

                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine(fakeUpdateWeather);
                            _weatherUpdated++;
                        }
                    }
                }
            }

            if (_weatherAdded > 0 || _weatherUpdated > 0)
                result = $"Added {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database";
            else
                result = string.Empty;

            Console.WriteLine("\n" + result);
            result.Should().Be($"Added {_weatherAdded} and updated {_weatherUpdated} forecasts. Now fetching forecasts from database");
        }

        [Test]
        public void WeatherForecastBetweenDatesLogicEndpointTest_DatesQueryInDatabaseHistorical()
        {
            // Arrange
            
            _cityName = "Stavanger";
            _fromDate = new DateTime(2022, 05, 25);
            _toDate = DateTime.Now;
            var datesQuery = new List<DateTime>();


            // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
            TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
            _cityName = textInfo.ToTitleCase(_cityName);


            foreach (DateTime day in EachDay(_fromDate, _toDate))
            {
                datesQuery.Add(day);
            }

            // Act

            var result = string.Empty;


            if (!CityExists(_cityName))
            {
                //await(new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                Console.WriteLine($"Adding new City: {_cityName} to database");

                // UPDATE CITIES
                Console.WriteLine("Updating cityquery");
            }


            foreach (DateTime date in datesQuery)
            {
                if (date > DateTime.Now)
                {
                    foreach (var strategy in strategies)
                    {
                        if (GetWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData =  strategy.GetWeatherDataFrom(city, date);
                            var fakeAddWeatherDataToDatabaseStrategy = new FakeAddWeatherToDatabaseStrategy();
                            fakeAddWeatherDataToDatabaseStrategy.Add(weatherData.Result, city);
                            
                            // Add(WeatherForecastDto weatherData, CityDto city)
                           
                            Console.WriteLine(weatherData.Result);
                            _weatherAdded++;
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            var city = GetCityDtoBy(_cityName);
                            var weatherData = strategy.GetWeatherDataFrom(city, date);

                            var fakeUpdateWeatherDataToDatabaseStrategy = new FakeUpdateWeatherToDatabaseStrategy();
                            fakeUpdateWeatherDataToDatabaseStrategy.Update(weatherData.Result, city, date.Date);

                            
                            // Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
                            Console.WriteLine(weatherData.Result);
                            _weatherUpdated++;
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
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
        }

        private bool CityExists(string cityName)
        {
            return _cities.ToList().Any(c => c.Name.Equals(cityName));
        }

        private CityDto GetCityDtoBy(string cityName)
        {
            return _cities.Where(c => c.Name.Equals(cityName)).First();
        }

        private bool UpdateWeatherDataBy(DateTime date) // DateExists(DateTime date)
        {
            return _dates.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        private bool GetWeatherDataBy(DateTime date) // !DateExists(DateTime date)
        {
            return !_dates.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }
    }
}
