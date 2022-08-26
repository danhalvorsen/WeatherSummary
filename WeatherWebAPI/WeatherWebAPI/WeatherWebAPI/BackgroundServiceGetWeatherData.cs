using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetWeatherData : BackgroundService
    {
        private readonly IFactory _factory;
        private readonly IConfiguration _config;
        private readonly WeatherForecastMapping _contract;
        private readonly BackgroundServiceGetWeatherDataCommand _command;
        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _weatherDataStrategies = new();
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetWeatherData(IConfiguration config, 
            IFactory factory, 
            WeatherForecastMapping contract,
            BackgroundServiceGetWeatherDataCommand command)
        {
            _config = config;
            _factory = factory;
            _contract = contract;
            _command = command;
            _weatherDataStrategies.Add(_factory.Build<IYrStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IOpenWeatherStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IWeatherApiStrategy>());
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    DateTime start = DateTime.UtcNow.Date + new TimeSpan(14, 0, 0);
                    DateTime stop = DateTime.UtcNow.Date + new TimeSpan(15, 0, 0);

                    await StartWork(stoppingToken, start, stop, _config, _factory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task StartWork(CancellationToken stoppingToken, DateTime StartTime, DateTime StopTime, IConfiguration config, IFactory factory)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow > StartTime && DateTime.UtcNow < StopTime)
                {
                    Console.WriteLine($"{this.GetType().Name} DOING WORK");

                    await _command.GetOneWeekWeatherForecastForAllCities(_weatherDataStrategies);

                    Console.WriteLine($"{this.GetType().Name} DONE. Waiting {HOUR_DELAY} hours to do work again..");

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0)); // 24 hours delay
                }
            }
        }
    }
}
