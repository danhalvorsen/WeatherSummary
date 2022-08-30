using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Arguments;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

[Route("api/[controller]")]
[ApiController]
public partial class WeatherForecastController : ControllerBase
{
   
    private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _strategies = new();
    private readonly Args _arguments;

    public WeatherForecastController(Args args)
    {
        _strategies.Add(args.Common.Factory.Build<IYrStrategy>());
        _strategies.Add(args.Common.Factory.Build<IOpenWeatherStrategy>());
        _strategies.Add(args.Common.Factory.Build<IWeatherApiStrategy>());
        _arguments = args;
    }


    [Route("predictionByDate/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> PredictionByDate([FromQuery]DateQueryAndCity query)
    {
        var validationResult = _arguments.DateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastPredictionByDateQuery(_arguments.Common);

        return await command.GetWeatherForecastPredictionByDateForOneWeek(query);
    }

    [Route("date/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Date([FromQuery]DateQueryAndCity query)
    {
        var validationResult = _arguments.DateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);


        var command = new GetWeatherForecastByDateQuery(_arguments.Common);

        return await command.GetWeatherForecastByDate(query, _strategies);
    }

    [Route("between/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between([FromQuery]BetweenDateQueryAndCity query)
    {
        var validationResult = _arguments.BeetweenDateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastBetweenDatesQuery(_arguments.Common);

        return await command.GetWeatherForecastBetweenDates(query);
    }

    [Route("week/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Week([FromQuery]WeekQueryAndCity query)
    {
        var validationResult = _arguments.WeekQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastByWeekNumberQuery(_arguments.Common);

        return await command.GetWeatherForecastByWeek(query);
    }
}
