using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public class GetCitiesQuery : IGetCityQuery
    {
        private readonly IConfiguration _config;
        private const string _queryString = $"SELECT * FROM City";

        public GetCitiesQuery(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<List<CityDto>> GetAllCities()
        {
            var connectionString = _config.GetConnectionString("WeatherForecastDatabase");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryString, connection);
                connection.Open();

                var list = new List<CityDto>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    foreach (object o in reader)
                    {
                        list.Add(new CityDto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Country = reader["Country"].ToString(),
                            Altitude = (float)Convert.ToDouble(reader["Altitude"]),
                            Longitude = (float)Convert.ToDouble(reader["Longitude"]),
                            Latitude = (float)Convert.ToDouble(reader["Latitude"]),
                        });
                    }
                }

                await command.ExecuteNonQueryAsync();
                return list;
            }
        }
    }
}
