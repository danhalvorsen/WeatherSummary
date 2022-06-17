using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByDateCommand : BaseGetWeatherForecastCommands
    {
        public GetWeatherForecastByDateCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? cityName = query?.CityQuery?.City;
            DateTime date = query.DateQuery.Date.ToUniversalTime();

            var getCitiesQuery = new GetCitiesQuery(_config);
            var getDatesQuery = new GetDatesForCityQuery(_config);

            try
            {
                // Itterating through all the cities and dates in the database   
                _citiesDatabase = await getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

                // Checking if the city is in our database, if not it's getting added.
                if (!CityExists(cityName))
                {
                    await GetCityAndAddToDatabase(cityName);
                    _citiesDatabase = await getCitiesQuery.GetAllCities();
                    //cityName = _citiesDatabase.Last().Name; <- Tror dette fikser navn skrevet inn på flere språk.
                    // Fikser kun ved adding første gang. Trengs nok api for translation..?
                }

                if (date >= DateTime.UtcNow.Date)
                {
                    var city = GetCityDtoBy(cityName);

                    foreach (var weatherStrategy in weatherDataStrategies)
                    {
                        _datesDatabase = await getDatesQuery.GetDatesForCity(city.Name, weatherStrategy);

                        if (GetWeatherDataBy(date))
                        {
                            await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);
                        }
                        if (UpdateWeatherDataBy(date))
                        {
                            await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);
                        }
                    }
                }
                return GetWeatherForecastFromDatabase(cityName, date);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new List<WeatherForecastDto>();
        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(string? cityName, DateTime date)
        {
            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
            $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                        $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                            $"WHERE CAST([Date] as Date) = '{date}' AND City.Name = '{cityName}'";

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return getWeatherDataFromDatabaseStrategy.Query(queryString);
        }
    }

}
