using System.Diagnostics;

namespace WeatherWebAPI.Factory.Strategy
{
    public abstract class BaseMapperConfigFunctions
    {
        // Need to convert from Unix to DateTime when fetching data from OpenWeather datasource and vice versa
        protected static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);/*ToLocalTime();*/
            return dateTime;
        }

        protected static int DateTimeToUnixTime(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime(); // If this is not done, the time would be 2 hours ahead of what we'd actually want.
            int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        protected static double VisibilityConvertedToFogAreaFraction(double value)
        {
            var trace = new StackTrace(true);

            if(trace.GetFrame(1)?.GetMethod()?.DeclaringType?.Namespace == "WeatherWebAPI.Factory.Strategy.WeatherApi")
                return Math.Abs((value - 10) * 10);
            else
                return Math.Abs((value / 100) - 100);

        }
    }
}
