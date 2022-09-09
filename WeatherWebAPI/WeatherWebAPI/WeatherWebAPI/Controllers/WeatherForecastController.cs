using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Query;

[Route("api/[controller]")]
[ApiController]
public partial class WeatherForecastController : ControllerBase
{
    private readonly List<IGetWeatherDataStrategy>? _weatherDataStrategies = new();
    private readonly IMapper _mapper;
    private readonly IGetCitiesQuery _getCitiesQuery;
    private readonly IOpenWeatherFetchCityStrategy _openWeatherFetchCityStrategy;
    private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;
    private readonly DateQueryAndCityValidator _dateQueryAndCityValidator;
    private readonly BetweenDateQueryAndCityValidator _beetweenDateQueryAndCityValidator;
    private readonly WeekQueryAndCityValidator _weekQueryAndCityValidator;

    public WeatherForecastController(
        IMapper mapper,
        IGetCitiesQuery getCitiesQuery,
        IOpenWeatherFetchCityStrategy openWeatherFetchCityStrategy,
        IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy,
        DateQueryAndCityValidator dateQueryAndCityValidator,
        BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator,
        WeekQueryAndCityValidator weekQueryAndCityValidator,
        StrategyResolver strategyPicker)
    {
        _mapper = mapper;
        _getCitiesQuery = getCitiesQuery;
        _openWeatherFetchCityStrategy = openWeatherFetchCityStrategy;
        _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
        _dateQueryAndCityValidator = dateQueryAndCityValidator;
        _beetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
        _weekQueryAndCityValidator = weekQueryAndCityValidator;

        _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.Yr));
        _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.OpenWeather));
    }


    [Route("predictionByDate/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> PredictionByDate([FromQuery] DateQueryAndCity query)
    {
        var validationResult = _dateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastPredictionByDateQuery(_mapper, _getCitiesQuery, _openWeatherFetchCityStrategy, _getWeatherDataFromDatabaseStrategy);

        return await command.GetWeatherForecastPredictionByDateForOneWeek(query);
    }

    [Route("date/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Date([FromQuery] DateQueryAndCity query)
    {
        var validationResult = _dateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastByDateQuery(_mapper, _getCitiesQuery, _openWeatherFetchCityStrategy, _getWeatherDataFromDatabaseStrategy);

        return await command.GetWeatherForecastByDate(query, _weatherDataStrategies);
    }

    [Route("between/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between([FromQuery] BetweenDateQueryAndCity query)
    {
        var validationResult = _beetweenDateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastBetweenDatesQuery(_mapper, _getCitiesQuery, _openWeatherFetchCityStrategy, _getWeatherDataFromDatabaseStrategy);

        return await command.GetWeatherForecastBetweenDates(query);
    }

    [Route("week/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Week([FromQuery] WeekQueryAndCity query)
    {
        var validationResult = _weekQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastByWeekNumberQuery(_mapper, _getCitiesQuery, _openWeatherFetchCityStrategy, _getWeatherDataFromDatabaseStrategy);

        return await command.GetWeatherForecastByWeek(query);
    }
}
