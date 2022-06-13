using System;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.Database;

namespace Tests.Fakes
{
    public class FakeUpdateWeatherToDatabaseStrategy : IUpdateWeatherDataToDatabaseStrategy
    {
        public Task Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
        {
            return Task.FromResult(CreateTestData(weatherData, city, dateToBeUpdated));
        }
        private static WeatherForecastDto CreateTestData(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
        {
                return new WeatherForecastDto
                {
                    Date = weatherData.Date,
                    Temperature = weatherData.Temperature,
                    Windspeed = weatherData.Windspeed,
                    WindDirection = weatherData.WindDirection,
                    WindspeedGust = weatherData.WindspeedGust,
                    Pressure = weatherData.Pressure,
                    Humidity = weatherData.Humidity,
                    ProbOfRain = weatherData.ProbOfRain,
                    AmountRain = weatherData.AmountRain,
                    CloudAreaFraction = weatherData.CloudAreaFraction,
                    FogAreaFraction = weatherData.FogAreaFraction,
                    ProbOfThunder = weatherData.ProbOfThunder,
                    City = city.Name,
                    WeatherType = "Date changed from: " + dateToBeUpdated.ToString(),
                    Source = new WeatherSourceDto()
                };
        }
    }
}
