using AutoMapper;
using System.Diagnostics;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.Arguments
{
    public class CommonArgs
    {
        public IConfiguration Config { get; set; }
        public IFactory Factory { get; set; }
        public IMapper Mapper { get; set; }

        public CommonArgs(IConfiguration config, IFactory factory, IMapper mapper)
        {
            Debug.Assert(config != null);
            Debug.Assert(factory != null);
            Debug.Assert(mapper != null);

            Config = config;
            Factory = factory;
            Mapper = mapper;
        }
    }

    public class ArgsValidators
    {
        public ArgsValidators()
        {

        }
    }

    public class Args
    {
        public CommonArgs Common { get; set; }
        public DateQueryAndCityValidator DateQueryAndCityValidator { get; set; }
        public CityQueryValidator CityQueryValidator { get; set; }
        public DaysQueryValidator DaysQueryValidator { get; set; }
        public BetweenDateQueryAndCityValidator BeetweenDateQueryAndCityValidator { get; set; }
        public WeekQueryAndCityValidator WeekQueryAndCityValidator { get; set; }
        public DaysQueryAndCityValidator DaysQueryAndCityValidator { get; set; }

        public Args(CommonArgs commonArgs, DateQueryAndCityValidator dateQueryAndCityValidator, CityQueryValidator cityQueryValidator, DaysQueryValidator daysQueryValidator, BetweenDateQueryAndCityValidator beetweenDateQueryAndCityValidator, WeekQueryAndCityValidator weekQueryAndCityValidator, DaysQueryAndCityValidator daysQueryAndCityValidator)
        {
            Common = commonArgs;
            DateQueryAndCityValidator = dateQueryAndCityValidator;
            CityQueryValidator = cityQueryValidator;
            DaysQueryValidator = daysQueryValidator;
            BeetweenDateQueryAndCityValidator = beetweenDateQueryAndCityValidator;
            WeekQueryAndCityValidator = weekQueryAndCityValidator;
            DaysQueryAndCityValidator = daysQueryAndCityValidator;
        }
    }

}


