using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherWebAPI.Automapper;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests.Fakes
{
    public class FakeYrStrategy : IGetWeatherDataStrategy<WeatherForecast.WeatherData>, IYrStrategy
    {
        public const int YEAR = 2002;
        public const int MONTH = 02;
        public const int DAY = 02;
        public const int TEMP = 12;
        public const int WIND_SPEED = 11;
        public const int WIND_DIRECTION = 10;
        public const int WIND_SPEED_GUST = 9;
        public const int PRESSURE = 8;
        public const int HUMIDITY = 7;
        public const int PROB_OF_RAIN = 6;
        public const int AMOUNT_RAIN = 5;
        public const int CLOUD_AREA_FRACTION = 4;
        public const int FOG_AREA_FRACTION = 3;
        public const int PROB_OF_THUNDER = 2;
        private const string STAVANGER = "Stavanger";
        private const string OSLO = "Oslo";
        private const string WEATHERTYPE = "sunny";

        public Task<WeatherForecast.WeatherData> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        {
            return Task.FromResult(CreateTestData(city.Name!, queryDate));
        }

        private static WeatherForecast.WeatherData CreateTestData(string cityName, DateTime date)
        {
            if (cityName == STAVANGER || cityName == OSLO)
            {
                return new WeatherForecast.WeatherData
                {
                    Date = date,
                    Temperature = TEMP,
                    Windspeed = WIND_SPEED,
                    WindDirection = WIND_DIRECTION,
                    WindspeedGust = WIND_SPEED_GUST,
                    Pressure = PRESSURE,
                    Humidity = HUMIDITY,
                    ProbOfRain = PROB_OF_RAIN,
                    AmountRain = AMOUNT_RAIN,
                    CloudAreaFraction = CLOUD_AREA_FRACTION,
                    FogAreaFraction = FOG_AREA_FRACTION,
                    ProbOfThunder = PROB_OF_THUNDER,
                    City = cityName,
                    WeatherType = WEATHERTYPE,
                    Source = new WeatherSourceDto { DataProvider = "Yr" }
                };
            }
            throw new ArgumentException("City name doesn't match test constants");
        }

        public string GetDataSource()
        {
            return "Yr";
        }
    }
}
