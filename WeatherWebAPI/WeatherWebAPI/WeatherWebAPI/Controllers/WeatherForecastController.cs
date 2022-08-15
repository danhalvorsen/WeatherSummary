using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IFactory _factory;

    private readonly DateQueryAndCityValidator _dateQueryAndCityValidator;
    private readonly CityQueryValidator _cityQueryValidator;
    private readonly DaysQueryValidator _daysQueryValidator;
    private readonly BetweenDateQueryAndCityValidator _beetweenDateQueryAndCityValidator;
    private readonly WeekQueryAndCityValidator _weekQueryAndCityValidator;
    private readonly DaysQueryAndCityValidator _daysQueryAndCityValidator;

    private readonly List<IGetWeatherDataStrategy<WeatherForecastDto>> _strategies = new();


    public WeatherForecastController(IConfiguration config, IFactory factory,
       DateQueryAndCityValidator dateQueryAndCityValidator,
       CityQueryValidator cityQueryValidator,
       DaysQueryValidator daysQueryValidator,
       BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator,
       WeekQueryAndCityValidator weekQueryAndCityValidator,
       DaysQueryAndCityValidator daysQueryAndCityValidator)
    {
        this._config = config;
        this._factory = factory;
        this._dateQueryAndCityValidator = dateQueryAndCityValidator;
        this._cityQueryValidator = cityQueryValidator;
        this._daysQueryValidator = daysQueryValidator;
        this._beetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
        this._weekQueryAndCityValidator = weekQueryAndCityValidator;
        this._daysQueryAndCityValidator = daysQueryAndCityValidator;

        _strategies.Add(this._factory.Build<IYrStrategy>());
        _strategies.Add(this._factory.Build<IOpenWeatherStrategy>());
    }


    [Route("predictionByDate/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> PredictionByDate([FromQuery]DateQueryAndCity query)
    {
        var validationResult = _dateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastPredictionByDate(_config, _factory);

        return await command.GetWeatherForecastPredictionByDateForOneWeek(query, _strategies);
    }

    [Route("date/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Date([FromQuery]DateQueryAndCity query)
    {
        var validationResult = _dateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastByDateCommand(_config, _factory);

        return await command.GetWeatherForecastByDate(query, _strategies);
    }

    [Route("between/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between([FromQuery]BetweenDateQueryAndCity query)
    {
        var validationResult = _beetweenDateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastBetweenDatesCommand(_config, _factory);

        return await command.GetWeatherForecastBetweenDates(query, _strategies);
    }

    [Route("week/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Week([FromQuery]WeekQueryAndCity query)
    {
        var validationResult = _weekQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastByWeekNumberCommand(_config, _factory);

        return await command.GetWeatherForecastByWeek(query, _strategies);
    }
}
