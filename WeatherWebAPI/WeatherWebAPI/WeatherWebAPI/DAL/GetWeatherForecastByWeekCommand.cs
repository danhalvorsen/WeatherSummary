using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database.GetWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByWeekCommand
    {
        private readonly IConfiguration _config;
        private readonly IFactory _factory;

        public GetWeatherForecastByWeekCommand(IConfiguration config, IFactory factory)
        {
            this._config = config;
            this._factory = factory;
        }

        public List<WeatherForecastDto> GetWeatherForecastByWeek(int week, CityQuery query)
        {
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
                                                        $"WHERE DATEPART(week, [Date]) = {week} AND City.Name = '{query.City}'";

            IGetWeatherDataFromDatabaseStrategy weatherData = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();

            return weatherData.Query(queryString);
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
        }
    }


}
