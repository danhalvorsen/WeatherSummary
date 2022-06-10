using System;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests
{
    public class FakeYrStrategy : IGetWeatherDataStrategy<WeatherForecastDto>, IYrStrategy
    {
        public const int Year = 2002;
        public const int Month = 02;
        public const int Day = 02;
        public const int Temp = 12;
        public const int Windspeed = 11;
        public const int WindDir = 10;
        public const int WindSpdGust = 9;
        public const int Pressure = 8;
        public const int Humidity = 7;
        public const int ProbRain = 6;
        public const int AmountRain = 5;
        public const int CloudAreaFraction = 4;
        public const int FogAreaFraction = 3;
        public const int ProbOfThunder = 2;
        private const string STAVANGER = "Stavanger";
        private const string OSLO = "Oslo";
        private const string WEATHERTYPE = "sunny";

        public Task<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            return Task.FromResult(CreateTestData(city.Name));
        }

        private static WeatherForecastDto CreateTestData(string cityName)
        {
            if (cityName == STAVANGER || cityName == OSLO)
            {
                return new WeatherForecastDto
                {
                    Date = new DateTime(Year, Month, Day),
                    Temperature = Temp,
                    Windspeed = Windspeed,
                    WindDirection = WindDir,
                    WindspeedGust = WindSpdGust,
                    Pressure = Pressure,
                    Humidity = Humidity,
                    ProbOfRain = ProbRain,
                    AmountRain = AmountRain,
                    CloudAreaFraction = CloudAreaFraction,
                    FogAreaFraction = FogAreaFraction,
                    ProbOfThunder = ProbOfThunder,
                    City = cityName,
                    WeatherType = WEATHERTYPE,
                    Source = new WeatherSourceDto()
                };
            }
            throw new ArgumentException("City name doesn't match test constants");
        }
    }
}
