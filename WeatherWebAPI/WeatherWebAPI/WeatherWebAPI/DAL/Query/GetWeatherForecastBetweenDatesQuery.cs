using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherForecastBetweenDatesQuery : BaseFunctionsForQueriesAndCommands
    {

        protected readonly WeatherForecastMapping _weatherForecastMapping;

        public GetWeatherForecastBetweenDatesQuery(IConfiguration config, IFactory factory, WeatherForecastMapping weatherForecastMapping) : base(config, factory)
        {
            _weatherForecastMapping = weatherForecastMapping;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity)
        {
            string? citySearchedFor = betweenDateQueryAndCity?.CityQuery?.City;
            string? cityName;
            DateTime fromDate = betweenDateQueryAndCity!.BetweenDateQuery!.From.ToUniversalTime();
            DateTime toDate = betweenDateQueryAndCity!.BetweenDateQuery.To!.ToUniversalTime();

            var datesQuery = new List<DateTime>();
            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            var dtoList = new List<WeatherForecastDto>();

            try
            {
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                foreach (DateTime day in EachDay(fromDate, toDate))
                {
                    datesQuery.Add(day);
                }

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;
                }
                else
                {
                    cityName = citySearchedFor;
                }

                string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                            $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                    $"WHERE CAST(DateForecast as date) = CAST([Date] as date) AND CAST([DateForecast] as Date) BETWEEN '{fromDate}' AND '{toDate}' AND City.Name = '{cityName}' " +
                                        $"ORDER BY [DateForecast], [Date]";

                await MakeWeatherForecastDto(dtoList, _weatherForecastMapping, queryString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dtoList;
        }
    }
}
