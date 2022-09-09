using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI
{
    public class BackgroundServiceCalculateScore : BackgroundService
    {
        private const int HOUR_DELAY = 24;
        private readonly IAddScoreToDatabaseStrategy _addScoreToDatabaseStrategy;
        private readonly IBackgroundServiceCalculateScoreQuery _query;
        private readonly ILogger<BackgroundServiceCalculateScore> _logger;
        private readonly SetTimeHour _setTime;
        private readonly SetTimeHourValidator _setTimeHourValidator;

        public BackgroundServiceCalculateScore(
            IAddScoreToDatabaseStrategy addScoreToDatabaseStrategy, 
            IBackgroundServiceCalculateScoreQuery query, 
            ILogger<BackgroundServiceCalculateScore> logger,
            SetTimeHour setTime,
            SetTimeHourValidator setTimeHourValidator
            )
        {
            _addScoreToDatabaseStrategy = addScoreToDatabaseStrategy;
            _query = query;
            _logger = logger;
            _setTime = setTime;
            _setTimeHourValidator = setTimeHourValidator;
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

                await Task.Delay(10000, CancellationToken.None);

                DateTime start = _setTime.SetHour(_setTime.StartHour);
                DateTime stop = _setTime.SetHour(_setTime.StopHour);
                
                await DoWork(start, stop, stoppingToken);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
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
                    _logger.LogInformation("{BackgroundServiceGetScore} DOING WORK",
                        this.GetType().Name);
                    
                    var scoreResults = await _query.CalculateScore();
                    await _addScoreToDatabaseStrategy.Add(scoreResults);

                    _logger.LogInformation("{BackgroundServiceGetScore} DONE. Waiting {HOUR_DELAY} hours to do work again..", 
                        this.GetType().Name, 
                        HOUR_DELAY);

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0), stoppingToken); // 24 hours delay
                }
            }
        }
    }
}
