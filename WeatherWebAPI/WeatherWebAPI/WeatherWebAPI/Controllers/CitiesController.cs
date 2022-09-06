using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.DAL.Commands;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IGetCitiesQuery _query;
        private readonly CityQueryValidator _cityQueryValidator;

        public CitiesController(IGetCitiesQuery query, CityQueryValidator cityQueryValidator)
        {
            _query = query;
            _cityQueryValidator = cityQueryValidator;
        }

        [Route("addCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> AddCityToDatabase([FromQuery]CityQuery query)
        {
            var validationResult = _cityQueryValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new AddCityCommand(IGetCitiesQuery);

            await command.AddCity(query);

            return Ok(command);
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
