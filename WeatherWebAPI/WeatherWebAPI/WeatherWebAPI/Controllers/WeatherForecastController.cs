using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
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
    private List<IGetWeatherDataStrategy<WeatherForecastDto>> _strategies = new();


    public WeatherforecastController(IConfiguration config, IFactory factory)
    {
        this._config = config;
        this._factory = factory;
        _strategies.Add(this._factory.Build<IYrStrategy>());
        _strategies.Add(this._factory.Build<IOpenWeatherStrategy>());
    }

    [Route("api/weatherforecast/date/")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Date(DateQueryAndCity query)
    {
        var command = new GetWeatherForecastByDateCommand(_config, _factory);
        
        return await command.GetWeatherForecastByDate(query, _strategies);
    }

    [Route("api/weatherforecast/between/")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between(BetweenDateQueryAndCity query)
    {
        var command = new GetWeatherForecastBetweenDatesCommand(_config, _factory);

        return await command.GetWeatherForecastBetweenDates(query, _strategies);
    }

    [Route("api/weatherforecast/week/")]
    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> Week(
        [Required()] int week,
            CityQuery query) // irriterende med liten "w" i week på selve endpointen.
    {
        var command = new GetWeatherForecastByWeekCommand(_config, _factory);
        
        return await command.GetWeatherForecastByWeek(week, query, _strategies);
    }

    //[HttpPost("InsertWeatherData")]
    //public ActionResult<WeatherForecastDto> Create(WeatherForecastDto addWeatherData)
    //{
    //    var command = new Commands(config);
    //    return command.AddWeatherDataToDatabase(addWeatherData);
    //}
}
