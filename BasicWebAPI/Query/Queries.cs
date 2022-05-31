using Newtonsoft.Json;
using System;

namespace BasicWebAPI.Query
{
    public class DateQuery
    {
        public DateTime Date { get; set; }
    }

    public class CityQuery
    {
        public string City { get; set; }
    }
    public class BetweenDateQuery
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }

    public class DateQueryAndCity
    {
        public DateQuery DateQuery { get; set; }
        public CityQuery CityQuery { get; set; }
    }

    public class BetweenDateQueryAndCity
    {
        public BetweenDateQuery BetweenDateQuery { get; set; }
        public CityQuery CityQuery { get; set; }
    }
}
