using WebAPI.Application.Common.Weather;

namespace WebAPI.Host.Controllers.Weather;

public class WeatherforecastController : VersionedApiController
{
    [HttpGet("{date:DateTime}")]
    [OpenApiOperation("Get Weather data for one spesfic date", "")]
    public ActionResult<WeatherforecastDto> Day(DateTime date)
    {
        return new WeatherforecastDto
        {
            DateTime = "01.01.2022"
        };
    }

}
