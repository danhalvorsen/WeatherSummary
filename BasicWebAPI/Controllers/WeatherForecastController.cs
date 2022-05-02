using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

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

    [HttpGet("{date:DateTime}")]
    public ActionResult<WeatherforecastDto> Day(DateTime date)
    {
        return new WeatherforecastDto
        {
            Id = 1,
            City = "Stavanger",
            Date = DateTime.Now.Date,
            Temperature = 14.3F,
            Windspeed = 2.5F,
            WindDirection = 293.8F,
            WindspeedGust = 5.6F,
            Pressure = 1023.7F,
            Humidity = 42.4F,
            ProbOfRain = 0F,
            AmountRain = 0F,
            CloudAreaFraction = 13.5F,
            FogAreaFraction = 0F,
            ProbOfThunder = 0F
        };
    }

    [HttpGet("{between}")]
    public ActionResult<List<WeatherforecastDto>> Between(DateTime from, DateTime to)
    {
        string getStartDate = from.ToString().Substring(0, 5);
        string getEndDate = to.ToString().Substring(0, 5);
        double numberOfDays = 0;

        if (getStartDate.Substring(3, 2) == getEndDate.Substring(3, 2))
        {
            double x = Convert.ToDouble(getStartDate.Substring(0, 2));
            double y = Convert.ToDouble(getEndDate.Substring(0, 2));

            numberOfDays = y - x;

            var listDays = new List<WeatherforecastDto>();

            for (int i = 0; i <= numberOfDays; i++)
            {
                listDays.Add(new WeatherforecastDto
                {
                    Id = i + 1,
                    City = "Stavanger",
                    Date = DateTime.Now.Date, //($"0{x + i}" + from.ToString().Substring(2)),
                    Temperature = 14.3F,
                    Windspeed = 2.5F,
                    WindDirection = 293.8F,
                    WindspeedGust = 5.6F,
                    Pressure = 1023.7F,
                    Humidity = 42.4F,
                    ProbOfRain = 0F,
                    AmountRain = 0F,
                    CloudAreaFraction = 13.5F,
                    FogAreaFraction = 0F,
                    ProbOfThunder = 0F
                });
            }

            return listDays;
        }

        return null;

 

        //else
        //{
            //if(getStartDate.Substring(2, 4) != getEndDate.Substring(2, 4))
            //{
            //    double xmonths = Convert.ToDouble(getStartDate.Substring(2, 4));
            //    double ymonths = Convert.ToDouble(getEndDate.Substring(2, 4));

            //    if(xmonths % 2 == 0 || ymonths % 2 == 0)
            //    {
                    
            //    }
            //}
        //}
    }

}
