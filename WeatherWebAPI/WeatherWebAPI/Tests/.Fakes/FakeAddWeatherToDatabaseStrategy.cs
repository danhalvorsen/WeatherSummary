using System.Threading.Tasks;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace Tests.Fakes
{
    public class FakeAddWeatherToDatabaseStrategy /*: IAddWeatherDataToDatabaseStrategy*/
    {
        public async Task<WeatherForecast> Add(WeatherForecast weatherData, CityDto city)
        {
            return await Task.FromResult(CreateTestData(weatherData, city));
        }

        private static WeatherForecast CreateTestData(WeatherForecast weatherData, CityDto city)
        {
            return new WeatherForecast
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
                Source = weatherData.Source
            };
        }
    }
}
