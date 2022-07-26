using System.Data.SqlClient;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.Query
{
    public class GetDatesForCityQuery : IGetDatesForCityQuery
    {
        private readonly IConfiguration config;

        public GetDatesForCityQuery(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<WeatherForecastDto>> GetDatesForCity(string cityName, IGetWeatherDataStrategy<WeatherForecastDto> strategy)
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

                var list = new List<WeatherForecastDto>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        list.Add(new WeatherForecastDto
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
