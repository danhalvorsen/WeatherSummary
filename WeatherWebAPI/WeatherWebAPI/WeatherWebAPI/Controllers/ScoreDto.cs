namespace WeatherWebAPI.Controllers
{
    public class ScoreDto
    {
        public float Score { get; set; }
        public float ScoreWeighted { get; set; }
        public int FK_WeatherDataId { get; set; }
    }
}
