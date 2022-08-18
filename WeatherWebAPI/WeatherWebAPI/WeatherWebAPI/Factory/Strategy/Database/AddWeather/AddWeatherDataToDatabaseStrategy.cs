using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddWeatherDataToDatabaseStrategy : IAddWeatherDataToDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public AddWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        {
            this._config = config;
        }

        public async Task Add(WeatherForecast weatherData, CityDto city)
        {
            string queryString = $"DECLARE @city_id INT " +
                                    $"DECLARE @source_id INT " +
                                        $"DECLARE @fk_weatherdata_id INT " +
                                            $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                                $"SELECT @city_id = Id FROM City WHERE City.Name = '{city.Name}' " +
                                                    $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{weatherData.Source?.DataProvider}' " +
                                                        $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                                            $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId, DateForecast) " +
                                    $"VALUES('{weatherData.Date}', '{weatherData.WeatherType}', {weatherData.Temperature}, {weatherData.Windspeed}, " +
                                        $"{weatherData.WindDirection}, {weatherData.WindspeedGust}, {weatherData.Pressure}, {weatherData.Humidity}, {weatherData.ProbOfRain}, " +
                                            $"{weatherData.AmountRain}, {weatherData.CloudAreaFraction}, {weatherData.FogAreaFraction}, {weatherData.ProbOfThunder}, @city_id, '{weatherData.DateForecast}')" +

                                    $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                        $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId) " +
                                            $"VALUES('{weatherData.Date}', @source_id, @fk_weatherdata_id)";

            using (SqlConnection connection = new(_config.ConnectionString))
            {
                SqlCommand command = new(queryString, connection);
                connection.Open();

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
