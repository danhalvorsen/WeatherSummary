using FluentValidation;

namespace WeatherWebAPI;

public class SetTimeHourValidator : AbstractValidator<SetTimeHour>
{
    public SetTimeHourValidator()
    {
        RuleFor(p => p.StartHour).NotEmpty().WithMessage("{Propertyname} can't be empty, null or whitespace");
        RuleFor(p => p.StopHour).NotEmpty().WithMessage("{Propertyname} can't be empty, null or whitespace");
        RuleFor(p => p.StopHour).Must((p, m, x) => { return p.StartHour < p.StopHour; }).WithMessage("{PropertyName} should be higher than the start time");
    }
}
