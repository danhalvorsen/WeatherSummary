using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.DAL.Commands;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IGetCitiesQuery _query;
        private readonly IFetchCityCommand _fetchCommand;
        private readonly IAddCityToDatabaseStrategy _strategy;
        private readonly CityQueryValidator _cityQueryValidator;

        public CitiesController(
            ILogger<CitiesController> logger,
            IGetCitiesQuery query,
            IFetchCityCommand fetchCommand,
            IAddCityToDatabaseStrategy strategy, 
            CityQueryValidator cityQueryValidator)
        {
            _logger = logger;
            _query = query;
            _fetchCommand = fetchCommand;
            _strategy = strategy;
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

            var command = new AddCityCommand(_fetchCommand, _strategy);
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
            return await _query.GetAllCities();
        }
    }
}
