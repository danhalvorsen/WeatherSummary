namespace WeatherWebAPI.Controllers
{
    public class ScoreDto
    {
        public double? Score { get; set; }
        public double? ScoreWeighted { get; set; }
        public int FK_WeatherDataId { get; set; }
    }
}
