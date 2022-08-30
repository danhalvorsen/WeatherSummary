namespace WeatherWebAPI.Contracts.BaseContract
{
    public class WeatherForecast
    {
        public WeatherForecast()
        {
            Forecast = new List<WeatherData>();
        }


        public List<WeatherData> Forecast { get; set; }

        public WeatherData GetByDate(DateTime date)
        {
            return Forecast.Where(f => f.DateForecast.Equals(date)).First();
        }

        public bool AnyForcast(DateTime date) // DateExist()
        {
            return Forecast.ToList().Any(y =>
            {
                bool SameDate = y.Date.Equals(date);
                return SameDate;
            });
        }
        public class WeatherData
        {
            public WeatherData()
            {
                Source = new WeatherSourceDto();
                Score = new Scores();
            }

            public int WeatherForecastId { get; set; }
            public string? City { get; set; }
            public DateTime Date { get; set; }
            public DateTime DateForecast { get; set; }
            public string? WeatherType { get; set; }
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
            public Scores Score { get; set; }
        }
    }
}
