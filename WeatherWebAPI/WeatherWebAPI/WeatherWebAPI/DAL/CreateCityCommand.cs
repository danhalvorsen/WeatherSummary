using System.Data.SqlClient;
using System.Globalization;
using WeatherWebAPI.Factory;

namespace WeatherWebAPI.DAL
{
    public class CreateCityCommand
    {
        private readonly IConfiguration _config;

        public CreateCityCommand(IConfiguration config)
        {
            this._config = config;
        }

        public async Task InsertCityIntoDatabase(string city, IWeatherDataStrategy<> strategy, GetWeatherDataFactory factory)
        {
            try
            {
                var result = await factory.GetCityDataFrom(city, strategy);

                // Getting the full country name
                var twoLetterCountryAbbreviation = new CultureInfo(result[0].Country);
                var countryName = new RegionInfo(twoLetterCountryAbbreviation.Name);
                result[0].Country = countryName.EnglishName;

                string queryString = $"INSERT INTO City ([Name], Country, Altitude, Longitude, Latitude) " +
                                        $"VALUES('{result[0].Name}', '{result[0].Country}', {result[0].Altitude}, {result[0].Longitude}, {result[0].Latitude})";

                using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("WeatherForecastDatabase")))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
