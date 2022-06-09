using System.Data.SqlClient;

namespace WeatherWebAPI.Query
{
    public class GetDatesQuery
    {
        private readonly IConfiguration config;
        private const string queryString = $"SELECT * FROM WeatherData";

        public GetDatesQuery(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<WeatherForecastDto>> GetAllDates()
        {
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
                return list;
            }
        }
    }
}
