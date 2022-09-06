using WeatherWebAPI.DAL;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetWeatherData : BackgroundService
    {
        private readonly BackgroundServiceGetWeatherDataCommand _command;
        private readonly ILogger<BackgroundServiceGetWeatherData> _logger;
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetWeatherData(BackgroundServiceGetWeatherDataCommand command, ILogger<BackgroundServiceGetWeatherData> logger)
        {
            _command = command;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                    DateTime start = DateTime.UtcNow.Date + new TimeSpan(06, 0, 0);
                    DateTime stop = DateTime.UtcNow.Date + new TimeSpan(18, 0, 0);

                    await DoWork(start, stop, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task DoWork(DateTime StartTime, DateTime StopTime, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow > StartTime && DateTime.UtcNow < StopTime)
                {
                    _logger.LogInformation("{BackgroundServiceGetWeatherData} DOING WORK",
                        this.GetType().Name);

                    await _command.GetOneWeekWeatherForecastForAllCities();

                    _logger.LogInformation("{BackgroundServiceGetWeatherData} DONE. Waiting {HOUR_DELAY} hours to do work again..",
                        this.GetType().Name,
                        HOUR_DELAY);

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0), stoppingToken); // 24 hours delay
                }
            }
        }
    }
}
