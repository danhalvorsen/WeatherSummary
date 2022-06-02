using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeatherWebAPI.DAL;
using WeatherWebAPI.Factory;
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
    private readonly IConfiguration config;

    public WeatherforecastController(IConfiguration config)
    {
        this.config = config;
    }

    [HttpGet("DateQueryAndCity")]

    public async Task<ActionResult<List<WeatherForecastDto>>> Day([FromQuery] DateQueryAndCity query)
    {

        //try
        //{

        //}
        //catch (Exception e)
        //{
        //    switch (httpcontext.response.statuscode)
        //    {
        //        case 404:
        //            response.statuscode = 404;
        //            return statuscode(response.statuscode, e.message);
        //        case 500:
        //            response.statuscode = 500;
        //            return statuscode(response.statuscode, e.message);

        //        default:
        //            return statuscode(response.statuscode, response.body);
        //    }
        //    return statuscode(response.statuscode, response.body);
        //    return statuscode(404, e.message); // error not found 404

        //    Console.WriteLine(e.Message);
        //}

        var command = new GetWeatherForecastByDateCommand(config);
        return await command.GetWeatherForecastByDate(query, new List<IWeatherDataStrategy<WeatherForecastDto>> { new YrStrategy<WeatherForecastDto>(), new OpenWeatherStrategy() });
    }

    [HttpGet("BetweenDateQueryAndCity")]
    public async Task<ActionResult<List<WeatherForecastDto>>> Between([FromQuery] BetweenDateQueryAndCity query)
    {
        var command = new GetWeatherForecastBetweenDatesCommand(config);
        return await command.GetWeatherForecastBetweenDates(query, new List<IWeatherDataStrategy<WeatherForecastDto>> { new YrStrategy(), new OpenWeatherStrategy() });

    }

    [HttpGet("Week")]
    public ActionResult<List<WeatherForecastDto>> Week(int week, CityQuery query) // irriterende med liten "w" i week på selve endpointen.
    {
        var command = new GetWeatherForecastByWeekCommand(config);
        return command.GetWeatherForecastByWeek(week, query);
    }

    //[HttpPost("InsertWeatherData")]
    //public ActionResult<WeatherForecastDto> Create(WeatherForecastDto addWeatherData)
    //{
    //    var command = new Commands(config);
    //    return command.AddWeatherDataToDatabase(addWeatherData);
    //}
}
