using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherWebAPI.Contracts;
using WeatherWebAPI.DAL.Query;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRatingController : ControllerBase
    {
        private readonly List<IGetWeatherDataStrategy>? _weatherDataStrategies = new();
        private readonly ILogger<CompanyRatingController> _logger;
        private readonly IMapper _mapper;
        private readonly IGetWeatherDataFromDatabaseStrategy _getWeatherDataFromDatabaseStrategy;
        private readonly IGetCitiesQuery _getCitiesQuery;
        private readonly IOpenWeatherFetchCityStrategy _openWeatherFetchCityStrategy;
        private readonly CityQueryValidator _cityQueryValidator;
        private readonly DaysQueryValidator _daysQueryValidator;
        private readonly DaysQueryAndCityValidator _daysQueryAndCityValidator;

        public CompanyRatingController(
            ILogger<CompanyRatingController> logger,
            IMapper mapper,
            IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy,
            IGetCitiesQuery getCitiesQuery,
            IOpenWeatherFetchCityStrategy openWeatherFetchCityStrategy,
            CityQueryValidator cityQueryValidator,
            DaysQueryValidator daysQueryValidator,
            DaysQueryAndCityValidator daysQueryAndCityValidator,
            StrategyResolver strategyPicker
            )
        {
            _logger = logger;
            _mapper = mapper;
            _getWeatherDataFromDatabaseStrategy = getWeatherDataFromDatabaseStrategy;
            _getCitiesQuery = getCitiesQuery;
            _openWeatherFetchCityStrategy = openWeatherFetchCityStrategy;
            _cityQueryValidator = cityQueryValidator;
            _daysQueryValidator = daysQueryValidator;
            _daysQueryAndCityValidator = daysQueryAndCityValidator;
            
            _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.Yr));
            _weatherDataStrategies?.Add(strategyPicker(WeatherProvider.OpenWeather));
        }


        [Route("avgScoreWeatherProvider/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreForWeatherProvider()
        {
            var command = new GetAvgScoresWeatherProviderQuery(_mapper, _getWeatherDataFromDatabaseStrategy);

            return await command.CalculateAverageScores(_weatherDataStrategies!);
        }


        [Route("avgScorePredictionLength/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreSelectedPredictionLength([FromQuery] DaysQuery query)
        {
            var validationResult = _daysQueryValidator.Validate(query);
            if (!validationResult.IsValid)
            {
                _logger.LogError("{Errors}",validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }
               
            var command = new GetAvgScoresForSelectedPredictionLengthQuery(_mapper, _getWeatherDataFromDatabaseStrategy);

            return await command.CalculateAverageScoresForSelectedPredictionLength(query, _weatherDataStrategies!);
        }

        [Route("avgScoreWeatherProviderForCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreCity([FromQuery] CityQuery query)
        {
            var validationResult = _cityQueryValidator.Validate(query);
            if (!validationResult.IsValid)
            {
                _logger.LogError("{Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var command = new GetAvgScoresWeatherProviderForCityQuery(_mapper, _getCitiesQuery, _getWeatherDataFromDatabaseStrategy, _openWeatherFetchCityStrategy);

            return await command.CalculateAverageScoresFor(query, _weatherDataStrategies!);
        }

        [Route("avgScorePredictionLengthAndCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreSelectedPredictionLengthForCity([FromQuery] DaysQueryAndCity query)
        {
            var validationResult = _daysQueryAndCityValidator.Validate(query);
            if (!validationResult.IsValid)
            {
                _logger.LogError("{Errors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var command = new GetAvgScoresForSelectedPredictionLengthForCityQuery(_mapper, _getCitiesQuery, _getWeatherDataFromDatabaseStrategy, _openWeatherFetchCityStrategy);

            return await command.CalculateAverageScoresForSelectedPredictionLengthAndCity(query, _weatherDataStrategies!);
        }
    }
}
