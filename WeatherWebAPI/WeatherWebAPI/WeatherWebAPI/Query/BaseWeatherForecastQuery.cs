using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Query
{
    public abstract class BaseWeatherForecastQuery
    {
        protected readonly IConfiguration config;

        public BaseWeatherForecastQuery(IConfiguration config)
        {
            this.config = config;
        }

        //public List<WeatherForecastDto> Query(string queryString)
        //{
        //    try
        //    {
        //        var connectionString = config.GetConnectionString("WeatherForecastDatabase");
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlCommand command = new SqlCommand(queryString, connection);
        //            connection.Open();

        //            var WeatherForecastDtos = new List<WeatherForecastDto>();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {

        //                foreach (object o in reader)
        //                {
        //                    var weatherSource = new WeatherSourceDto();
        //                    weatherSource.DataProvider = reader["SourceName"].ToString();

        //                    WeatherForecastDtos.Add(new WeatherForecastDto
        //                    {
        //                        City = reader["CityName"].ToString(),
        //                        Date = Convert.ToDateTime(reader["Date"]),
        //                        WeatherType = reader["WeatherType"].ToString(),
        //                        Temperature = (float)Convert.ToDouble(reader["Temperature"]),
        //                        Windspeed = (float)Convert.ToDouble(reader["Windspeed"]),
        //                        WindDirection = (float)Convert.ToDouble(reader["WindDirection"]),
        //                        WindspeedGust = (float)Convert.ToDouble(reader["WindspeedGust"]),
        //                        Pressure = (float)Convert.ToDouble(reader["Pressure"]),
        //                        Humidity = (float)Convert.ToDouble(reader["Humidity"]),
        //                        ProbOfRain = (float)Convert.ToDouble(reader["ProbOfRain"]),
        //                        AmountRain = (float)Convert.ToDouble(reader["AmountRain"]),
        //                        CloudAreaFraction = (float)Convert.ToDouble(reader["CloudAreaFraction"]),
        //                        FogAreaFraction = (float)Convert.ToDouble(reader["FogAreaFraction"]),
        //                        ProbOfThunder = (float)Convert.ToDouble(reader["ProbOfThunder"]),
        //                        Source = weatherSource,
        //                    });
        //                }
        //            }
        //            return WeatherForecastDtos;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //protected WeatherForecastDto InsertIntoDatabase(WeatherForecastDto addWeatherData, string queryString)
        //{
        //    using (SqlConnection connection = new SqlConnection(config.GetConnectionString("WeatherForecastDatabase")))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        connection.Open();

        //        command.ExecuteNonQuery();
        //    }
        //    return addWeatherData;
        //}

        //protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        //{
        //    for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
        //        yield return day;
        //}
    }
}
