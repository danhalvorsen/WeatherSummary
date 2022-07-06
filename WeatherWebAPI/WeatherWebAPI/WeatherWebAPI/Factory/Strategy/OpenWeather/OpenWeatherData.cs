namespace WeatherWebAPI.Factory.Strategy.OpenWeather
{
    public class LocalNames
    {
        public string? lt { get; set; }
        public string? it { get; set; }
        public string? sr { get; set; }
        public string? ascii { get; set; }
        public string? si { get; set; }
        public string? bn { get; set; }
        public string? oc { get; set; }
        public string? sq { get; set; }
        public string? hi { get; set; }
        public string? eo { get; set; }
        public string? kw { get; set; }
        public string? ko { get; set; }
        public string? kv { get; set; }
        public string? os { get; set; }
        public string? el { get; set; }
        public string? hu { get; set; }
        public string? rm { get; set; }
        public string? kk { get; set; }
        public string? ru { get; set; }
        public string? cs { get; set; }
        public string? gn { get; set; }
        public string? lv { get; set; }
        public string? ja { get; set; }
        public string? ku { get; set; }
        public string? yo { get; set; }
        public string? th { get; set; }
        public string? zu { get; set; }
        public string? my { get; set; }
        public string? ta { get; set; }
        public string? tl { get; set; }
        public string? be { get; set; }
        public string? feature_name { get; set; }
        public string? ca { get; set; }
        public string? ln { get; set; }
        public string? or { get; set; }
        public string? ar { get; set; }
        public string? kn { get; set; }
        public string? ba { get; set; }
        public string? mt { get; set; }
        public string? an { get; set; }
        public string? no { get; set; }
        public string? nl { get; set; }
        public string? ro { get; set; }
        public string? sc { get; set; }
        public string? gv { get; set; }
        public string? bg { get; set; }
        public string? et { get; set; }
        public string? af { get; set; }
        public string? ga { get; set; }
        public string? uk { get; set; }
        public string? zh { get; set; }
        public string? es { get; set; }
        public string? @is { get; set; }
        public string? mk { get; set; }
        public string? fi { get; set; }
        public string? cy { get; set; }
        public string? bo { get; set; }
        public string? br { get; set; }
        public string? sl { get; set; }
        public string? tg { get; set; }
        public string? sv { get; set; }
        public string? fa { get; set; }
        public string? yi { get; set; }
        public string? ka { get; set; }
        public string? fr { get; set; }
        public string? sk { get; set; }
        public string? ht { get; set; }
        public string? gl { get; set; }
        public string? hy { get; set; }
        public string? ab { get; set; }
        public string? li { get; set; }
        public string? de { get; set; }
        public string? he { get; set; }
        public string? mi { get; set; }
        public string? ur { get; set; }
        public string? pt { get; set; }
        public string? tt { get; set; }
        public string? en { get; set; }
        public string? mr { get; set; }
        public string? te { get; set; }
        public string? cu { get; set; }
        public string? ky { get; set; }
        public string? wo { get; set; }
        public string? ps { get; set; }
        public string? am { get; set; }
        public string? gu { get; set; }
        public string? co { get; set; }
        public string? ne { get; set; }
        public string? cv { get; set; }
        public string? lo { get; set; }
        public string? vi { get; set; }
        public string? eu { get; set; }
        public string? gd { get; set; }
        public string? tr { get; set; }
        public string? fy { get; set; }
        public string? pl { get; set; }
        public string? sa { get; set; }
        public string? ml { get; set; }
        public string? mn { get; set; }
    }

    public class City
    {
        public string? name { get; set; }
        public IList<LocalNames>? local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string? country { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }

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
        public double wind_gust { get; set; }
        public IList<Weather>? weather { get; set; }
        public Rain? rain { get; set; }

    }
    public class Minutely
    {
        public int dt { get; set; }
        public double precipitation { get; set; }

    }
    public class WeatherHourly
    {
        public int id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }

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
        public IList<WeatherHourly>? weather { get; set; }
        public double pop { get; set; }
        public Rain? rain { get; set; }

    }

    public class Rain
    {
        public double _1h { get; set; }
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
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }

    }
    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }
        public Temp? temp { get; set; }
        public Feels_like? feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<WeatherDaily>? weather { get; set; }
        public double clouds { get; set; }
        public double pop { get; set; }
        public double rain { get; set; }
        public double uvi { get; set; }


    }
    public class ApplicationOpenWeather
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string? timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current? current { get; set; }
        public IList<Minutely>? minutely { get; set; }
        public IList<Hourly>? hourly { get; set; }
        public IList<Daily>? daily { get; set; }
        public City? city { get; set; }
    }
}
