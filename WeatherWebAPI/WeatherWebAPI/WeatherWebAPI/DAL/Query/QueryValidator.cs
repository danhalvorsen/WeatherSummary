using FluentValidation;

namespace WeatherWebAPI.Query
{
    public class DateQueryAndCityValidator : AbstractValidator<DateQueryAndCity>
    {
        public DateQueryAndCityValidator()
        {
            RuleFor(p => p.CityQuery).SetValidator(new CityQueryValidator()!); 
            
            RuleFor(p => p.DateQuery).SetValidator(new DateQueryValidator()!);
        }
    }

    public class DateQueryValidator : AbstractValidator<DateQuery>
    {
       
        public DateQueryValidator()
        {
            DateTime valueToCompare = new(2020, 1, 1);
            
            RuleFor(p => p.Date).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
            RuleFor(p => p.Date).GreaterThan(valueToCompare).WithMessage("{PropertyName} should be higher than the lower limit: " + $"{valueToCompare.Date}");
        }
    }

    public class CityQueryValidator : AbstractValidator<CityQuery>
    {

        public CityQueryValidator()
        {
            RuleFor(p => p.City).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
        }
    }

    public class DaysQueryValidator : AbstractValidator<DaysQuery>
    {
        public DaysQueryValidator()
        {
            RuleFor(p => p.Days).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
            RuleFor(p => p.Days).InclusiveBetween(1, 7).WithMessage("{PropertyName} has to have a value between {From}-{To}");
        }
    }

    public class BetweenDateQueryValidator : AbstractValidator<BetweenDateQuery>
    {
        public BetweenDateQueryValidator()
        {
            DateTime valueToCompare = new(2020, 1, 1);
            
            RuleFor(p => p.From).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
            RuleFor(p => p.From).GreaterThan(valueToCompare).WithMessage("{PropertyName} should be higher than the lower limit: " + $"{valueToCompare.Date}");

            RuleFor(p => p.To).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
            RuleFor(p => p.To).GreaterThan(valueToCompare).WithMessage("{PropertyName} should be higher than the lower limit: " + $"{valueToCompare.Date}");
            RuleFor(p => p.To).Must((p, m, x) => { return p.From <= p.To; }).WithMessage("{PropertyName} should be higher than or equal to From-date");
                //GreaterThan(s => s.From).WithMessage("{PropertyName} should be higher than the From-date");
        }
    }

    public class BetweenDateQueryAndCityValidator : AbstractValidator<BetweenDateQueryAndCity>
    {
        public BetweenDateQueryAndCityValidator()
        {
            RuleFor(p => p.CityQuery).SetValidator(new CityQueryValidator()!);
            RuleFor(p => p.BetweenDateQuery).SetValidator(new BetweenDateQueryValidator()!);
        }
    }

    public class WeekQueryAndCityValidator : AbstractValidator<WeekQueryAndCity>
    {
        public WeekQueryAndCityValidator()
        {
            RuleFor(p => p.CityQuery).SetValidator(new CityQueryValidator()!);

            RuleFor(p => p.Week).NotEmpty().WithMessage("{PropertyName} can't be empty, null or whitespace");
            RuleFor(p => p.Week).InclusiveBetween(1, 52).WithMessage("{PropertyName} has to have a value between {From}-{To}");
        }
    }

    public class DaysQueryAndCityValidator : AbstractValidator<DaysQueryAndCity>
    {
        public DaysQueryAndCityValidator()
        {
            RuleFor(p => p.CityQuery).SetValidator(new CityQueryValidator()!);

            RuleFor(p => p.DaysQuery).SetValidator(new DaysQueryValidator()!);
        }
    }
}
