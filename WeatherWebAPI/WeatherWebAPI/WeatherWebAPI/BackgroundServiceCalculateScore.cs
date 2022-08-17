using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
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
        private readonly WeatherForecastContract _contract;
        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _weatherDataStrategies = new();
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetScore(IConfiguration config, IFactory factory, WeatherForecastContract contract)
        {
            _config = config;
            _factory = factory;
            _contract = contract;
            _weatherDataStrategies.Add(_factory.Build<IYrStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IOpenWeatherStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IWeatherApiStrategy>());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(10000);

                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine($"{this.GetType().Name} DOING WORK");

                    var command = new BackgroundServiceCalculateScoreCommand(_config, _factory);
                    await command.CalculateScore();

                    Console.WriteLine($"{this.GetType().Name} DONE. Waiting {HOUR_DELAY} hours to do work again..");

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0)); // 24 hours delay
                    //await Task.Delay(1000);
                }
                //await Task.CompletedTask;
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
    }
}
