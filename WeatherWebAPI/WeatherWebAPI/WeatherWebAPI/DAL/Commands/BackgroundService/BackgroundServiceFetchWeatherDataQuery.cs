using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class BackgroundServiceFetchWeatherDataQuery : BaseFunctionsForQueriesAndCommands, IBackgroundServiceFetchWeatherDataQuery
    {
        private readonly ILogger<BackgroundServiceFetchWeatherDataQuery> _logger;
        private readonly IGetDatesForCityQuery _getDatesForCityQuery;

        public BackgroundServiceFetchWeatherDataQuery(
            ILogger<BackgroundServiceFetchWeatherDataQuery> logger,
            IGetDatesForCityQuery getDatesForCityQuery
            ) : base()
        {
            _logger = logger;
            _getDatesForCityQuery = getDatesForCityQuery;
        }

        public async Task<List<WeatherForecast.WeatherData>> GetOneWeekWeatherForecastForAllCitiesFor(IGetWeatherDataStrategy strategy, List<CityDto> cities)
        {
            DateTime fromDate = DateTime.UtcNow;
            DateTime toDate = fromDate.AddDays(7);
            var datesQuery = new List<DateTime>();
            var OneWeekForecast = new List<WeatherForecast.WeatherData>();

            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            try
            {
                //foreach (var city in _cities)
                //{

                var forecastInDatabase = await _getDatesForCityQuery.GetDatesForCity(/*city.*/cities[0].Name!, strategy);

                if (!ForecastExist(fromDate, forecastInDatabase))
                {
                    foreach (DateTime date in datesQuery)
                    {
                        var weatherData = await strategy.GetWeatherDataFrom(cities[0] /*city*/, date);
                        weatherData.City = cities[0].Name;
                        OneWeekForecast.Add(weatherData);
                    }
                }
                if (ForecastExist(fromDate, forecastInDatabase))
                {
                    _logger.LogInformation("Weather forecast already fetched from {dataprovider} for {city}\t\tDate: {date}",
                        strategy.GetDataSource(),
                        cities[0].Name,
                        fromDate.Date);
                }
                //}

                return OneWeekForecast;
            }
            catch (Exception e)
            {
                _logger.LogError("{Error}", e);
                throw;
            }
        }
    }
}
