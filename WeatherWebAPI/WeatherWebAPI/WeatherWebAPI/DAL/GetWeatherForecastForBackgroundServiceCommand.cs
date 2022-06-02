using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastForBackgroundServiceCommand : BaseWeatherForecastQuery
    {
        private readonly GetWeatherDataFactory _factory;

        public GetWeatherForecastForBackgroundServiceCommand(IConfiguration config) : base(config)
        {
            this._factory = new GetWeatherDataFactory();
        }

        public async Task GetWeatherForecastForAllCities(List<IWeatherDataStrategy> getWeatherDataStrategies)
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
                Console.WriteLine(e.Message);
            }
        }
    }
}
