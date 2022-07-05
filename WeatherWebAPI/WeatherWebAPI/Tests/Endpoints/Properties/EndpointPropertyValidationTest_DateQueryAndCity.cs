using FluentAssertions;
using NUnit.Framework;
using System;
using WeatherWebAPI.Query;

namespace Tests.Endpoints.Properties
{
    public class EndpointPropertyValidationTest_DateQueryAndCity
    {
        private DateQueryAndCityValidator? _dateQueryAndCityValidator;
        private DateQueryAndCity? _query;

        private DateTime _dateNotEmptyOrNullLower;
        private DateTime _dateNotEmptyOrNullHigher;

        [SetUp]
        public void Setup()
        {
            _query = new DateQueryAndCity
            {
                DateQuery = new DateQuery
                {
                    Date = DateTime.Now
                },
                CityQuery = new CityQuery
                {
                    City = "Stavanger"
                }
            };

            _dateQueryAndCityValidator = new DateQueryAndCityValidator
            {

            };

            _dateNotEmptyOrNullHigher = DateTime.Now;
            _dateNotEmptyOrNullLower = new DateTime(2000, 1, 1);
        }

        [Test]
        public void DateIsEmptyOrNull()
        {
            _query!.DateQuery!.Date = DateTime.MinValue; // Basically null??
            var validationResult = _dateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date: " + _query.DateQuery.Date);
            Console.WriteLine("City: " + _query.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateIsNotEmptyOrNullButIsLowerThanLimit()
        {
            _query!.DateQuery!.Date = _dateNotEmptyOrNullLower;
            var validationResult = _dateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date: " + _query.DateQuery.Date);
            Console.WriteLine("City: " + _query.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateIsNotEmptyOrNullAndIsHigherThanLimit()
        {
            _query!.DateQuery!.Date = _dateNotEmptyOrNullHigher;
            var validationResult = _dateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date: " + _query.DateQuery.Date);
            Console.WriteLine("City: " + _query.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.ToString();

            result.Should().Be("");
        }

        [Test]
        public void CityIsNotEmptyOrNull()
        {
            var validationResult = _dateQueryAndCityValidator?.Validate(_query!);

            Console.WriteLine("Date: " + _query?.DateQuery?.Date);
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
            var validationResult = _dateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date: " + _query.DateQuery?.Date);
            Console.WriteLine("City: " + _query.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }
    }
}
