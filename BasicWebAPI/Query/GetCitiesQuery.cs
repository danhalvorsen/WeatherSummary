using BasicWebAPI.Controllers;
using BasicWebAPI.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BasicWebAPI.DAL
{
    public class GetCitiesQuery : IQuery
    {
        private readonly IConfiguration config;
        private const string queryString = $"SELECT * FROM City";

        public GetCitiesQuery(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<CityDto>> GetAllCities()
        {
            var connectionString = config.GetConnectionString("WeatherForecastDatabase");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
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
                return list;
            }
        }
    }
}
