using WeatherWebAPI.Contracts.BaseContract;

namespace WeatherWebAPI.DAL.Query
{
    public class WeatherTuple : Tuple<
        List<WeatherForecast.WeatherData>,
        List<WeatherForecast.WeatherData>>
    {
        public List<WeatherForecast.WeatherData> ActualWeather { get; }
        public List<WeatherForecast.WeatherData> PredictedWeather { get; }

        public WeatherTuple(List<WeatherForecast.WeatherData> a, List<WeatherForecast.WeatherData> b) : base(a, b)
        {
            ActualWeather = a;
            PredictedWeather = b;
        }
    }

}
