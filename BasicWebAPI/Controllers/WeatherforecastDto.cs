using BasicWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public class WeatherForecastDto
{
    public WeatherForecastDto()
    {
        Source = new WeatherSourceDto();
    }

    public WeatherForecastDto(DateTime date, float temperature, float windspeed, float windDirection,
        float windspeedGust, float pressure, float humidity, float probOfRain, float amountRain, float cloudAreaFraction, float fogAreaFraction, float probOfThunder, string city, string weatherType)
    {
        //Id = id;
        //FK_CityId = cityId;
        Date = date;
        Temperature = temperature;
        Windspeed = windspeed;
        WindDirection = windDirection;
        WindspeedGust = windspeedGust;
        Pressure = pressure;
        Humidity = humidity;
        ProbOfRain = probOfRain;
        AmountRain = amountRain;
        CloudAreaFraction = cloudAreaFraction;
        FogAreaFraction = fogAreaFraction;
        ProbOfThunder = probOfThunder;
        City = city;
        WeatherType = weatherType;
        Source = new WeatherSourceDto();
    }

    //public int Id { get; set; }
    //public int FK_CityId { get; set; }
    public string City { get; set; }
    public DateTime Date { get; set; }
    public string WeatherType { get; set; }
    public float Temperature { get; set; }
    public float Windspeed { get; set; }
    public float WindDirection { get; set; }
    public float WindspeedGust { get; set; }
    public float Pressure { get; set; }
    public float Humidity { get; set; }
    public float ProbOfRain { get; set; }
    public float AmountRain { get; set; }
    public float CloudAreaFraction { get; set; }
    public float FogAreaFraction { get; set; }
    public float ProbOfThunder { get; set; }
    public WeatherSourceDto Source { get; set; }

}