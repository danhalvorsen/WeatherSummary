using System.Globalization;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastBetweenDatesCommand : BaseWeatherForecastQuery
    {
        private readonly IFactory factory;
       

        public GetWeatherForecastBetweenDatesCommand(IConfiguration config, IFactory factory) : base(config)
        {
            this.factory = factory;
        }

        

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity, List<IGetWeatherDataStrategy<WeatherForecastDto>> getWeatherDataStrategies)
        {
            string? cityName = betweenDateQueryAndCity?.CityQuery?.City;
            DateTime fromDate = betweenDateQueryAndCity.BetweenDateQuery.From;
            DateTime toDate = betweenDateQueryAndCity.BetweenDateQuery.To;
            var datesQuery = new List<DateTime>();

            try
            {
                // Itterating through all the cities & dates in the database
                var getCitiesQuery = new GetCitiesQuery(config);
                var cities = await getCitiesQuery.GetAllCities();

                var getDatesQueryDatabase = new GetDatesQuery(config);
                var datesDatabase = await getDatesQueryDatabase.GetAllDates();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

                // Getting the all the dates between the from and to datequeries
                foreach (DateTime day in EachDay(fromDate, toDate))
                {
                    datesQuery.Add(day);
                }

                // Checking if the city is in our database, if not it's getting added.
                if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
                {
                    await (new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName));
                }

                // Updateing cityquery
                var getCitiesQueryUpdate = new GetCitiesQuery(config);
                var citiesUpdated = await getCitiesQueryUpdate.GetAllCities();

                if (!datesDatabase.ToList().Any(d => d.Date.Date.Equals(datesQuery) && DateTime.Now < d.Date))
                {
                    foreach (DateTime date in datesQuery)
                    {
                        foreach (var strategy in getWeatherDataStrategies)
                        {
                            //var city = citiesUpdated.Where(c => c.Name.Equals(cityName)).First();

                            //await new AddWeatherDataForCityCommand(config).GetWeatherDataForCity(date, city, strategy);


                            //ToDo: asdfafsaf

                            await GetWeatherDataAndAddToDatabase(cityName, date, cities, strategy);
                        }
                    }
                }
                if (datesDatabase.ToList().Any(d => d.Date.Date.Equals(datesQuery) && DateTime.Now < d.Date))
                {

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, " +
                    $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                        $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                            $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                    $"WHERE CAST([Date] as Date) BETWEEN '{fromDate}' AND '{toDate}' AND City.Name = '{cityName}'";

            return Query(queryString);
        }

        //internal Task<ActionResult<List<WeatherForecastDto>>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecastDto>> list)
        //{
        //    throw new NotImplementedException();
        //}

        private async Task GetWeatherDataAndAddToDatabase(string? cityName, DateTime date, List<Controllers.CityDto> cities, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy)
        {
            var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);
            var addToDatabaseStrategy = factory.Build<IAddWeatherDataToDatabaseStrategy>();
            addToDatabaseStrategy.Add(weatherData);
        }
    }
}
