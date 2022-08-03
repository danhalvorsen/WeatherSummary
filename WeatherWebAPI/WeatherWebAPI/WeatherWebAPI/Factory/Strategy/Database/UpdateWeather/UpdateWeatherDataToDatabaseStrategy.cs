using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class UpdateWeatherDataToDatabaseStrategy : IUpdateWeatherDataToDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public UpdateWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        {
            this._config = config;
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
                        $"ProbOfThunder = {weatherData.ProbOfThunder}, " +
                        $"DateForecast = {weatherData.DateForecast} " + 
                            $"FROM WeatherData " +
                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                        $"INNER JOIN [Source] ON [Source].Id = SourceWeatherData.FK_SourceId " +
                                            $"WHERE CAST([DateForecast] as Date) = '{dateToBeUpdated.Date}' AND City.Name = '{city.Name}' AND [Source].Name = '{weatherData.Source.DataProvider}'" + 
                                            
                                 $"UPDATE SourceWeatherData " +
                                    $"SET ConnectionDate = '{weatherData.Date}' " +
                                        $"FROM SourceWeatherData " + 
                                            $"INNER JOIN WeatherData ON WeatherData.Id = SourceWeatherData.FK_WeatherDataId " + 
                                                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " + 
                                                    $"INNER JOIN [Source] ON [Source].Id = SourceWeatherData.FK_SourceId " +
                                                        $"WHERE CAST([ConnectionDate] as Date) = '{dateToBeUpdated.Date}' AND City.Name = '{city.Name}' AND [Source].Name = '{weatherData.Source.DataProvider}'";

            using (SqlConnection connection = new(_config.ConnectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
