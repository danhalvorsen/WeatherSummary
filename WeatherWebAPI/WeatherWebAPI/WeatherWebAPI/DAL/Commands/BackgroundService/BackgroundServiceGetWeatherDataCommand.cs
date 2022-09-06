using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class BackgroundServiceGetWeatherDataCommand : BaseFunctionsForQueriesAndCommands, IBackgroundServiceGetWeatherDataCommand
    {
        private readonly List<IStrategy> _strategies = new();
        private readonly ILogger<BackgroundServiceGetWeatherDataCommand> _logger;
        private readonly IYrStrategy _yrStrategy;
        private readonly IOpenWeatherStrategy _openWeatherStrategy;

        public BackgroundServiceGetWeatherDataCommand(ILogger<BackgroundServiceGetWeatherDataCommand> logger, IYrStrategy yrStrategy, IOpenWeatherStrategy openWeatherStrategy) : base()
        {
            _logger = logger;
            _yrStrategy = yrStrategy;
            _openWeatherStrategy = openWeatherStrategy;
        }

        public async Task GetOneWeekWeatherForecastForAllCities()
        {
            DateTime fromDate = DateTime.UtcNow;
            DateTime toDate = fromDate.AddDays(7);
            var datesQuery = new List<DateTime>();

            var getCitiesQuery = new GetCitiesQuery(Config!);
            var getDatesQueryDatabase = new GetDatesForCityQuery(Config!);

            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            try
            {
                _citiesDatabase = await getCitiesQuery.GetAllCities();

                foreach (IGetWeatherDataStrategy strategy in _strategies)
                {
                    //foreach (var city in _citiesDatabase)
                    //{
                    _forecasts = await getDatesQueryDatabase.GetDatesForCity(/*city.*/_citiesDatabase[0].Name!, strategy);

                    if (!ForecastExist(fromDate))
                    {
                        foreach (DateTime date in datesQuery)
                        {
                            var weatherData = await strategy.GetWeatherDataFrom(_citiesDatabase[0] /*city*/, date);
                            await AddWeatherToDatabaseFor(/*city*/_citiesDatabase[0], weatherData);
                        }
                    }
                    if (ForecastExist(fromDate))
                    {
                        _logger.LogInformation("Weather forecast already fetched from {dataprovider} for {city}\t\tDate: {date}",
                            strategy.GetDataSource(),
                            _citiesDatabase[0].Name,
                            fromDate.Date);
                    }
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
