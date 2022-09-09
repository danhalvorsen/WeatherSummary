namespace WeatherWebAPI
{
    public class SetTimeHour
    {
        public int StartHour { get; set; }
        public int StopHour { get; set; }

        public DateTime SetHour(int hour)
        {
            return DateTime.UtcNow.Date + new TimeSpan(hour, 0, 0);
        }
    }
}
