using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.DAL.Commands;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IFactory _factory;

        private readonly CityQueryValidator _cityQueryValidator;

        public CitiesController(IConfiguration config, IFactory factory, CityQueryValidator cityQueryValidator)
        {
            _config = config;
            _factory = factory;
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

            var command = new AddCityCommand(_config, _factory);

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
