using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebAPI.Query;

namespace Tests.Endpoints.Properties
{
    public class EndpointPropertyValidationTest_WeekQueryAndCity
    {
        private WeekQueryAndCityValidator? _weekQueryAndCityValidator;
        private WeekQueryAndCity? _query;
        private int weekOutsideRange;

        [SetUp]
        public void Setup()
        {
            _query = new WeekQueryAndCity
            {
                Week = new Random().Next(1,53), // Range 1-52
                CityQuery = new CityQuery
                {
                    City = "Stavanger"
                }
            };

            _weekQueryAndCityValidator = new WeekQueryAndCityValidator
            {
                
            };

            weekOutsideRange = 53;
        }

        [Test]
        public void WeekInRange()
        {
            var validationResult = _weekQueryAndCityValidator?.Validate(_query!);

            Console.WriteLine("Week: " + _query?.Week);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors.ToString();

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void WeekOutsideRange()
        {
            _query!.Week = weekOutsideRange;
            var validationResult = _weekQueryAndCityValidator?.Validate(_query!);

            Console.WriteLine("Week: " + _query?.Week);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors.ToString();

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void CityIsNotEmptyOrNull()
        {
            var validationResult = _weekQueryAndCityValidator?.Validate(_query!);

            Console.WriteLine("Week: " + _query?.Week);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors.ToString();

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void CityIsEmptyOrNull()
        {
            _query!.CityQuery!.City = "";
            var validationResult = _weekQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Week: " + _query?.Week);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }
    }
}
