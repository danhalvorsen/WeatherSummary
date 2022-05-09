using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BasicWebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}
