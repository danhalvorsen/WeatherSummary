namespace WeatherWebAPI.Controllers
{
    public class WeatherForecastDto
    {
        public WeatherForecastDto()
        {
            Source = new WeatherSourceDto();
            Score = new ScoreDto();
        }
        public int WeatherForecastId { get; set; }
        public string? City { get; set; }
        public DateTime Date { get; set; }
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
        public DateTime DateForecast { get; set; }
        public WeatherSourceDto? Source { get; set; }
        public ScoreDto? Score { get; set; }

    }
}