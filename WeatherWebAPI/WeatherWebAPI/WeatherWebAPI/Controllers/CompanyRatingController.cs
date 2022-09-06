using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRatingController : ControllerBase
    {
        private readonly ILogger<CompanyRatingController> _logger;
        private readonly List<IStrategy> _strategies = new();

        public CompanyRatingController(ILogger<CompanyRatingController> logger)
        {
            
            _logger = logger;
            _strategies.Add(args.Common.Factory!.Build(StrategyType.Yr));
            _strategies.Add(args.Common.Factory!.Build(StrategyType.OpenWeather));
            //_strategies.Add(args.Common.Factory.Build(StrategyType.WeatherApi));
        }


        [Route("avgScoreWeatherProvider/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreForWeatherProvider()
        {
            var command = new GetAvgScoresWeatherProviderQuery(_arguments.Common);
            return await command.CalculateAverageScores(GetWeatherDataStrategies());
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

            return await command.CalculateAverageScoresForSelectedPredictionLength(query, GetWeatherDataStrategies());
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

            return await command.CalculateAverageScoresFor(query, GetWeatherDataStrategies());
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

            return await command.CalculateAverageScoresForSelectedPredictionLengthAndCity(query, GetWeatherDataStrategies());
        }


        private IEnumerable<IStrategy> GetWeatherDataStrategies()
        {
            return _strategies.Where(s =>
                s.StrategyType.Equals(StrategyType.Yr) ||
                s.StrategyType.Equals(StrategyType.OpenWeather));
        }

    }
}
