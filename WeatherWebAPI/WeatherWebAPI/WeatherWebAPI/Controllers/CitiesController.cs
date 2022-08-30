using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Arguments;
using WeatherWebAPI.DAL.Commands;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CommonArgs _commonArgs;
        private readonly CityQueryValidator _cityQueryValidator;

        public CitiesController(CommonArgs commonArgs, CityQueryValidator cityQueryValidator)
        {
            _commonArgs = commonArgs;
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

            var command = new AddCityCommand(_commonArgs);

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
            var command = new GetCitiesQuery(_commonArgs.Config);

            return await command.GetAllCities();
        }
    }
}
