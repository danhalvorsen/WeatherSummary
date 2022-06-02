
using System.Globalization;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByDateCommand : BaseWeatherForecastQuery
    {

        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastByDateCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IWeatherDataStrategy> getWeatherDataStrategies)
        {
            string? cityName = query?.CityQuery?.City;
            DateTime date = query.DateQuery.Date;

            try
            {
                // Itterating through all the cities and dates in the database
                var getCitiesQuery = new GetCitiesQuery(_config);
                var cities = await getCitiesQuery.GetAllCities();

                var getDatesQuery = new GetDatesQuery(_config);
                var dates = await getDatesQuery.GetAllDates();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                cityName = textInfo.ToTitleCase(cityName);

                // Checking if the city is in our database, if not it's getting added.
                if (!cities.ToList().Any(c => c.Name.Equals(cityName)))
                {
                    await (new CreateCityCommand(_config).InsertCityIntoDatabase(cityName, new OpenWeatherStrategy(), _factory));
                }

                // Updateing cityquery
                var getCitiesQueryUpdate = new GetCitiesQuery(_config);
                var citiesUpdated = await getCitiesQueryUpdate.GetAllCities();

                foreach (var strategy in getWeatherDataStrategies)
                {
                    var city = citiesUpdated.Where(c => c.Name.Equals(cityName)).First();

                    await (new AddWeatherDataForCityCommand(_config).GetWeatherDataForCity(city, strategy));
                }

                if (!dates.ToList().Any(d => d.Date.Date.Equals(date.Date) && DateTime.Now < d.Date))
                {
                    foreach (var strategy in getWeatherDataStrategies)
                    {
                        var city = citiesUpdated.Where(c => c.Name.Equals(cityName)).First();

                        await (new AddWeatherDataForCityCommand(_config).GetWeatherDataForCity(date, city, strategy));
                    }
                }

                if (dates.ToList().Any(d => d.Date.Date.Equals(date.Date) && DateTime.Now < d.Date))
                {
                    foreach (var strategy in getWeatherDataStrategies)
                    {
                        var city = citiesUpdated.Where(c => c.Name.Equals(cityName)).First();

                        await (new AddWeatherDataForCityCommand(_config).UpdateWeatherDataForCity(date, city, strategy));
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


            return DatabaseQuery(queryString);
        }
    }
}
