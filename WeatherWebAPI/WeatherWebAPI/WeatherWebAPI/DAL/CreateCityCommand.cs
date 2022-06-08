using System.Data.SqlClient;
using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace WeatherWebAPI.DAL
{
    public class CreateCityCommand
    {
        private readonly IConfiguration _config;
        private readonly IFactory factory;

        public CreateCityCommand(IConfiguration config, IFactory factory)
        {
            this._config = config;
            this.factory = factory;
        }

        public async Task/*<CityDto>*/ InsertCityIntoDatabase(string city)
        {
            try
            {
                var strategy = this.factory.Build<IOpenWeatherStrategy>();

                var result = await strategy.GetCityDataFor(city);

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
                //return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //return new CityDto();
        }
    }
}
