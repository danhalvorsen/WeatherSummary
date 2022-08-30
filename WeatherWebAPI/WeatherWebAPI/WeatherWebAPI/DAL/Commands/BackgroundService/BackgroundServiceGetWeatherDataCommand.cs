using WeatherWebAPI.Arguments;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL.Commands.BackgroundService;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{

    public class BackgroundServiceGetWeatherDataCommandConfiguration : IMyConfiguration 
    {

    }

    public class BackgroundServiceGetWeatherDataCommand : BaseFunctionsForQueriesAndCommands
    {
        public BackgroundServiceGetWeatherDataCommand(CommonArgs commonArgs) : base(commonArgs) 
        {

        }

        public async Task GetOneWeekWeatherForecastForAllCities(List<IGetWeatherDataStrategy<WeatherForecast>> weatherDataStrategies)
        {
            DateTime fromDate = DateTime.UtcNow;
            DateTime toDate = fromDate.AddDays(7);
            var datesQuery = new List<DateTime>();

            var getCitiesQuery = new GetCitiesQuery(_commonArgs.Config);
            var getDatesQueryDatabase = new GetDatesForCityQuery(_commonArgs.Config);

            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                datesQuery.Add(day);
            }

            try
            {
                _citiesDatabase = await getCitiesQuery.GetAllCities();

                foreach (var weatherStrategy in weatherDataStrategies)
                {
                    foreach (var city in _citiesDatabase)
                    {
                        _forcasts = await getDatesQueryDatabase.GetDatesForCity(/*city.*/_citiesDatabase[0].Name!, weatherStrategy);

                        if (!AnyForcast(fromDate))
                        {
                            foreach (DateTime date in datesQuery)
                            {
                                await GetWeatherDataAndAddToDatabase(date, weatherStrategy, /*city*/_citiesDatabase[0]);
                            }
                        }
                        if (AnyForcast(fromDate))
                        {
                            Console.WriteLine($"Weather forecast already fetched from {weatherStrategy.GetDataSource()} for {_citiesDatabase[0].Name}\t\tDate: {fromDate.Date}");
                        }
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
