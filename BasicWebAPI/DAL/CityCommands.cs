//using BasicWebAPI.Controllers;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;

//namespace BasicWebAPI.DAL
//{
//    public class CityCommands
//    {
//        private readonly IConfiguration config;
//        public CityCommands(IConfiguration config)
//        {
//            this.config = config;
//        }

//        public List<CityDto> GetCities()
//        {
//            var list = new List<CityDto>();
//            string queryString = $"SELECT * FROM City";

//            //list = DatabaseCityQuery(list, queryString);

//            return DatabaseCityQuery(list, queryString);
//        }

//        private List<CityDto> DatabaseCityQuery(List<CityDto> list, string queryString)
//        {
//            var connectionString = config.GetConnectionString("WeatherForecastDatabase");
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                SqlCommand command = new SqlCommand(queryString, connection);
//                connection.Open();

//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    foreach (object o in reader)
//                    {
//                        list.Add(new CityDto
//                        {
//                            Id = Convert.ToInt32(reader["Id"]),
//                            Name = reader["Name"].ToString(),
//                            Country = reader["Country"].ToString(),
//                            Altitude = (float)Convert.ToDouble(reader["Altitude"]),
//                            Longitude = (float)Convert.ToDouble(reader["Longitude"]),
//                            Latitude = (float)Convert.ToDouble(reader["Latitude"]),
//                        }) ;
//                    }
//                    //reader.Close();
//                }                
//                return list;
//            }
//        }
//    }
//}
