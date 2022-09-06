﻿using AutoMapper;
using System.Globalization;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy;

namespace WeatherWebAPI.DAL
{
    public abstract class BaseFunctionsForQueriesAndCommands
    {
        protected List<CityDto>? _citiesDatabase;
        protected List<WeatherForecast.WeatherData>? _forecasts;

        public BaseFunctionsForQueriesAndCommands()
        {
            
        }


        protected async Task AddCityToDatabase(List<CityDto> city)
        {
            var addCityToDatabaseStrategy = (IAddCityToDatabaseStrategy)Factory!.Build(StrategyType.AddCityToDatabase);
            await addCityToDatabaseStrategy.Add(city);
        }

        protected async Task AddWeatherToDatabaseFor(CityDto city, WeatherForecast.WeatherData weatherData)
        {
            var addWeatherDataToDatabaseStrategy = (IAddWeatherDataToDatabaseStrategy)Factory!.Build(StrategyType.AddWeatherToDatabase);
            await addWeatherDataToDatabaseStrategy.Add(weatherData, city);
        }

        protected async Task<List<CityDto>> GetCityData(string? citySearchedFor)
        {
            var strategy = (IGetCityDataStrategy)Factory!.Build(StrategyType.OpenWeatherGetCity);

            var city = await strategy.GetCityDataFor(citySearchedFor!);
            GetCountryFromAbbreviation(city);

            return city;
        }

        protected bool CityExists(string cityName)
        {
            return _citiesDatabase!.ToList().Any(c => c.Name!.Equals(cityName));
        }

        protected CityDto GetCityDtoBy(string cityName)
        {
            return _citiesDatabase!.Where(c => c.Name!.Equals(cityName)).First();
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
        }

        protected bool ForecastExist(DateTime date) // DateExist()
        {
            return _forecasts!.ToList().Any(i => i.Date.Date.Equals(date.Date));
        }

        protected bool ForecastDoNotExist(DateTime date) // !DateExists()
        {
            return !_forecasts!.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        protected static double CalculateAverageScore(double sum, List<WeatherForecastDto> data)
        {
            var average = !double.IsNaN(sum / data.Count) ? Math.Round((sum / data.Count), 2) : 0;
            return average;
        }

        protected async Task MakeWeatherForecastDto(IMapper mapper, List<WeatherForecastDto> dtoList, string queryString)
        {
            var getWeatherDataFromDatabaseStrategy = (IGetWeatherDataFromDatabaseStrategy)Factory!.Build(StrategyType.GetWeatherDataFromDatabase);
            var weatherForecasts = await getWeatherDataFromDatabaseStrategy.Get(queryString);

            foreach (var weather in weatherForecasts)
            {
                var forecastDto = mapper.Map<WeatherForecastDto>(weather);
                dtoList.Add(forecastDto);
            }
        }

        protected static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1 || firstWeek == 52)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        protected static void GetCountryFromAbbreviation(List<CityDto> city)
        {
            var twoLetterCountryAbbreviation = new CultureInfo(city[0].Country!);
            var countryName = new RegionInfo(twoLetterCountryAbbreviation.Name);
            city[0].Country = countryName.EnglishName;
        }
    }
}
