﻿using Microsoft.AspNetCore.Mvc;
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
        
        private readonly IConfiguration _config;
        private readonly IFactory _factory;

        private readonly CityQueryValidator _cityQueryValidator;
        private readonly DaysQueryValidator _daysQueryValidator;
        private readonly DaysQueryAndCityValidator _daysQueryAndCityValidator;

        private readonly List<IGetWeatherDataStrategy<WeatherForecast>> _strategies = new();

        public CompanyRatingController(IConfiguration config, IFactory factory, ILogger<CompanyRatingController> logger, 
            CityQueryValidator cityQueryValidator, DaysQueryValidator daysQueryValidator, DaysQueryAndCityValidator daysQueryAndCityValidator)
        {
            _config = config;
            _factory = factory;
            _logger = logger;
            _cityQueryValidator = cityQueryValidator;
            _daysQueryValidator = daysQueryValidator;
            _daysQueryAndCityValidator = daysQueryAndCityValidator;

            _strategies.Add(this._factory.Build<IYrStrategy>());
            _strategies.Add(this._factory.Build<IOpenWeatherStrategy>());
            _strategies.Add(_factory.Build<IWeatherApiStrategy>());
        }


        [Route("avgScoreWeatherProvider/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreForWeatherProvider()
        {
            var command = new GetAvgScoresWeatherProviderQuery(_config, _factory);

            return await command.CalculateAverageScores(_strategies);
        }

        [Route("avgScorePredictionLength/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageDto>>> GetAverageScoreSelectedPredictionLength([FromQuery]DaysQuery query)
        {
            var validationResult = _daysQueryValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresForSelectedPredictionLengthQuery(_config, _factory);

            return await command.CalculateAverageScoresForSelectedPredictionLength(query, _strategies);
        }

        [Route("avgScoreWeatherProviderForCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreCity([FromQuery]CityQuery query)
        {
            var validationResult = _cityQueryValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresWeatherProviderForCityQuery(_config, _factory);

            return await command.CalculateAverageScoresFor(query, _strategies);
        }

        [Route("avgScorePredictionLengthAndCity/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<List<ScoresAverageForCityDto>>> GetAverageScoreSelectedPredictionLengthForCity([FromQuery]DaysQueryAndCity query)
        {
            var validationResult = _daysQueryAndCityValidator.Validate(query);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new GetAvgScoresForSelectedPredictionLengthForCityQuery(_config, _factory);

            return await command.CalculateAverageScoresForSelectedPredictionLengthAndCity(query, _strategies);
        }
    }
}