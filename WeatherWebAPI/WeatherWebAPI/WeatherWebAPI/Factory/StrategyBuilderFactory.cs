using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Factory
{
    public class StrategyBuilderFactory : IFactory
    {
        private readonly IConfiguration config;

        public StrategyBuilderFactory(IConfiguration config)
        {
            this.config = config;
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
                    ConnectionString = config.GetConnectionString("WeatherForecastDatabase")
                });
                return strategy;
            }

            throw new Exception("Find a better Exception");
        }


        // private IMapper? _mapper;
        //private MapperConfiguration? _config;
        //private static HttpClient? httpClient;

        //private MapperConfiguration? CreateConfigForFetchingWeatherData(DateTime queryDate, IWeatherDataStrategy strategy)
        //{
        //    MapperConfiguration config;

        //    if (strategy.GetType() == typeof(YrStrategy))
        //    {
        //            config = new MapperConfiguration(

        //            cfg => cfg.CreateMap<ApplicationYr, WeatherForecastDto>()
        //            .ForPath(dest => dest.Date, opt => opt.MapFrom(src => src.properties.meta.updated_at)) // date
        //            .ForPath(dest => dest.WeatherType, opt => opt // weathertype
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.next_1_hours.summary.symbol_code))
        //            .ForPath(dest => dest.Temperature, opt => opt  // temperature
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.air_temperature))
        //            .ForPath(dest => dest.Windspeed, opt => opt // windspeed
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.wind_speed))
        //            .ForPath(dest => dest.WindDirection, opt => opt // wind direction
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.wind_from_direction))
        //            .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.wind_speed_of_gust))
        //            .ForPath(dest => dest.Pressure, opt => opt // pressure
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.air_pressure_at_sea_level))
        //            .ForPath(dest => dest.Humidity, opt => opt // humidity
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.relative_humidity))
        //            .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.next_1_hours.details.probability_of_precipitation))
        //            .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.next_1_hours.details.precipitation_amount))
        //            .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.cloud_area_fraction))
        //            .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.instant.details.fog_area_fraction))
        //            .ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
        //                .MapFrom(src => src.properties.timeseries
        //                    .ToList()
        //                    .Single(i => i.time.Equals(queryDate))
        //                        .data.next_1_hours.details.probability_of_thunder))
        //            .AfterMap((s, d) => d.Source.DataProvider = strategy.DataSource.ToString().Replace("Strategy", "")));

        //        return config;
        //    }
        //    if (strategy.GetType() == typeof(OpenWeatherStrategy))
        //    {
        //        config = new MapperConfiguration(

        //            cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecastDto>()
        //         .ForPath(dest => dest.Date, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.current.dt.Value))) // date <- this is an UNIX int type
        //         .ForPath(dest => dest.WeatherType, opt => opt // weathertype
        //             .MapFrom(src => src.current.weather[0].description)) // <-- Got a mapper exception once, because the city of stockholm had 2 descriptions. Just made this one
        //                                                                  // enter the first one each time. Should work.
        //                 //.ToList()
        //                 //.Single()
        //                 //   .description))
        //         .ForPath(dest => dest.Temperature, opt => opt  // temperature
        //             .MapFrom(src => src.current.temp))
        //         .ForPath(dest => dest.Windspeed, opt => opt // windspeed
        //             .MapFrom(src => src.current.wind_speed))
        //         .ForPath(dest => dest.WindDirection, opt => opt // wind direction
        //             .MapFrom(src => src.current.wind_deg))
        //         .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
        //             .MapFrom(src => src.current.wind_gust))
        //         .ForPath(dest => dest.Pressure, opt => opt // pressure
        //             .MapFrom(src => src.current.pressure))
        //         .ForPath(dest => dest.Humidity, opt => opt // humidity
        //             .MapFrom(src => src.current.humidity))
        //         .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
        //             .MapFrom(src => src.hourly
        //                .ToList()
        //                .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
        //                    .pop * 100))
        //         .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
        //            .MapFrom(src => src.minutely
        //                .ToList()
        //                .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
        //                    .precipitation))
        //         .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
        //            .MapFrom(src => src.current.clouds))
        //         .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
        //            .MapFrom(src => src.current.visibility))
        //          //.ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
        //          //   .MapFrom(src => src.properties.timeseries
        //          //       .ToList()
        //          //       .Single(i => i.time.Equals(queryDate))
        //          //           .data.next_1_hours.details.probability_of_thunder))
        //          .AfterMap((s, d) => d.Source.DataProvider = strategy.DataSource.ToString().Replace("Strategy", "")) // Adding the datasource name to weatherforceastdto
        //          .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction(d.FogAreaFraction))
        //         );
        //        return config;
        //    }
        //    else
        //        return null;
        //}
        //private MapperConfiguration CreateConfigForFetchingCityCoordinates()
        //{
        //    MapperConfiguration config;

        //    config = new MapperConfiguration(

        //    cfg => cfg.CreateMap<ApplicationOpenWeather, CityDto>()
        //    .ForPath(dest => dest.Name, opt => opt
        //        .MapFrom(src => src.city.name)) // name
        //    .ForPath(dest => dest.Longitude, opt => opt
        //        .MapFrom(src => src.city.lon))
        //    .ForPath(dest => dest.Latitude, opt => opt
        //        .MapFrom(src => src.city.lat))
        //    .ForPath(dest => dest.Country, opt => opt
        //        .MapFrom(src => src.city.country))
        //    );
        //    return config;
        //} // Not in use due to StreamAsync

        //public async Task<WeatherForecastDto> GetWeatherDataFrom(DateTime queryDate, double lat, double lon, IWeatherDataStrategy strategy) // For BetweenDates
        //{
        //    //-- HtppClient getting data from chosen strategy
        //    httpClient = strategy.MakeHttpClientConnection();
        //    var response = await httpClient.GetAsync(strategy.MakeUriWeatherCall(lat, lon));

        //    if (response.IsSuccessStatusCode && strategy.GetType() == typeof(YrStrategy))
        //    {
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

        //        //-- Mapper
        //        TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
        //        queryDate = queryDate.Date + ts;

        //        _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
        //        _mapper = new Mapper(_config);

        //        var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

        //        return resultWeatherData;
        //    }
        //    if (response.IsSuccessStatusCode && strategy.GetType() == typeof(OpenWeatherStrategy))
        //    {
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

        //        //-- Mapper
        //        TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
        //        queryDate = queryDate.Date + ts;

        //        _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
        //        _mapper = new Mapper(_config);

        //        var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

        //        return resultWeatherData;
        //    }
        //    else
        //        return null;
        //}

        //public async Task<WeatherForecastDto> GetWeatherDataFrom(double lat, double lon, IWeatherDataStrategy strategy)
        //{
        //    //-- HtppClient getting data from chosen strategy
        //    httpClient = strategy.MakeHttpClientConnection();
        //    var response = await httpClient.GetAsync(strategy.MakeUriWeatherCall(lat, lon));

        //    if (response.IsSuccessStatusCode && strategy.GetType() == typeof(YrStrategy))
        //    {
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        var weatherData = JsonSerializer.Deserialize<ApplicationYr>(responseBody);

        //        //-- Mapper
        //        DateTime queryDate = DateTime.Now;
        //        TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
        //        queryDate = queryDate.Date + ts;

        //        _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
        //        _mapper = new Mapper(_config);

        //        var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

        //        return resultWeatherData;
        //    }
        //    if (response.IsSuccessStatusCode && strategy.GetType() == typeof(OpenWeatherStrategy))
        //    {
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        var weatherData = JsonSerializer.Deserialize<ApplicationOpenWeather>(responseBody);

        //        //-- Mapper
        //        DateTime queryDate = DateTime.Now;
        //        TimeSpan ts = new TimeSpan((queryDate.Hour + 1), 0, 0); // Setting the query date to get the closest weatherforecast from when the call were made.
        //        queryDate = queryDate.Date + ts;

        //        _config = CreateConfigForFetchingWeatherData(queryDate, strategy);
        //        _mapper = new Mapper(_config);

        //        var resultWeatherData = _mapper.Map<WeatherForecastDto>(weatherData);

        //        return resultWeatherData;
        //    }
        //    else
        //        return null;
        //}

        //public async Task<List<CityDto>> GetCityDataFrom(string city, IWeatherDataStrategy strategy) // Have to use list when using streamasync
        //{
        //    try
        //    {
        //        httpClient = strategy.MakeGeoHttpClientConnection();

        //        // By default BaseAddress = BaseUrl, so we have to change that here.
        //        var response = await httpClient.GetAsync(strategy.MakeGeoUriCityCall(city));
        //        await Task.CompletedTask;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var streamTask = httpClient.GetStreamAsync(strategy.GeoUri);
        //            var cityInfo = await JsonSerializer.DeserializeAsync<List<CityDto>>(await streamTask);

        //            return cityInfo;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return null;
        //}

        // Need to convert from Unix to DateTime when fetching data from OpenWeather datasource and vice versa
        //private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        //{
        //    // Unix timestamp is seconds past epoch
        //    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //    dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        //    return dateTime;
        //}

        //private static int DateTimeToUnixTime(DateTime dateTime)
        //{
        //    dateTime = dateTime.ToUniversalTime(); // If this is not done, the time would be 2 hours ahead of what we'd actually want.
        //    int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        //    return unixTimestamp;
        //}

        //private double VisibilityConvertedToFogAreaFraction(double value)
        //{
        //    return Math.Abs((value / 100) - 100);
        //}

    }
}