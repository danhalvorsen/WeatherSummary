using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using BasicWebAPI.DAL;
using Microsoft.Extensions.Configuration;
using BasicWebAPI.Query;

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
    private readonly IConfiguration config;

    public WeatherforecastController(IConfiguration config)
    {
        this.config = config;
    }

    [HttpGet("DateQueryAndCity")]
    public ActionResult<List<WeatherForecastDto>> Day([FromQuery] DateQueryAndCity query)
    {
        var command = new Commands(config);
        return command.GetWeatherForecastByDate(query);
    }

    [HttpGet("BetweenDateQueryAndCity")]
    public ActionResult<List<WeatherForecastDto>> Between([FromQuery] BetweenDateQueryAndCity query)
    {
        var command = new Commands(config);
        return command.GetWeatherForecastBetweenDates(query);
    }

    [HttpGet("Week")]
    public ActionResult<List<WeatherForecastDto>> Week(int week, CityQuery query) // irriterende med liten "w" i week på selve endpointen.
    {
        var command = new Commands(config);
        return command.GetWeatherForecastByWeek(week, query);
    }

    [HttpPost("InsertWeatherData")]
    public ActionResult<WeatherForecastDto> Create(WeatherForecastDto addWeatherData)
    {
        var command = new Commands(config);
        return command.AddWeatherDataToWeatherDataAndSourceWeatherDataTable(addWeatherData);
    }

}
