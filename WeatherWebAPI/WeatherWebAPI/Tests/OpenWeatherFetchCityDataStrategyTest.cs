 
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicWebAPI.Factory;

namespace TestProject
{
    public class OpenWeatherFetchCityDataStrategyTest
    {
        private GetWeatherDataFactory _factory = new GetWeatherDataFactory();
        private IWeatherDataStrategy _strategy = new OpenWeatherStrategy();
        private string _city = "Oslo";


        [Test]
        public async Task ShouldGetName()
        {
            var result = await _factory.GetCityDataFrom(_city, _strategy);

            Console.WriteLine(result[0].Name);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Name
                .Should()
                    .NotBeNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetCountry()
        {
            var result = await _factory.GetCityDataFrom(_city, _strategy);

            Console.WriteLine(result[0].Country);


            // Test for formatering av landskode
            Console.WriteLine(result[0].Country);
            var c = new CultureInfo(result[0].Country);
            var r = new RegionInfo(c.Name);
            result[0].Country = r.EnglishName;

            Console.WriteLine(result[0].Country);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Country
                .Should()
                    .NotBeNullOrEmpty();
        }

        [Test]
        public async Task ShouldGetLatitude()
        {
            var result = await _factory.GetCityDataFrom(_city, _strategy);

            Console.WriteLine(result[0].Latitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Latitude
                .Should()
                    .BeGreaterThanOrEqualTo(-90)
                    .And
                    .BeLessThanOrEqualTo(90);
        }

        [Test]
        public async Task ShouldGetLongitude()
        {
            var result = await _factory.GetCityDataFrom(_city, _strategy);

            Console.WriteLine(result[0].Longitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Longitude
                .Should()
                    .BeGreaterThanOrEqualTo(-180)
                    .And
                    .BeLessThanOrEqualTo(180);
        }

        [Test]
        public async Task ShouldGetAltitude()
        {
            var result = await _factory.GetCityDataFrom(_city, _strategy);

            Console.WriteLine(result[0].Altitude);

            // If we use streamAsync and list.
            //---------------------------------
            result[0].Altitude
                .Should()
                    .Be(0);
        }
    }
}
