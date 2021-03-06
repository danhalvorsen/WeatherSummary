using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByWeekCommand : BaseGetWeatherForecastCommands
    {
        public GetWeatherForecastByWeekCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByWeek(WeekQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? citySearchedFor = query.CityQuery?.City;
            string? cityName = "";
            DateTime monday = FirstDateOfWeekISO8601(DateTime.UtcNow.Year, query.Week);
            DateTime sunday = monday.AddDays(6);


            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            var getDatesQueryDatabase = new GetDatesForCityQuery(_config);
            var datesInWeek = new List<DateTime>();

            try
            {
                // Itterating through all the cities and dates in the database   
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                // Getting the all the dates between the from and to datequeries
                foreach (DateTime day in EachDay(monday, sunday))
                {
                    datesInWeek.Add(day);
                }

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;

                    if (cityName != "")
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

                var city = GetCityDtoBy(cityName!);

                foreach (DateTime date in datesInWeek)
                {
                    if (date >= DateTime.UtcNow.Date)
                    {
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
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return GetWeatherForecastFromDatabase(query, cityName);
        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(WeekQueryAndCity query, string? cityName)
        {
            string queryString = $"SET DATEFIRST 1 " +
                                  $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE DATEPART(week, [Date]) = {query.Week} AND City.Name = '{cityName}'";

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return getWeatherDataFromDatabaseStrategy.Get(queryString);
        }
    }
}
