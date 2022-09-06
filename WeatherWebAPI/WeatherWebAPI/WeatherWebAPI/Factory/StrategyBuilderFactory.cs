//using AutoMapper;
//using WeatherWebAPI.Factory.Strategy.Database;
//using WeatherWebAPI.Factory.Strategy.OpenWeather;
//using WeatherWebAPI.Factory.Strategy.WeatherApi;
//using WeatherWebAPI.Factory.Strategy.YR;

//namespace WeatherWebAPI.Factory
//{
//    public class BaseFactory<T> : IFactory<T, StrategyType>
//    {

//        public BaseFactory()
//        {
//            EnsureEnumType.EnsureEnum<StrategyType>();
//        }

//        public virtual T Build(StrategyType Type)
//        {
//            throw new NotImplementedException("Method must be overloaded.");
//        }
//    }

//    public class StrategyBuilderFactory : BaseFactory<IStrategy>
//    {
//        private readonly IConfiguration _config;
//        private readonly IMapper _mapper;
//        private readonly ILogger<StrategyBuilderFactory> _logger;

//        public StrategyBuilderFactory(IConfiguration config, IMapper mapper, ILogger<StrategyBuilderFactory> logger)
//        {
//            _config = config;
//            _mapper = mapper;
//            _logger = logger;
//        }

//        public override IStrategy Build(StrategyType strategyType) 
//        {
//            switch (strategyType)
//            {
//                case StrategyType.Yr:
//                    {
//                        var strategy = new YrStrategy(_mapper, new YrConfig(), new HttpClient());
//                        return strategy;
//                    }
//                case StrategyType.OpenWeather:
//                    {
//                        var strategy = new OpenWeatherStrategy(_mapper, new OpenWeatherConfig(), new HttpClient());
//                        return strategy;
//                    }
//                case StrategyType.WeatherApi:
//                    {
//                        var strategy = new WeatherApiStrategy(_mapper, new WeatherApiConfig(), new HttpClient());
//                        return strategy;
//                    }
//                case StrategyType.OpenWeatherGetCity:
//                    {
//                        var strategy = new OpenWeatherFetchCityStrategy(new OpenWeatherConfig(), new HttpClient());
//                        return strategy;
//                    }
//                case StrategyType.AddWeatherToDatabase:
//                    {
//                        var strategy = new AddWeatherDataToDatabaseStrategy(new AddWeatherDataToDatabaseConfig
//                        {
//                            ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//                        });
//                        return strategy;
//                    }
//                case StrategyType.AddCityToDatabase:
//                    {
//                        var strategy = new AddCityToDatabaseStrategy(new AddCityToDatabaseConfig
//                        {
//                            ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//                        });
//                        return strategy;
//                    }
//                case StrategyType.AddScoreToDatabase:
//                    {
//                        var strategy = new AddScoreToDatabaseStrategy(new AddScoreToDatabaseConfig
//                        {
//                            ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//                        });
//                        return strategy;
//                    }
//                case StrategyType.GetWeatherDataFromDatabase:
//                    {
//                        var strategy = new GetWeatherDataFromDatabaseStrategy(new GetWeatherDataFromDatabaseConfig
//                        {
//                            ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//                        });
//                        return strategy;
//                    }
//                case StrategyType.GetScoreFromDatabase:
//                    {
//                        var strategy = new GetScoreFromDatabaseStrategy(new GetScoreFromDatabaseConfig
//                        {
//                            ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//                        });
//                        return strategy;
//                    }
//                default:
//                    {
//                        _logger.LogWarning("{this.GetType().Name} does not contain strategy {strategyType}",
//                            this.GetType().Name,
//                            strategyType);
//                        return null;
//                    }
//            }
//        }
//    }

//    //public class CommandFactory : BaseFactory<IStrategy>
//    //{
//    //    private readonly IConfiguration _config;
//    //    private readonly ILogger<CommandFactory> _logger;

//    //    public CommandFactory(IConfiguration config, ILogger<CommandFactory> logger)
//    //    {
//    //        _config = config;
//    //        _logger = logger;
//    //    }

//    //    public override IStrategy Build(StrategyType strategyType)
//    //    {
//    //        switch (strategyType)
//    //        {
//    //            case StrategyType.AddWeatherToDatabase:
//    //                {
//    //                    var strategy = new AddWeatherDataToDatabaseStrategy(new AddWeatherDataToDatabaseConfig
//    //                    {
//    //                        ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//    //                    });
//    //                    return strategy;
//    //                }
//    //            case StrategyType.AddCityToDatabase:
//    //                {
//    //                    var strategy = new AddCityToDatabaseStrategy(new AddCityToDatabaseConfig
//    //                    {
//    //                        ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//    //                    });
//    //                    return strategy;
//    //                }
//    //            case StrategyType.AddScoreToDatabase:
//    //                {
//    //                    var strategy = new AddScoreToDatabaseStrategy(new AddScoreToDatabaseConfig
//    //                    {
//    //                        ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//    //                    });
//    //                    return strategy;
//    //                }
//    //            default:
//    //                {
//    //                    _logger.LogWarning($"{this.GetType().Name} does not contain strategy {strategyType}");
//    //                    return null;
//    //                }
//    //        }
//    //    }
//    //}

//    //public class QueryFactory : BaseFactory<IStrategy>
//    //{
//    //    private readonly IConfiguration _config;
//    //    private readonly ILogger<QueryFactory> _logger;

//    //    public QueryFactory(IConfiguration config, ILogger<QueryFactory> logger)
//    //    {
//    //        _config = config;
//    //        _logger = logger;
//    //    }

//    //    public override IStrategy Build(StrategyType strategyType)
//    //    {
//    //        switch (strategyType)
//    //        {
//    //            case StrategyType.GetWeatherDataFromDatabase:
//    //                {
//    //                    var strategy = new GetWeatherDataFromDatabaseStrategy(new GetWeatherDataFromDatabaseConfig
//    //                    {
//    //                        ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//    //                    });
//    //                    return strategy;
//    //                }
//    //            case StrategyType.GetScoreFromDatabase:
//    //                {
//    //                    var strategy = new GetScoreFromDatabaseStrategy(new GetScoreFromDatabaseConfig
//    //                    {
//    //                        ConnectionString = _config.GetConnectionString("WeatherForecastDatabase")
//    //                    });
//    //                    return strategy;
//    //                }
//    //            default:
//    //                {
//    //                    _logger.LogWarning($"{this.GetType().Name} does not contain strategy {strategyType}");
//    //                    return null;
//    //                }
//    //        }
//    //    }
//    //}

//    public static class EnsureEnumType
//    {
//        public static void EnsureEnum<T>()
//        {
//            if (!typeof(T).IsEnum)
//                throw new ArgumentException("T must be an enum");
//        }
//    }
//}