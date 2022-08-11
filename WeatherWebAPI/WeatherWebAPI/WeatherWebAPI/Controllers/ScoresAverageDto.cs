namespace WeatherWebAPI.Controllers
{
    public class ScoresAverageDto
    {
        public double? AverageScore { get; set; }
        public double? AveragecoreWeighted { get; set; }
        public string? DataProvider { get; set; }
    }
}
