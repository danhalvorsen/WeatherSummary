using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IFactory _factory;
    private readonly WeatherForecastContract _contract;

    private readonly DateQueryAndCityValidator _dateQueryAndCityValidator;
    private readonly CityQueryValidator _cityQueryValidator;
    private readonly DaysQueryValidator _daysQueryValidator;
    private readonly BetweenDateQueryAndCityValidator _beetweenDateQueryAndCityValidator;
    private readonly WeekQueryAndCityValidator _weekQueryAndCityValidator;
    private readonly DaysQueryAndCityValidator _daysQueryAndCityValidator;

    private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _strategies = new();


    public WeatherForecastController(IConfiguration config, IFactory factory, WeatherForecastContract contract,
       DateQueryAndCityValidator dateQueryAndCityValidator,
       CityQueryValidator cityQueryValidator,
       DaysQueryValidator daysQueryValidator,
       BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator,
       WeekQueryAndCityValidator weekQueryAndCityValidator,
       DaysQueryAndCityValidator daysQueryAndCityValidator)
    {
        _config = config;
        _factory = factory;
        _contract = contract;
        _dateQueryAndCityValidator = dateQueryAndCityValidator;
        _cityQueryValidator = cityQueryValidator;
        _daysQueryValidator = daysQueryValidator;
        _beetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
        _weekQueryAndCityValidator = weekQueryAndCityValidator;
        _daysQueryAndCityValidator = daysQueryAndCityValidator;

        _strategies.Add(_factory.Build<IYrStrategy>());
        _strategies.Add(_factory.Build<IOpenWeatherStrategy>());
        _strategies.Add(_factory.Build<IWeatherApiStrategy>());
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


        var command = new GetWeatherForecastPredictionByDate(_config, _factory, _contract);

        return await command.GetWeatherForecastPredictionByDateForOneWeek(query);
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


        var command = new GetWeatherForecastByDateCommand(_config, _factory, _contract);

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

        var command = new GetWeatherForecastBetweenDatesCommand(_config, _factory, _contract);

        return await command.GetWeatherForecastBetweenDates(query);
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

        var command = new GetWeatherForecastByWeekNumberCommand(_config, _factory, _contract);

        return await command.GetWeatherForecastByWeek(query);
    }
}
