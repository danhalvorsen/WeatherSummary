using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherWebAPI.Controllers;

namespace Tests.Fakes
{
    public class FakeAddCityToDatabaseStrategy /*: IAddCityToDatabaseStrategy*/
    {
        public async Task<CityDto> Add(List<CityDto> city)
        {
            return await Task.FromResult(CreateTestData(city));
        }

        private static CityDto CreateTestData(List<CityDto> city)
        {
            return new CityDto
            {
                Id = city[0].Id,
                Name = city[0].Name,
                Country = city[0].Country,
                Altitude = city[0].Altitude,
                Longitude = city[0].Longitude,
                Latitude = city[0].Latitude
            };
        }
    }
}
