using Microsoft.AspNetCore.Http;
using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class GetWeatherForecastByWeekNumberCommand : BaseCommands
    {
        private readonly WeatherForecastContract _contract;

        public GetWeatherForecastByWeekNumberCommand(IConfiguration config, IFactory factory, WeatherForecastContract contract) : base(config, factory)
        {
            _contract = contract;
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastByWeek(WeekQueryAndCity query)
        {
            string? citySearchedFor = query.CityQuery?.City;
            string? cityName = "";
            DateTime monday = FirstDateOfWeekISO8601(DateTime.UtcNow.Year, query.Week);
            DateTime sunday = monday.AddDays(6);

            var dtoList = new List<WeatherForecastDto>();
            var getCitiesQueryDatabase = new GetCitiesQuery(_config);
            var datesInWeek = new List<DateTime>();

            try
            {
                _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();

                // Making sure the city names are in the right format (Capital Letter + rest of name, eg: Stavanger, not StAvAngeR)
                TextInfo textInfo = new CultureInfo("no", true).TextInfo;
                citySearchedFor = textInfo.ToTitleCase(citySearchedFor!);

                foreach (DateTime day in EachDay(monday, sunday))
                {
                    datesInWeek.Add(day);
                }

                if (!CityExists(citySearchedFor!))
                {
                    var cityData = await GetCityData(citySearchedFor);
                    cityName = cityData[0].Name;

                    if (cityName != "")
                    {
                        if (!CityExists(cityName!))
                        {
                            await AddCityToDatabase(cityData);
                            _citiesDatabase = await getCitiesQueryDatabase.GetAllCities();
                        }
                    }
                    return new();
                }
                else
                {
                    cityName = citySearchedFor;

                    string queryString = $"SET DATEFIRST 1 " +
                                  $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                        $"City.[Name] as CityName, [Source].[Name] as SourceName, Score.Score, Score.ScoreWeighted, Score.FK_WeatherDataId FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                    $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                        $"FULL OUTER JOIN Score ON Score.FK_WeatherDataId = WeatherData.Id " +
                                                            $"WHERE CAST([DateForecast] as date) = CAST([Date] as date) AND DATEPART(week, [DateForecast]) = {query.Week} AND City.Name = '{cityName}' " +
                                                                $"ORDER BY [DateForecast], [Date]";

                    await MakeWeatherForecastDto(dtoList, _contract, queryString);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return dtoList;
        }
    }
}
