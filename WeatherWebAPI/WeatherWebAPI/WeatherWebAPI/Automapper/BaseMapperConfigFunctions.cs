using System.Diagnostics;

namespace WeatherWebAPI.Automapper
{
    public abstract class BaseMapperConfigFunctions
    {
        private const int MAX_VALUE_VISIBILITY = 10;
        private const int PERCENTAGE_FACTOR = 10;

        protected static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            return dateTime;
        }

        protected static int DateTimeToUnixTime(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime();
            int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        protected static double VisibilityConvertedToFogAreaFraction(double value)
        {
            return Math.Abs((value - MAX_VALUE_VISIBILITY) * PERCENTAGE_FACTOR);
        }
    }
}
