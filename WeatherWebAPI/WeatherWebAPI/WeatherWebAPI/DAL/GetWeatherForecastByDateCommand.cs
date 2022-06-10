using System.Globalization;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByDateCommand : BaseWeatherForecastQuery
    {
        private readonly IFactory factory;

        public GetWeatherForecastByDateCommand(IConfiguration config, IFactory factory) : base(config)
        {
            this.factory = factory;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? cityName = query?.CityQuery?.City;
            DateTime date = query.DateQuery.Date;

            try
            {
                // Itterating through all the cities and dates in the database
                var getCitiesQuery = new GetCitiesQuery(config);
                var cities = await getCitiesQuery.GetAllCities();

                var getDatesQuery = new GetDatesQuery(config);
                var dates = await getDatesQuery.GetAllDates();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

                // Checking if the city is in our database, if not it's getting added.
                if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
                {
                    await new CreateCityCommand(config, factory).InsertCityIntoDatabase(cityName);

                    // Updateing cityquery
                    var getCitiesQueryUpdate = new GetCitiesQuery(config);
                    var citiesUpdated = await getCitiesQueryUpdate.GetAllCities();

                    foreach (var weatherStrategy in weatherDataStrategies)
                    {
                        await GetWeatherDataAndAddToDatabase(cityName, date, citiesUpdated, weatherStrategy);
                    }
                }

                if ((!dates.ToList().Any(d => d.Date.Date.Equals(date.Date))) && DateTime.Now < date)
                {
                    foreach (var weatherStrategy in weatherDataStrategies)
                    {
                        await GetWeatherDataAndAddToDatabase(cityName, date, cities, weatherStrategy);
                    }
                }

                if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date)) && DateTime.Now < date)
                {
                    foreach (var strategy in weatherDataStrategies)
                    {
                        var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

                        await new AddWeatherDataForCityCommand(config).UpdateWeatherDataForCity(date, city, strategy);
                    }
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
                                                        $"WHERE CAST([Date] as Date) = '{date}' AND City.Name = '{cityName}'";


            return Query(queryString);
        }

        private async Task GetWeatherDataAndAddToDatabase(string? cityName, DateTime date, List<Controllers.CityDto> cities, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy)
        {
            var city = cities.ToList().Where(c => c.Name.Equals(cityName)).First();

            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);
            var addToDatabaseStrategy = factory.Build<IAddWeatherDataToDatabaseStrategy>();
            addToDatabaseStrategy.Add(weatherData);
        }
    }
}
