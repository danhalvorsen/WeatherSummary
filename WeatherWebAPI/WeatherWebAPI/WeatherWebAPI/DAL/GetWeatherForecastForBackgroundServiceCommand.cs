using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastForBackgroundServiceCommand : BaseWeatherForecastQuery
    {
        public GetWeatherForecastForBackgroundServiceCommand(IConfiguration config) : base(config)
        {
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
                        await strategy.GetWeatherDataFrom(city, DateTime.Now); // REMEMBER TO ADD WRITING TO DATABASE
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
