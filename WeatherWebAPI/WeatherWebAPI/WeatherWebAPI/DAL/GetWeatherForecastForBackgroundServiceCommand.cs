using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastForBackgroundServiceCommand : BaseWeatherForecastQuery
    {
        private readonly IFactory factory;

        public GetWeatherForecastForBackgroundServiceCommand(IConfiguration config, IFactory factory) : base(config)
        {
            this.factory = factory;
        }

        public async Task GetWeatherForecastForAllCities(List<IGetWeatherDataStrategy<WeatherForecastDto>> strategies)
        {
            try
            {
                var getCitiesQuery = new GetCitiesQuery(config);
                var cities = await getCitiesQuery.GetAllCities();

                foreach (var strategy in strategies)
                {
                    foreach (var city in cities)
                    {
                        await (new AddWeatherDataForCityCommand(config, factory).GetWeatherDataForCity(city, strategy));
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
