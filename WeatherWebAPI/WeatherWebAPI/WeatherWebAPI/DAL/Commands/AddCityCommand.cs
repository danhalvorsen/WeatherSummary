using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Commands
{
    public class AddCityCommand : BaseFunctionsForQueriesAndCommands, IAddCityCommand
    {
        private readonly IFetchCityCommand _fetchCityCommand;
        private readonly IAddCityToDatabaseStrategy _addCityToDatabaseStrategy;

        public AddCityCommand(
            IFetchCityCommand fetchCityCommand,
            IAddCityToDatabaseStrategy addCityToDatabaseStrategy
            ) : base()
        {
            _fetchCityCommand = fetchCityCommand;
            _addCityToDatabaseStrategy = addCityToDatabaseStrategy;
        }

        public async Task AddCity(CityQuery query)
        {
            try
            {
                var city = await _fetchCityCommand.FetchCity(query);

                if (city != null)
                {
                    await _addCityToDatabaseStrategy.Add(city);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
