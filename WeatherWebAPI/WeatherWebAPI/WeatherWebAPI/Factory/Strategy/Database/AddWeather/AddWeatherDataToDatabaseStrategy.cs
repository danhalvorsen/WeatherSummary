using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddWeatherDataToDatabaseStrategy : IAddWeatherDataToDatabaseStrategy
    {
        private readonly IConfiguration _config;

        public AddWeatherDataToDatabaseStrategy(IConfiguration config)
        {
            _config = config;
        }

        public StrategyType StrategyType => StrategyType.AddWeatherToDatabase;

        public async Task Add(List<WeatherForecast.WeatherData> forecasts)
        {
            try
            {

                foreach (var forecast in forecasts)
                {
                    string queryString = $"DECLARE @city_id INT " +
                        $"DECLARE @source_id INT " +
                            $"DECLARE @fk_weatherdata_id INT " +
                                $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                    $"SELECT @city_id = Id FROM City WHERE City.Name = '{forecast.City}' " +
                                        $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{forecast.Source?.DataProvider}' " +
                                            $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                                $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId, DateForecast) " +
                        $"VALUES('{forecast.Date}', '{forecast.WeatherType}', {forecast.Temperature}, {forecast.Windspeed}, " +
                            $"{forecast.WindDirection}, {forecast.WindspeedGust}, {forecast.Pressure}, {forecast.Humidity}, {forecast.ProbOfRain}, " +
                                $"{forecast.AmountRain}, {forecast.CloudAreaFraction}, {forecast.FogAreaFraction}, {forecast.ProbOfThunder}, @city_id, '{forecast.DateForecast}')" +

                        $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                            $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId) " +
                                $"VALUES('{forecast.Date}', @source_id, @fk_weatherdata_id)";

                    using SqlConnection connection = new(_config.GetConnectionString("WeatherForecastDatabase"));
                    SqlCommand command = new(queryString, connection);
                    connection.Open();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
