using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastForBackgroundServiceCommand : BaseGetWeatherForecastCommands
    {
        public GetWeatherForecastForBackgroundServiceCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task Get1WeekWeatherForecastForAllCities(List<IGetWeatherDataStrategy<WeatherForecastDto>> weatherDataStrategies)
        {
            DateTime fromDate = DateTime.UtcNow;
            DateTime toDate = fromDate.AddDays(6);
            var datesQuery = new List<DateTime>();

            var getCitiesQuery = new GetCitiesQuery(_config);
            var getDatesQueryDatabase = new GetDatesForCityQuery(_config);

            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            try
            {
                _citiesDatabase = await getCitiesQuery.GetAllCities();


                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    //foreach (var city in _citiesDatabase)
                    //{
                    _datesDatabase = await getDatesQueryDatabase.GetDatesForCity(/*city.*/_citiesDatabase[0].Name!, weatherStrategy);

                        if (DateDoesNotExistInDatabase(fromDate))
                        {
                            foreach (DateTime date in datesQuery)
                            {
                                await GetWeatherDataAndAddToDatabase(date, weatherStrategy, /*city*/_citiesDatabase[0]);
                            }
                        }
                        if (DateExistsInDatabase(fromDate))
                        {
                            Console.WriteLine($"Weather forecast already fetched from {weatherStrategy.GetDataSource()} for {_citiesDatabase[0].Name}\t\tDate: {fromDate.Date}");
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
