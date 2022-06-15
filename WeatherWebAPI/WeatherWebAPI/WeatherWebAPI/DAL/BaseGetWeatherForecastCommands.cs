using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace WeatherWebAPI.DAL
{
    public abstract class BaseGetWeatherForecastCommands
    {
        protected readonly IConfiguration _config;
        protected readonly IFactory _factory;
        protected List<CityDto>? _citiesDatabase;
        protected List<WeatherForecastDto>? _datesDatabase;

        public BaseGetWeatherForecastCommands(IConfiguration config, IFactory factory)
        {
            this._config = config;
            this._factory = factory;
        }

        protected async Task GetCityAndAddToDatabase(string? cityName)
        {
            IGetCityDataStrategy<CityDto> strategy = _factory.Build<IOpenWeatherStrategy>();
            var city = await strategy.GetCityDataFor(cityName);

            IAddCityToDatabaseStrategy addCityToDatabaseStrategy = _factory.Build<IAddCityToDatabaseStrategy>();
            await addCityToDatabaseStrategy.Add(city);
        }

        protected async Task GetWeatherDataAndUpdateDatabase(DateTime date, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy, CityDto city)
        {
            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);

            IUpdateWeatherDataToDatabaseStrategy updateDatabaseStrategy = _factory.Build<IUpdateWeatherDataToDatabaseStrategy>();
            await updateDatabaseStrategy.Update(weatherData, city, date);
        }

        protected async Task GetWeatherDataAndAddToDatabase(DateTime date, IGetWeatherDataStrategy<WeatherForecastDto> weatherStrategy, CityDto city)
        {
            var weatherData = await weatherStrategy.GetWeatherDataFrom(city, date);

            IAddWeatherDataToDatabaseStrategy addToDatabaseStrategy = _factory.Build<IAddWeatherDataToDatabaseStrategy>();
            await addToDatabaseStrategy.Add(weatherData, city);
        }

        protected bool CityExists(string cityName)
        {
            return _citiesDatabase!.ToList().Any(c => c.Name!.Equals(cityName));
        }

        protected CityDto GetCityDtoBy(string cityName)
        {
            return _citiesDatabase!.Where(c => c.Name!.Equals(cityName)).First();
        }

        protected bool UpdateWeatherDataBy(DateTime date)
        {
            return _datesDatabase!.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        protected bool GetWeatherDataBy(DateTime date)
        {
            return !_datesDatabase!.ToList().Any(d => d.Date.Date.Equals(date.Date));
        }

        protected IEnumerable<DateTime> EachDay(DateTime from, DateTime thru) // Between dates
        {
            for (var day = from; day <= thru; day = day.AddDays(1)) // Add .Date if you don't want time to from and thru
                yield return day;
        }
    }
}
