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

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByWeek(int week, CityQuery query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? cityName = query.City;
            DateTime monday = FirstDateOfWeekISO8601(DateTime.UtcNow.Year, week);
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
                cityName = textInfo.ToTitleCase(cityName!);

                // Getting the all the dates between the from and to datequeries
                foreach (DateTime day in EachDay(monday, sunday))
                {
                    datesInWeek.Add(day);
                }

                // Checking if the city is in our database, if not it's getting added.
                if (!CityExists(cityName))
                {
                    await GetCityAndAddToDatabase(cityName);
                    _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();
                    //cityName = _citiesDatabase.Last().Name; <- Tror dette fikser navn skrevet inn på flere språk.
                    // Fikser kun ved adding første gang. Trengs nok api for translation..?
                }

                var city = GetCityDtoBy(cityName);

                foreach (DateTime date in datesInWeek)
                {
                    if (date >= DateTime.UtcNow.Date)
                    {
                        foreach (var weatherStrategy in weatherDataStrategies)
                        {
                            _datesDatabase = await getDatesQueryDatabase.GetDatesForCity(city.Name!, weatherStrategy);

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
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return GetWeatherForecastFromDatabase(week, query);
        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(int week, CityQuery query)
        {
            string queryString = $"SET DATEFIRST 1 " +
                                  $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"WHERE DATEPART(week, [Date]) = {week+1} AND City.Name = '{query.City}'"; // Should change this to match the between query. Since we're basically doing the same in both.

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return getWeatherDataFromDatabaseStrategy.Query(queryString);
        }
    }
}
