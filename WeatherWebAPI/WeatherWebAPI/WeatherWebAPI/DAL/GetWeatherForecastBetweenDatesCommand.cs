using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastBetweenDatesCommand : BaseGetWeatherForecastCommands
    {
        public GetWeatherForecastBetweenDatesCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity, List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            string? citySearchedFor = betweenDateQueryAndCity?.CityQuery?.City;
            string? cityName = "";
            DateTime fromDate = betweenDateQueryAndCity!.BetweenDateQuery!.From.ToUniversalTime();
            DateTime toDate = betweenDateQueryAndCity!.BetweenDateQuery.To!.ToUniversalTime();

            var datesQuery = new List<DateTime>();
            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            //var getDatesQueryDatabase = new GetDatesForCityQuery(_config);

            try
            {
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo? textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                // Getting the all the dates between the from and to datequeries
                foreach (DateTime day in EachDay(fromDate, toDate))
                {
                    datesQuery.Add(day);
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

                //var city = GetCityDtoBy(cityName!);

                //foreach (DateTime date in datesQuery)
                //{
                //    if (date >= DateTime.UtcNow.Date)
                //    {
                //        foreach (var weatherStrategy in weatherDataStrategies)
                //        {
                //            _datesDatabase = await getDatesQueryDatabase.GetDatesForCity(city.Name!, weatherStrategy);

                //            if (DateDoesNotExistInDatabase(date))
                //            {  
                //                await GetWeatherDataAndAddToDatabase(date, weatherStrategy, city);
                //            }
                //            if (DateExistsInDatabase(date))
                //            {
                //                await GetWeatherDataAndUpdateDatabase(date, weatherStrategy, city);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return GetWeatherForecastFromDatabase(cityName, fromDate, toDate);

        }

        private List<WeatherForecastDto> GetWeatherForecastFromDatabase(string? cityName, DateTime fromDate, DateTime toDate)
        {
            string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                    $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                        $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                            $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                    $"WHERE CAST([DateForecast] as Date) BETWEEN '{fromDate}' AND '{toDate}' AND City.Name = '{cityName}' " +
                                        $"ORDER BY [DateForecast], [Date]";

            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return getWeatherDataFromDatabaseStrategy.Get(queryString);
        }
    }
}
