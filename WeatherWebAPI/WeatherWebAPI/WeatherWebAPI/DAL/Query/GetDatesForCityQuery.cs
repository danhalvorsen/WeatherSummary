using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;

namespace WeatherWebAPI.Query
{
    public class GetDatesForCityQuery : IGetDatesForCityQuery
    {
        private readonly IConfiguration _config;

        public GetDatesForCityQuery(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<WeatherForecast.WeatherData>> GetDatesForCity(string cityName, IGetWeatherDataStrategy strategy)
        {
            string queryString = $"SELECT [Date] FROM WeatherData " +
                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                            $"INNER JOIN [Source] ON [Source].Id = SourceWeatherData.FK_SourceId " +
                                                $"WHERE City.Name = '{cityName}' AND [Source].Name = '{strategy.GetDataSource()}'";

            using SqlConnection connection = new(_config.GetConnectionString("WeatherForecastDatabase"));
            SqlCommand command = new(queryString, connection);
            connection.Open();

            var list = new List<WeatherForecast.WeatherData>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                foreach (object o in reader)
                {
                    list.Add(new WeatherForecast.WeatherData
                    {
                        Date = Convert.ToDateTime(reader["Date"])
                    });
                }
            }

            await command.ExecuteNonQueryAsync();
            return list;
        }
    }
}
