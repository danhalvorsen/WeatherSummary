using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Arguments;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Factory.Strategy.WeatherApi;
using WeatherWebAPI.Factory.Strategy.YR;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRatingController : ControllerBase
    {
        private readonly ILogger<CompanyRatingController> _logger;
   
        private readonly Args _arguments;
        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _strategies = new();

        public CompanyRatingController(Args args, ILogger<CompanyRatingController> logger)
        {
            _arguments = args;
            _logger = logger;
            _strategies.Add(args.Common.Factory.Build<IYrStrategy>());
            _strategies.Add(args.Common.Factory.Build<IOpenWeatherStrategy>());
            _strategies.Add(args.Common.Factory.Build<IWeatherApiStrategy>());
        }


        [Route("avgScoreWeatherProvider/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreForWeatherProvider()
        {
            var command = new GetAvgScoresWeatherProviderQuery(_arguments.Common);

            return await command.CalculateAverageScores(_strategies);
        }

        [Route("avgScorePredictionLength/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreSelectedPredictionLength([FromQuery]DaysQuery query)
        {
            var validationResult = _arguments.DaysQueryValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresForSelectedPredictionLengthQuery(_arguments.Common);

            return await command.CalculateAverageScoresForSelectedPredictionLength(query, _strategies);
        }

        [Route("avgScoreWeatherProviderForCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreCity([FromQuery]CityQuery query)
        {
            var validationResult = _arguments.CityQueryValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresWeatherProviderForCityQuery(_arguments.Common);

            return await command.CalculateAverageScoresFor(query, _strategies);
        }

        [Route("avgScorePredictionLengthAndCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreSelectedPredictionLengthForCity([FromQuery]DaysQueryAndCity query)
        {
            var validationResult = _arguments.DaysQueryAndCityValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresForSelectedPredictionLengthForCityQuery(_arguments.Common);

            return await command.CalculateAverageScoresForSelectedPredictionLengthAndCity(query, _strategies);
        }
    }
}
