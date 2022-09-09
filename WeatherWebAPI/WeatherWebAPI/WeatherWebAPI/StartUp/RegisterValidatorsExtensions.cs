using FluentValidation.AspNetCore;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.StartUp;

public static class RegisterValidatorsExtensions
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<DateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<DateQueryValidator>();
            options.RegisterValidatorsFromAssemblyContaining<CityQueryValidator>();
            options.RegisterValidatorsFromAssemblyContaining<BetweenDateQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<BetweenDateQueryValidator>();
            options.RegisterValidatorsFromAssemblyContaining<WeekQueryAndCity>();
            options.RegisterValidatorsFromAssemblyContaining<WeekQueryAndCityValidator>();
            options.RegisterValidatorsFromAssemblyContaining<DaysQueryValidator>();
        });

        return services;
    }
}





