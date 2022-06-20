using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IFactory _factory;
        private readonly IConfiguration _config;
        private readonly List<IGetWeatherDataStrategy<WeatherForecastDto>> _weatherDataStrategies = new();

        public MyBackgroundService(IConfiguration config, IFactory factory)
        {
            this._config = config;
            this._factory = factory;
            _weatherDataStrategies.Add(this._factory.Build<IYrStrategy>());
            _weatherDataStrategies.Add(this._factory.Build<IOpenWeatherStrategy>());
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("BackgroundService doing work");
                    
                    var command = new GetWeatherForecastForBackgroundServiceCommand(_config, _factory);
                    await command.GetWeatherForecastForAllCities(_weatherDataStrategies);

                    await Task.Delay(new TimeSpan(24, 0, 0)); // 24 hours delay
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
