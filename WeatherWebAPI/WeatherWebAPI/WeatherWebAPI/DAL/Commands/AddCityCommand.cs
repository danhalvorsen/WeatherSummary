using System.Globalization;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.Commands
{
    public class AddCityCommand : BaseFunctionsForQueriesAndCommands
    {
        private readonly IGetCitiesQuery _getCitiesQuery;

        public AddCityCommand(IGetCitiesQuery getCitiesQuery) : base()
        {
            _getCitiesQuery = getCitiesQuery;
        }

        public async Task AddCity(CityQuery query)
        {
            string? citySearchedFor = query.City;
            string? cityName;

            try
            {
                _citiesDatabase = await _getCitiesQuery.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;

                    if (cityName != "")
                    {
                        if (!CityExists(cityName!))
                        {
                            await AddCityToDatabase(cityData);
                        }
                    }
                }
                else
                {
                    cityName = citySearchedFor;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
