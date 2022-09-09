using AutoMapper;
using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherForecastBetweenDatesQuery : BaseFunctionsForQueriesAndCommands
    {
        private readonly IMapper _mapper;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IOpenWeatherFetchCityStrategy _openWeatherFetchCityStrategy;
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;

        public GetWeatherForecastBetweenDatesQuery(
            IMapper mapper, 
            IGetCitiesQuery getCitiesQuery,
            IOpenWeatherFetchCityStrategy openWeatherFetchCityStrategy,
            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy
            ) : base()
        {
            _mapper = mapper;
            _getCitiesQuery = getCitiesQuery;
            _openWeatherFetchCityStrategy = openWeatherFetchCityStrategy;
            _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastBetweenDates(BetweenDateQueryAndCity betweenDateQueryAndCity)
        {
            string? citySearchedFor = betweenDateQueryAndCity?.CityQuery?.City;
            string? cityName;
            DateTime fromDate = betweenDateQueryAndCity!.BetweenDateQuery!.From.ToUniversalTime();
            DateTime toDate = betweenDateQueryAndCity!.BetweenDateQuery.To!.ToUniversalTime();

            var datesQuery = new List<DateTime>();
            var dtoList = new List<WeatherForecastDto>();

            try
            {
                var cities = await _getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                var textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                foreach (DateTime day in EachDay(fromDate, toDate))
                {
                    datesQuery.Add(day);
                }

                if (!CityExists(citySearchedFor!, cities))
                {
                    var cityData = await GetCityData(citySearchedFor, _openWeatherFetchCityStrategy);
                    cityName = cityData[0].Name;
                }
                else
                {
                    cityName = citySearchedFor;
                }

                string queryString = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Value, Score.ValueWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                            $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                    $"WHERE CAST(DateForecast as date) = CAST([Date] as date) AND CAST([DateForecast] as Date) BETWEEN '{fromDate}' AND '{toDate}' AND City.Name = '{cityName}' " +
                                        $"ORDER BY [DateForecast], [Date]";

                await MakeWeatherForecastDto(_mapper, dtoList, queryString, _getWeatherDataFromDatabaseStrategy);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dtoList;
        }
    }
}
