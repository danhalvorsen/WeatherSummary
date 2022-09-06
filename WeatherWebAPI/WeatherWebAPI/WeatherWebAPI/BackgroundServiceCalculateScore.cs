using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetScore : BackgroundService
    {
        private const int HOUR_DELAY = 24;
        private readonly IAddScoreToDatabaseStrategy _addScoreToDatabaseStrategy;
        private readonly BackgroundServiceCalculateScoreQuery _query;
        private readonly ILogger<BackgroundServiceGetScore> _logger;

        public BackgroundServiceGetScore(IAddScoreToDatabaseStrategy addScoreToDatabaseStrategy , BackgroundServiceCalculateScoreQuery query, ILogger<BackgroundServiceGetScore> logger)
        {
            _addScoreToDatabaseStrategy = addScoreToDatabaseStrategy;
            _query = query;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(10000, CancellationToken.None);

                DateTime start = DateTime.UtcNow.Date + new TimeSpan(06, 0, 0);
                DateTime stop = DateTime.UtcNow.Date + new TimeSpan(18, 0, 0);
                
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

                    //var addScoreToDatabaseStrategy = (IAddScoreToDatabaseStrategy)Factory.Build(StrategyType.AddScoreToDatabase);
                    //await addScoreToDatabaseStrategy.Add(scoreResults);

                    _logger.LogInformation("{BackgroundServiceGetScore} DONE. Waiting {HOUR_DELAY} hours to do work again..", 
                        this.GetType().Name, 
                        HOUR_DELAY);

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0), stoppingToken); // 24 hours delay
                }
            }
        }
    }
}
