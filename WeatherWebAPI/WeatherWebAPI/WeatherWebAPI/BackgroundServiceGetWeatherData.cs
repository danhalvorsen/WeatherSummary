using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetWeatherData : BackgroundService
    {
        private readonly IFactory _factory;
        private readonly IConfiguration _config;
        private readonly WeatherForecastContract _contract;
        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _weatherDataStrategies = new();
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetWeatherData(IConfiguration config, IFactory factory, WeatherForecastContract contract)
        {
            _config = config;
            _factory = factory;
            _contract = contract;
            _weatherDataStrategies.Add(_factory.Build<IYrStrategy>());
            _weatherDataStrategies.Add(_factory.Build<IOpenWeatherStrategy>());
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if(DateTime.UtcNow.Hour <= 12)
                { 

                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine($"{this.GetType().Name}: DOING WORK");
                    
                    var command = new BackgroundServiceGetWeatherDataCommand(_config, _factory);
                    await command.GetOneWeekWeatherForecastForAllCities(_weatherDataStrategies);

                    Console.WriteLine($"{this.GetType().Name} DONE. Waiting {HOUR_DELAY} hours to do work again..");

                    await Task.Delay(new TimeSpan(HOUR_DELAY, 0, 0)); // 24 hours delay
                    //await Task.Delay(1000);
                }
                //await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
