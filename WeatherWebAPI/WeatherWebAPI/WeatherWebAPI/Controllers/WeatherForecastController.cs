using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

public class ResponseHeaderAttribute : ActionFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public ResponseHeaderAttribute(string name, string value) =>
        (_name, _value) = (name, value);

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(_name, _value);

        base.OnResultExecuting(context);
    }
}

[ResponseHeader("Access-Control-Allow-Origin", "*")]
[ResponseHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept")]
public class WeatherforecastController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IFactory _factory;

    private readonly DateQueryAndCityValidator _dateQueryAndCityValidator;
    private readonly CityQueryValidator _cityQueryValidator;
    private readonly BetweenDateQueryAndCityValidator _beetweenDateQueryAndCityValidator;
    private readonly WeekQueryAndCityValidator _weekQueryAndCityValidator;

    private readonly List<IGetWeatherDataStrategy<WeatherForecastDto>> _strategies = new();


    public WeatherforecastController(IConfiguration config, IFactory factory, 
       DateQueryAndCityValidator dateQueryAndCityValidator,
       CityQueryValidator cityQueryValidator,
       BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator,
       WeekQueryAndCityValidator weekQueryAndCityValidator)
    {
        this._config = config;
        this._factory = factory;
        this._dateQueryAndCityValidator = dateQueryAndCityValidator;
        this._cityQueryValidator = cityQueryValidator;
        this._beetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
        this._weekQueryAndCityValidator = weekQueryAndCityValidator;

        _strategies.Add(this._factory.Build<IYrStrategy>());
        _strategies.Add(this._factory.Build<IOpenWeatherStrategy>());
    }

    [Route("api/weatherforecast/date/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Date(DateQueryAndCity query)
    {
        var validationResult = _dateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        

        var command = new GetWeatherForecastByDateCommand(_config, _factory);
        
        return await command.GetWeatherForecastByDate(query, _strategies);
    }

    [Route("api/weatherforecast/between/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between(BetweenDateQueryAndCity query)
    {
        var validationResult = _beetweenDateQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = new GetWeatherForecastBetweenDatesCommand(_config, _factory);

        return await command.GetWeatherForecastBetweenDates(query, _strategies);
    }

    [Route("api/weatherforecast/week/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Week(WeekQueryAndCity query)
    {
        var validationResult = _weekQueryAndCityValidator.Validate(query);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = new GetWeatherForecastByWeekNumberCommand(_config, _factory);
        
        return await command.GetWeatherForecastByWeek(query, _strategies);
    }

    [Route("api/weatherforecast/getCitiesInDatabase/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetCitiesFromDatabase()
    {
        var command = new GetCitiesQuery(_config);
        return await command.GetAllCities();
    }
}
