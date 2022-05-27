using System.Text.Json.Serialization;

namespace BasicWebAPI.Controllers
{
    public class CityDto
    {
        public CityDto()
        {

        }
        public CityDto(int id, string name, string country, double altitude, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Country = country;
            Altitude = altitude;
            Longitude = longitude;
            Latitude = latitude;
        }

        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        public double Altitude { get; set; }
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        //public override string ToString()
        //{
        //    return $"CityId: {Id}\nName: {Name}\nCountry: {Country}\nAltitude: {Altitude}\nLongitude: {Longitude}\nLatitude {Latitude}";
        //}
    }
}
