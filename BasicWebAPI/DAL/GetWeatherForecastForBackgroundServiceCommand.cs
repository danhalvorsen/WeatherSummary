using BasicWebAPI.Factory;
using BasicWebAPI.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebAPI.DAL
{
    public class GetWeatherForecastForBackgroundServiceCommand : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastForBackgroundServiceCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public async Task GetWeatherForecastForAllCities(List<IStrategy> getWeatherDataStrategies)
        {
            try
            {
                var getCitiesQuery = new GetCitiesQuery(_config);
                var cities = await getCitiesQuery.GetAllCities();

                foreach (var strategy in getWeatherDataStrategies)
                {
                    foreach (var city in cities)
                    {
                        await (new AddWeatherDataForCityCommand(_config).GetWeatherDataForCity(city, strategy));
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
