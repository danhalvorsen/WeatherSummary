using BasicWebAPI.DAL;
using BasicWebAPI.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicWebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        private GetWeatherDataFactory factory;
        private IStrategy strategy;

        private readonly IConfiguration config;

        public MyBackgroundService(IConfiguration config)
        {
            this.config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            factory = new GetWeatherDataFactory();
            strategy = new YrStrategy();
            var command = new Commands(config);

            while (!stoppingToken.IsCancellationRequested) {
                Console.WriteLine("Henter data fra Yr.no");

                //-- Getting the data from Yr (Only from Stavanger at this point in time)
                var result = await factory.GetWeatherDataFrom(strategy);
                
                //-- Adding data from Yr to the database
                //command.AddWeatherDataToWeatherDataAndSourceWeatherDataTable(result);
                Console.WriteLine(result.ToString());


                await Task.Delay(new TimeSpan(24, 0, 0)); // 24 hours delay
            }
            await Task.CompletedTask;

            
        }
    }
}
