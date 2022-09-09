using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Query;

namespace WeatherWebAPI
{
    public partial class BackgroundServiceGetWeatherData : BackgroundService
    {
        private const int HOUR_DELAY = 24;
        private readonly List<IGetWeatherDataStrategy>? _weatherDataStrategies = new();

        private readonly ILogger<BackgroundServiceGetWeatherData> _logger;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IAddWeatherDataToDatabaseStrategy _addWeatherDataToDatabaseStrategy;
        private readonly IBackgroundServiceFetchWeatherDataQuery _query;
        private readonly SetTimeHourValidator _setTimeHourValidator;
        private readonly SetTimeHour _setTime;

        public BackgroundServiceGetWeatherData(
            ILogger<BackgroundServiceGetWeatherData> logger,
            IGetCitiesQuery getCitiesQuery, 
            IAddWeatherDataToDatabaseStrategy addWeatherDataToDatabaseStrategy,
            IBackgroundServiceFetchWeatherDataQuery query,
            SetTimeHourValidator setTimeHourValidator,
            SetTimeHour setTime,
            StrategyResolver strategyPicker
            )
        {
            _logger = logger;
            _getCitiesQuery = getCitiesQuery;
            _addWeatherDataToDatabaseStrategy = addWeatherDataToDatabaseStrategy;
            _query = query;
            _setTimeHourValidator = setTimeHourValidator;
            _setTime = setTime;
            _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.Yr));
            _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.OpenWeather));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _setTime.StartHour = 06;
            _setTime.StopHour = 18;

            try
            {
                var validationResult = _setTimeHourValidator.Validate(_setTime);
                if (!validationResult.IsValid)
                    _logger.LogError("{Errors}", validationResult.Errors);

                DateTime start = _setTime.SetHour(_setTime.StartHour);
                DateTime stop = _setTime.SetHour(_setTime.StopHour);

                await DoWork(start, stop, stoppingToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DoWork(DateTime StartTime, DateTime StopTime, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow > StartTime && DateTime.UtcNow < StopTime)
                {
                    _logger.LogInformation("{BackgroundServiceGetWeatherData} DOING WORK", this.GetType().Name);

                    var cities = await _getCitiesQuery.GetAllCities();

                    foreach (var strategy in _weatherDataStrategies!)
                    {
                        var weatherForecasts = await _query.GetOneWeekWeatherForecastForAllCitiesFor(strategy, cities);
                        await _addWeatherDataToDatabaseStrategy.Add(weatherForecasts);
                    }

                    _logger.LogInformation("{BackgroundServiceGetWeatherData} DONE. Waiting {HOUR_DELAY} hours to do work again..",
                        this.GetType().Name,
                        HOUR_DELAY);

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0), stoppingToken); // 24 hours delay
                }
            }
        }
    }
}
