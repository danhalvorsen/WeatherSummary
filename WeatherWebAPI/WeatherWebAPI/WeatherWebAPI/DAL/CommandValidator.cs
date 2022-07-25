using FluentValidation;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.DAL
{
    public class CityDataValidator : AbstractValidator<List<CityDto>>
    {
        public CityDataValidator()
        {
            RuleFor(p => p.Count).NotEmpty().WithMessage($"Can't find city data");
        }
    }
}
