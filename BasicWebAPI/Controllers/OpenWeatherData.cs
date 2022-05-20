using System;
using System.Collections.Generic;

namespace BasicWebAPI.OpenWeather
{
    public class LocalNames
    {
        public string ps { get; set; }
        public string @is { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public IList<LocalNames> local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }
    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public double clouds { get; set; }
        public double visibility { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public IList<Weather> weather { get; set; }

    }
    public class Minutely
    {
        public int dt { get; set; }
        public double precipitation { get; set; }

    }
    public class WeatherHourly
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }
    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public double clouds { get; set; }
        public double visibility { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<WeatherHourly> weather { get; set; }
        public double pop { get; set; }

    }
    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }

    }
    public class Feels_like
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }

    }
    public class WeatherDaily
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }
    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }
        public Temp temp { get; set; }
        public Feels_like feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<WeatherDaily> weather { get; set; }
        public double clouds { get; set; }
        public double pop { get; set; }
        public double uvi { get; set; }

    }
    public class ApplicationOpenWeather
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public IList<Minutely> minutely { get; set; }
        public IList<Hourly> hourly { get; set; }
        public IList<Daily> daily { get; set; }
        public City city { get; set; }
    }
}
