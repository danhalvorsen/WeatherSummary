namespace WeatherWebAPI.Contracts
{
    public class ScoresAverageDto
    {
        public float AverageValue { get; set; }
        public float AverageWeightedValue { get; set; }
        public string? DataProvider { get; set; }
    }
}
