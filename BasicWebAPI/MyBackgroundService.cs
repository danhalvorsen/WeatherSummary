using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicWebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
                Console.WriteLine("Hey!");
                await Task.Delay(new TimeSpan(0, 0, 5)); // 5 second delay
            }
            await Task.CompletedTask;
        }
    }
}
