using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CitiesController(IConfiguration config)
        {
            _config = config;
        }


        [Route("getCitiesInDatabase/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<CityDto>>> GetCitiesFromDatabase()
        {
            var command = new GetCitiesQuery(_config);

            return await command.GetAllCities();
        }

    }
}
