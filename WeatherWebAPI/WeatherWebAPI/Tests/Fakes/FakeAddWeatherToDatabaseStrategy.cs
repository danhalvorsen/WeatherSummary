using System.Threading.Tasks;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.Database;

namespace Tests.Fakes
{
    public class FakeAddWeatherToDatabaseStrategy : IAddWeatherDataToDatabaseStrategy
    {
        public Task Add(WeatherForecastDto weatherData, CityDto city)
        {
            return Task.FromResult(CreateTestData(weatherData, city));
        }

        private static WeatherForecastDto CreateTestData(WeatherForecastDto weatherData, CityDto city)
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
                WeatherType = weatherData.WeatherType,
                Source = new WeatherSourceDto()
            };
        }
    }
}
