using Microsoft.AspNetCore.Mvc;
using System;

public class WeatherforecastController : ControllerBase
{

    [HttpGet("{date:DateTime}")]
    public ActionResult<WeatherforecastDto> Day(DateTime date)
    {
        return new WeatherforecastDto
        {
            DateTime = "01.01.2022"
        };
    }

}
