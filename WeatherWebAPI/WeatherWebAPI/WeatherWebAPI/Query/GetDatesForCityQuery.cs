using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public class GetDatesForCityQuery : IGetDatesQuery
    {
        private readonly IConfiguration config;

        public GetDatesForCityQuery(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<WeatherForecastDto>> GetDatesForCity(string cityName)
        {
            string queryString = $"SELECT [Date] FROM WeatherData " +
                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                        $"WHERE City.Name = '{cityName}'";

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
