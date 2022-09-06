using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Query;

[Route("api/[controller]")]
[ApiController]
public partial class WeatherForecastController : ControllerBase
{
    private readonly DateQueryAndCityValidator _dateQueryAndCityValidator;
    private readonly CityQueryValidator _cityQueryValidator;
    private readonly DaysQueryValidator _daysQueryValidator;
    private readonly BetweenDateQueryAndCityValidator _beetweenDateQueryAndCityValidator;
    private readonly WeekQueryAndCityValidator _weekQueryAndCityValidator;
    private readonly DaysQueryAndCityValidator _daysQueryAndCityValidator;
    private readonly IYrStrategy _yrStrategy;
    private readonly IOpenWeatherStrategy _openWeatherStrategy;

    public WeatherForecastController(
        DateQueryAndCityValidator dateQueryAndCityValidator,
        CityQueryValidator cityQueryValidator,
        DaysQueryValidator daysQueryValidator,
        BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator,
        WeekQueryAndCityValidator weekQueryAndCityValidator,
        DaysQueryAndCityValidator daysQueryAndCityValidator,
        IYrStrategy yrStrategy,
        IOpenWeatherStrategy openWeatherStrategy)
    {
        _dateQueryAndCityValidator = dateQueryAndCityValidator;
        _cityQueryValidator = cityQueryValidator;
        _daysQueryValidator = daysQueryValidator;
        _beetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
        _weekQueryAndCityValidator = weekQueryAndCityValidator;
        _daysQueryAndCityValidator = daysQueryAndCityValidator;
        _yrStrategy = yrStrategy;
        _openWeatherStrategy = openWeatherStrategy;

        //_strategies.Add(args.Common.Factory!.Build(StrategyType.Yr));
        //_strategies.Add(args.Common.Factory!.Build(StrategyType.OpenWeather));
        //_strategies.Add(args.Common.Factory.Build(StrategyType.WeatherApi));
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


        var command = new GetWeatherForecastPredictionByDateQuery();

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


        var command = new GetWeatherForecastByDateQuery();

        return await command.GetWeatherForecastByDate(query, _strategies);
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

        var command = new GetWeatherForecastBetweenDatesQuery(_arguments.Common);

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

        var command = new GetWeatherForecastByWeekNumberQuery(_arguments.Common);

        return await command.GetWeatherForecastByWeek(query);
    }
}
