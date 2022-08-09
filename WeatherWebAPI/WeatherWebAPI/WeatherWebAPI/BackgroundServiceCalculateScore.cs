﻿using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI
{
    public class BackgroundServiceGetScore : BackgroundService
    {
        private readonly IFactory _factory;
        private readonly IConfiguration _config;
        private readonly List<IGetWeatherDataStrategy<WeatherForecastDto>> _weatherDataStrategies = new();
        private const int HOUR_DELAY = 24;

        public BackgroundServiceGetScore(IConfiguration config, IFactory factory)
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
