using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Factory
{
    public class StrategyBuilderFactory : IFactory
    {
        private readonly IConfiguration _config;

        public StrategyBuilderFactory(IConfiguration config)
        {
            this._config = config;
        }

        public dynamic Build<S>() //Build(GetType(YrStrategy)
        {
            if (typeof(S).Name == typeof(IYrStrategy).Name)
            {
                var strategy = new YrStrategy(new YrConfig());
                return strategy;
            }
            if (typeof(S).Name == typeof(IOpenWeatherStrategy).Name)
            {
                var strategy = new OpenWeatherStrategy(new OpenWeatherConfig());
                return strategy;
            }
            if (typeof(S).Name == typeof(IAddWeatherDataToDatabaseStrategy).Name)
            {
                var strategy = new AddWeatherDataToDatabaseStrategy(new AddWeatherDataToDatabaseConfig
                {
                    ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }
            if (typeof(S).Name == typeof(IUpdateWeatherDataToDatabaseStrategy).Name)
            {
                var strategy = new UpdateWeatherDataToDatabaseStrategy(new UpdateWeatherDataToDatabaseConfig
                {
                    ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }
            if (typeof(S).Name == typeof(IGetWeatherDataFromDatabaseStrategy).Name)
            {
                var strategy = new GetWeatherDataFromDatabaseStrategy(new GetWeatherDataFromDatabaseConfig
                {
                    ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }
            if (typeof(S).Name == typeof(IAddCityToDatabaseStrategy).Name)
            {
                var strategy = new AddCityToDatabaseStrategy(new AddCityToDatabaseConfig
                {
                    ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }
            if (typeof(S).Name == typeof(IAddScoreToDatabaseStrategy).Name)
            {
                var strategy = new AddScoreToDatabaseStrategy(new AddScoreToDatabaseConfig
                {
                    ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }

            throw new Exception("Failed building strategy.");
        }
    }
}