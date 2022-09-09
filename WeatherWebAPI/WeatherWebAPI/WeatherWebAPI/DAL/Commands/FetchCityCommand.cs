using System.Globalization;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Commands
{
    public class FetchCityCommand : BaseFunctionsForQueriesAndCommands, IFetchCityCommand
    {
        private readonly ILogger<FetchCityCommand> _logger;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IOpenWeatherFetchCityStrategy _fetchCityStrategy;

        public FetchCityCommand(
            ILogger<FetchCityCommand> logger,
            IGetCitiesQuery getCitiesQuery, 
            IOpenWeatherFetchCityStrategy fetchCityStrategy
            )
        {
            _logger = logger;
            _getCitiesQuery = getCitiesQuery;
            _fetchCityStrategy = fetchCityStrategy;
        }

        public async Task<List<CityDto>> FetchCity(CityQuery query)
        {
            string? citySearchedFor = query.City;

            try
            {
                var cities = await _getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                var textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                if (!CityExists(citySearchedFor!, cities))
                {
                    var city = await _fetchCityStrategy.GetCityDataFor(citySearchedFor);

                    var twoLetterCountryAbbreviation = new CultureInfo(city[0].Country!);
                    var countryName = new RegionInfo(twoLetterCountryAbbreviation.Name);
                    city[0].Country = countryName.EnglishName;

                    return city;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("{Error}", e);
                throw;
            }

            throw new Exception($"Could not fetch city information for: {citySearchedFor}");
        }
    }
}
