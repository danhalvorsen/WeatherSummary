using AutoMapper;
using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherForecastPredictionByDateQuery : BaseFunctionsForQueriesAndCommands
    {
        private readonly IMapper _mapper;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IOpenWeatherFetchCityStrategy _openWeatherFetchCityStrategy;
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;

        public GetWeatherForecastPredictionByDateQuery(
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

        public async Task<List<WeatherForecastDto>> GetWeatherForecastPredictionByDateForOneWeek(DateQueryAndCity query)
        {
            string? citySearchedFor = query?.CityQuery?.City;
            string? cityName;
            DateTime date = query!.DateQuery!.Date.ToUniversalTime();

            var dtoList = new List<WeatherForecastDto>();

            try
            {
                var cities = await _getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

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
                                                    $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                                            $"WHERE CAST([Date] as Date) = '{date}' AND City.Name = '{cityName}' " +
                                                                $"ORDER BY DateForecast";

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
