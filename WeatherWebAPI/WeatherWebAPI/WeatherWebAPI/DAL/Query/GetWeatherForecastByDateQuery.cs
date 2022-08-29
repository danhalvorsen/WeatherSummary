using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Query
{
    public class GetWeatherForecastByDateQuery : BaseFunctionsForQueriesAndCommands
    {
        private readonly WeatherForecastMapping _weatherForecastMapping;

        public GetWeatherForecastByDateQuery(IConfiguration config, IFactory factory, WeatherForecastMapping weatherForecastMapping) : base(config, factory)
        {
            _weatherForecastMapping = weatherForecastMapping;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByDate(DateQueryAndCity query, List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
            string? citySearchedFor = query?.CityQuery?.City;
            string? cityName;
            DateTime date = query!.DateQuery!.Date.ToUniversalTime();

            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            var dtoList = new List<WeatherForecastDto>();

            try
            {
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;
                }
                else
                {
                    cityName = citySearchedFor;
                }

                DateTime now;
                if (DateTime.UtcNow.Date > date.Date)
                    now = date;
                else
                    now = DateTime.UtcNow;

                string queryString = $"SELECT TOP {weatherDataStrategies.Count} WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                            $"INNER JOIN [Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                    $"WHERE CAST([DateForecast] as date) = '{date}' AND CAST([Date] as date) = '{now}'  AND City.Name = '{cityName}' " +
                                        $"ORDER BY DateForecast";

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
