using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IFactory factory;
        private readonly IConfiguration config;
        private List<IGetWeatherDataStrategy<WeatherForecastDto>> strategies = new List<IGetWeatherDataStrategy<WeatherForecastDto>>();

        public MyBackgroundService(IConfiguration config, IFactory factory)
        {
            this.config = config;
            this.factory = factory;
            strategies.Add(this.factory.Build<IYrStrategy>());
            strategies.Add(this.factory.Build<IOpenWeatherStrategy>());
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("BackgroundService doing work");

                    var command = new GetWeatherForecastForBackgroundServiceCommand(config, factory);
                    await command.GetWeatherForecastForAllCities(strategies);

                    await Task.Delay(new TimeSpan(24, 0, 0)); // 24 hours delay
                    //await Task.Delay(1000);
                }
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
