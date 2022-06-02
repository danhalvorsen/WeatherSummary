namespace WeatherWebAPI
{
    public class MyBackgroundService : BackgroundService
    {

        private readonly IConfiguration config;

        public MyBackgroundService(IConfiguration config)
        {
            this.config = config;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    //Console.WriteLine("BackgroundService doing work");

                    //var command = new GetWeatherForecastForBackgroundServiceCommand(config);
                    //await command.GetWeatherForecastForAllCities(new List<IStrategy> { new YrStrategy(), new OpenWeatherStrategy() });

                    //await Task.Delay(new TimeSpan(24, 0, 0)); // 24 hours delay
                    await Task.Delay(1000);
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
