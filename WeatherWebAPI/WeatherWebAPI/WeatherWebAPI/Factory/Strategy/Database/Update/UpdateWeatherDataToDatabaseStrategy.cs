using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public interface IUpdateWeatherDataToDatabaseStrategy
    {
        Task Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated);
    }

    public class UpdateWeatherDataToDatabaseStrategy : IUpdateWeatherDataToDatabaseStrategy
    {
        private readonly IDatabaseConfig config;

        public UpdateWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        {
            this.config = config;
        }

        public async Task Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
        {
            string queryString = $"UPDATE WeatherData " +
                        $"SET [Date] = '{weatherData.Date}', " +
                        $"WeatherType = '{weatherData.WeatherType}', " +
                        $"Temperature = {weatherData.Temperature}, " +
                        $"Windspeed = {weatherData.Windspeed}, " +
                        $"WindDirection = {weatherData.WindDirection}, " +
                        $"WindspeedGust = {weatherData.WindspeedGust}, " +
                        $"Pressure = {weatherData.Pressure}, " +
                        $"Humidity = {weatherData.Humidity}, " +
                        $"ProbOfRain = {weatherData.ProbOfRain}, " +
                        $"AmountRain = {weatherData.AmountRain}, " +
                        $"CloudAreaFraction = {weatherData.CloudAreaFraction}, " +
                        $"FogAreaFraction = {weatherData.FogAreaFraction}, " +
                        $"ProbOfThunder = {weatherData.ProbOfThunder} " +
                            $"FROM WeatherData " +
                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                    $"WHERE CAST([Date] as Date) = '{dateToBeUpdated.Date}' AND City.Name = '{city.Name}'";

            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                await command.ExecuteNonQueryAsync();
            }
        }
    }
    public class UpdateWeatherDataToDatabaseConfig : IDatabaseConfig
    {
        public string? ConnectionString { get; set; }
    }
}
