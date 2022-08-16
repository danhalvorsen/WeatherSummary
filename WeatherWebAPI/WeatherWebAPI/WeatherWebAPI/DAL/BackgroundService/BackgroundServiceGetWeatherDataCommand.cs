using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class BackgroundServiceGetWeatherDataCommand : BaseCommands
    {
        public BackgroundServiceGetWeatherDataCommand(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task GetOneWeekWeatherForecastForAllCities(List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
            DateTime fromDate = DateTime.UtcNow;
            DateTime toDate = fromDate.AddDays(7);
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
