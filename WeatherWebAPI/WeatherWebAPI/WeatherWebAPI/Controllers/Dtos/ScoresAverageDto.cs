namespace WeatherWebAPI.Controllers
{
    public class ScoresAverageDto
    {
        public float AverageScore { get; set; }
        public float AverageScoreWeighted { get; set; }
        public string? DataProvider { get; set; }
    }
}
