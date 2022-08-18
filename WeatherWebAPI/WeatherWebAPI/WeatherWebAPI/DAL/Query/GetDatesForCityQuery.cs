using System.Data.SqlClient;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.Query
{
    public class GetDatesForCityQuery : IGetDatesQuery
    {
        private readonly IConfiguration config;

        public GetDatesForCityQuery(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<WeatherForecast>> GetDatesForCity(string cityName, IGetWeatherDataStrategy<WeatherForecast> strategy)
        {
            string queryString = $"SELECT [Date] FROM WeatherData " +
                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                            $"INNER JOIN [Source] ON [Source].Id = SourceWeatherData.FK_SourceId " +
                                                $"WHERE City.Name = '{cityName}' AND [Source].Name = '{strategy.GetDataSource()}'";

            var connectionString = config.GetConnectionString("WeatherForecastDatabase");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                var list = new List<WeatherForecast>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        list.Add(new WeatherForecast
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
}
