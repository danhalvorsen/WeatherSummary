using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetScore : BackgroundService
    {
        private readonly IFactory _factory;
        private readonly IConfiguration _config;
        private readonly BackgroundServiceCalculateScoreCommand _command;
        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _weatherDataStrategies = new();
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetScore(
            IConfiguration config, 
            IFactory factory,
            BackgroundServiceCalculateScoreCommand command)
        {
            _config = config;
            _factory = factory;
            _command = command;
            _weatherDataStrategies.Add(_factory.Build<IYrStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IOpenWeatherStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IWeatherApiStrategy>());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(10000);

                DateTime start = DateTime.UtcNow.Date + new TimeSpan(06, 0, 0);
                DateTime stop = DateTime.UtcNow.Date + new TimeSpan(18, 0, 0);
                
                await StartWork(start, stop, stoppingToken);
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

        private async Task StartWork(DateTime StartTime, DateTime StopTime, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow > StartTime && DateTime.UtcNow < StopTime)
                {
                    Console.WriteLine($"{this.GetType().Name} DOING WORK");
                    
                    await _command.CalculateScore();
                    
                    Console.WriteLine($"{this.GetType().Name} DONE. Waiting {HOUR_DELAY} hours to do work again..");

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0)); // 24 hours delay
                }
            }
        }
    }
}
