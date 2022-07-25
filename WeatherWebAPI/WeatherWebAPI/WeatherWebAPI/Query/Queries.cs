using System.ComponentModel.DataAnnotations;

namespace WeatherWebAPI.Query
{
    public class DateQuery
    {
        [Required]
        public DateTime Date { get; set; }
    }

    public class CityQuery
    {
        [Required]
        public string? City { get; set; }
    }
    public class BetweenDateQuery
    {
        [Required()]
        public DateTime From { get; set; }
        [Required()]
        public DateTime To { get; set; }
    }

    public class DateQueryAndCity
    {
        public DateQuery? DateQuery { get; set; }
        public CityQuery? CityQuery { get; set; }
    }

    
    public class BetweenDateQueryAndCity
    {
        public BetweenDateQuery? BetweenDateQuery { get; set; }
        public CityQuery? CityQuery { get; set; }
    }

    public class WeekQueryAndCity
    {
        [Required]
        public int Week { get; set; }
        public CityQuery? CityQuery { get; set; }
    }
}
