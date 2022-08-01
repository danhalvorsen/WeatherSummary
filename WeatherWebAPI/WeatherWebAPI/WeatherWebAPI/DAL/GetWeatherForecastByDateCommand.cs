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
            string? citySearchedFor = query?.CityQuery?.City;
            string? cityName = "";
            DateTime date = query!.DateQuery!.Date.ToUniversalTime();

            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            var getDatesQueryDatabase = new GetDatesForCityQuery(_config);

            try
            {
                // Itterating through all the cities and dates in the database   
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                // Checking if the city is in our database, if not it's getting added.
                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;

                    if(cityName != "")
                    {
                        if (!CityExists(cityName!))
                        {
                            await AddCityToDatabase(cityData);
                            _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();
                        }
                    }
                }
                else
                {
                    cityName = citySearchedFor;
                }

                if (date >= DateTime.UtcNow.Date)
                {
                    var city = GetCityDtoBy(cityName!);

                    foreach (var weatherStrategy in weatherDataStrategies)
                    {
                        _datesDatabase = await getDatesQueryDatabase.GetDatesForCity(city.Name!, weatherStrategy);

                        if (DateDoesNotExistInDatabase(date))
                        {
                            await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);
                        }
                        if (DateExistsInDatabase(date))
                        {
                            await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return GetWeatherForecastFromDatabase(cityName, date, weatherDataStrategies);
        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(string? cityName, DateTime date, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string queryString = $"SELECT TOP {weatherDataStrategies.Count} WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
            $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                    $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                        $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                            $"WHERE CAST([Date] as Date) >= '{date}' AND City.Name = '{cityName}' " +
                                $"ORDER BY [Date]";

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return getWeatherDataFromDatabaseStrategy.Get(queryString);
        }
    }

}
