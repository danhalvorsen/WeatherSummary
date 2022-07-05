using FluentAssertions;
using NUnit.Framework;
using System;
using WeatherWebAPI.Query;

namespace Tests.Endpoints.Properties
{
    public class EndpointPropertyValidationTest_BetweenDateQueryAndCity
    {
        private BetweenDateQueryAndCityValidator? _beetweenDateQueryAndCityValidator;
        private BetweenDateQueryAndCity? _query;

        private DateTime _dateNotEmptyOrNullLower;
        private DateTime _dateNotEmptyOrNullHigher;

        [SetUp]
        public void Setup()
        {
            _query = new BetweenDateQueryAndCity
            {
                BetweenDateQuery = new BetweenDateQuery
                {
                    From = DateTime.Now,
                    To = DateTime.Now.AddDays(1)
                },
                CityQuery = new CityQuery
                {
                    City = "Stavanger"
                }
            };

            _beetweenDateQueryAndCityValidator = new BetweenDateQueryAndCityValidator
            {

            };

            _dateNotEmptyOrNullHigher = DateTime.Now;
            _dateNotEmptyOrNullLower = new DateTime(2000, 1, 1);
        }

        [Test]
        public void DateFromIsEmptyOrNull()
        {
            _query!.BetweenDateQuery!.From = DateTime.MinValue; // Basically null??
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateFromIsNotEmptyOrNullButIsLowerThanLimit()
        {
            _query!.BetweenDateQuery!.From = _dateNotEmptyOrNullLower;
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateFromIsNotEmptyOrNullAndIsHigherThanLimit()
        {
            _query!.BetweenDateQuery!.From = _dateNotEmptyOrNullHigher;
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.ToString();

            result.Should().Be("");
        }

        [Test]
        public void DateToIsEmptyOrNull()
        {
            _query!.BetweenDateQuery!.To = DateTime.MinValue; // Basically null??
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateToIsNotEmptyOrNullButIsLowerThanLimit()
        {
            _query!.BetweenDateQuery!.To = _dateNotEmptyOrNullLower;
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }

        [Test]
        public void DateToIsNotEmptyOrNullAndIsHigherThanLimit()
        {
            _query!.BetweenDateQuery!.To = _dateNotEmptyOrNullHigher;
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.ToString();

            result.Should().Be("");
        }

        [Test]
        public void CityIsNotEmptyOrNull()
        {
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query!);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
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
            var validationResult = _beetweenDateQueryAndCityValidator?.Validate(_query);

            Console.WriteLine("Date From: " + _query?.BetweenDateQuery?.From);
            Console.WriteLine("Date To: " + _query?.BetweenDateQuery?.To);
            Console.WriteLine("City: " + _query?.CityQuery?.City);

            if (!validationResult!.IsValid)
                Console.WriteLine(validationResult);

            var result = validationResult.Errors;

            result.Should().NotBeNullOrEmpty(validationResult.Errors.ToString());
        }
    }
}
