using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.Database.GetWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastBetweenDatesCommand
    {
        private readonly IConfiguration config;
        private readonly IFactory _factory;
        private List<CityDto>? _cities;
        private List<WeatherForecastDto>? _datesDatabase;

        public GetWeatherForecastBetweenDatesCommand(IConfiguration config, IFactory factory)
        {
            this.config = config;
            _factory = factory;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? cityName = betweenDateQueryAndCity?.CityQuery?.City;
            DateTime fromDate = betweenDateQueryAndCity.BetweenDateQuery.From;
            DateTime toDate = betweenDateQueryAndCity.BetweenDateQuery.To;

            var datesQuery = new List<DateTime>();
            var getCitiesQuery = new GetCitiesQuery(config);
            var getDatesQueryDatabase = new GetDatesQuery(config);

            try
            {
                // Itterating through all the cities & dates in the database
                _cities = await getCitiesQuery.GetAllCities();
                _datesDatabase = await getDatesQueryDatabase.GetAllDates();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

                // Getting the all the dates between the from and to datequeries
                foreach (DateTime day in EachDay(fromDate, toDate))
                {
                    datesQuery.Add(day);
                }

                if (CityExists(cityName))
                {
                    await (new CreateCityCommand(config, _factory).InsertCityIntoDatabase(cityName));
                    _cities = await getCitiesQuery.GetAllCities();
                }

                foreach (DateTime date in datesQuery)
                {
                    if (date > DateTime.Now)
                    {
                        foreach (var weatherStrategy in weatherDataStrategies)
                        {
                            if (GetWeatherDataBy(date))
                            {
                                var city = GetCityDtoBy(cityName);
                                await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);

                            }
                            if (UpdateWeatherDataBy(date))
                            {
                                var city = GetCityDtoBy(cityName);

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

            return GetWeatherForecastFromDatabase(cityName, fromDate, toDate);

        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(string? cityName, DateTime fromDate, DateTime toDate)
        {
            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                    $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                        $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                            $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                    $"WHERE CAST([Date] as Date) BETWEEN '{fromDate}' AND '{toDate}' AND City.Name = '{cityName}'";

            IGetWeatherDataFromDatabaseStrategy weatherData = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();
            
            return weatherData.Query(queryString);
        }

        private async Task GetWeatherDataAndUpdateDatabase(DateTime date, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy, CityDto city)
        {
            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);
            IUpdateWeatherDataToDatabaseStrategy updateDatabaseStrategy = _factory.Build<IUpdateWeatherDataToDatabaseStrategy>();
            await updateDatabaseStrategy.Update(weatherData, city, date);
        }

        private async Task GetWeatherDataAndAddToDatabase(DateTime date, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy, CityDto city)
        {
            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);
            var addToDatabaseStrategy = _factory.Build<IAddWeatherDataToDatabaseStrategy>();
            addToDatabaseStrategy.Add(weatherData);
        }

        private bool CityExists(string cityName)
        {
            return !_cities.ToList().Any(c => c.Name.Equals(cityName));
        }

        private CityDto GetCityDtoBy(string cityName)
        {
            return _cities.Where(c => c.Name.Equals(cityName)).First();
        }

        private bool UpdateWeatherDataBy(DateTime date)
        {
            return _datesDatabase.ToList().Any(d => d.Date.Date.Equals(date));
        }

        private bool GetWeatherDataBy(DateTime date)
        {
            return !_datesDatabase.ToList().Any(d => d.Date.Date.Equals(date));
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
        }
    }
}
