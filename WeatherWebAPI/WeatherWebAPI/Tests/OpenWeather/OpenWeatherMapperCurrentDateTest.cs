using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherMapperCurrentDateTest
    {
        private int _unix;
        private DateTime dateTime;
        private MapperConfiguration? config;
        private MapperConfiguration CreateConfig(DateTime queryDate)
        {
            var config = new MapperConfiguration(

                    cfg => cfg.CreateMap<ApplicationOpenWeather, WeatherForecastDto>()
                 .ForPath(dest => dest.Date, opt => opt.MapFrom(src => UnixTimeStampToDateTime(src.current.dt))) // date <- this is an UNIX int type
                 .ForPath(dest => dest.WeatherType, opt => opt // weathertype
                     .MapFrom(src => src.current.weather[0].description)) // <-- Got a mapper exception once, because the city of stockholm had 2 descriptions. Just made this one
                                                                          // enter the first one each time. Should work.
                                                                          //.ToList()
                                                                          //.Single()
                                                                          //   .description))
                 .ForPath(dest => dest.Temperature, opt => opt  // temperature
                     .MapFrom(src => src.current.temp))
                 .ForPath(dest => dest.Windspeed, opt => opt // windspeed
                     .MapFrom(src => src.current.wind_speed))
                 .ForPath(dest => dest.WindDirection, opt => opt // wind direction
                     .MapFrom(src => src.current.wind_deg))
                 .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
                     .MapFrom(src => src.current.wind_gust))
                 .ForPath(dest => dest.Pressure, opt => opt // pressure
                     .MapFrom(src => src.current.pressure))
                 .ForPath(dest => dest.Humidity, opt => opt // humidity
                     .MapFrom(src => src.current.humidity))
                 .ForPath(dest => dest.ProbOfRain, opt => opt // probability of percipitation (probability of rain)
                     .MapFrom(src => src.hourly
                        .ToList()
                        .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
                            .pop * 100))
                 .ForPath(dest => dest.AmountRain, opt => opt // percipitation amount (amount of rain)
                    .MapFrom(src => src.hourly
                        .ToList()
                        .Single(i => i.dt.Equals(DateTimeToUnixTime(queryDate)))
                            .rain._1h))
                 .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
                    .MapFrom(src => src.current.clouds))
                 .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
                    .MapFrom(src => src.current.visibility))
                  //.ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
                  //   .MapFrom(src => src.properties.timeseries
                  //       .ToList()
                  //       .Single(i => i.time.Equals(queryDate))
                  //           .data.next_1_hours.details.probability_of_thunder))
                  .AfterMap((s, d) => d.Source.DataProvider = "OpenWeather") // Adding the datasource name to weatherforceastdto
                  .AfterMap((s, d) => d.FogAreaFraction = VisibilityConvertedToFogAreaFraction((float)d.FogAreaFraction))
                 );
            return config;
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private static int DateTimeToUnixTime(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime(); // If this is not done, the time would be 2 hours ahead of what we'd actually want.
            int unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        private float VisibilityConvertedToFogAreaFraction(float value)
        {
            return Math.Abs(value / 100 - 100);
        }


        [SetUp]
        public void Setup()
        {
            IGetWeatherDataStrategy<WeatherForecastDto> strategy = new OpenWeatherStrategy(new OpenWeatherConfig());
            dateTime = DateTime.Now; /*new DateTime(2022, 05, 18, 8, 0, 0); */// May 18, 2022 8:00:00
            _unix = DateTimeToUnixTime(dateTime); //1652853600; Wednesday, May 18, 2022 8:00:00 AM GMT+02:00 DST
            config = CreateConfig(dateTime);
        }

        [Test]
        public void ShouldMapDate()
        {
            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    dt = _unix
                }
            };
            // Act
            IMapper mapper = new Mapper(config);

            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.Date);
            result.Date.Should().Be(UnixTimeStampToDateTime(_unix));
        }

        [Test]
        public void ShouldMapWeatherType()
        {
            var weatherType = "sunny";

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    weather = new List<Weather> { new Weather {
                            description = weatherType
                        }
                    }
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.WeatherType);
            result.WeatherType.Should().Be(weatherType);
        }

        [Test]
        public void ShouldMapTemp()
        {
            var temp = 100.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    temp = temp
                }
            };

            IMapper mapper = new Mapper(config);

            //Act
            var result = mapper.Map<WeatherForecastDto>(application);

            //Assert
            Console.WriteLine(result.Temperature);
            result.Temperature.Should().Be(temp);


        }

        [Test]
        public void ShouldMapWindspeed()
        {
            var windspeed = 15.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    wind_speed = windspeed
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.Windspeed);
            result.Windspeed.Should().Be(windspeed);

        }

        [Test]
        public void ShouldMapWindDirection()
        {
            var windDirection = 100.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    wind_deg = windDirection
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.WindDirection);
            result.WindDirection.Should().Be(windDirection);
        }

        [Test]
        public void ShouldMapWindspeedGust()
        {
            var windspeedGust = 20.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    wind_gust = windspeedGust
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.WindspeedGust);

            result.WindspeedGust.Should().Be(windspeedGust);
        }

        [Test]
        public void ShouldMapPressure()
        {
            var pressure = 1000.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    pressure = pressure
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(pressure);

            result.Pressure.Should().Be(pressure);
        }

        [Test]
        public void ShouldMapHumidity()
        {
            var humidity = 50.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    humidity = humidity
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(humidity);

            result.Humidity.Should().Be(humidity);
        }

        [Test]
        public void ShouldMapProbOfRain() // Prob of rain is 0-1, where 0 is 0% and 1 is 100%
        {
            var probOfRain = 0.5f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                hourly = new List<Hourly> { new Hourly {
                    dt = _unix,
                    pop = probOfRain
                }}
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(probOfRain);

            result.ProbOfRain.Should().Be(probOfRain * 100);
        }

        [Test]
        public void ShouldMapAmountOfRain()
        {
            var amountOfRain = 10.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                hourly = new List<Hourly> { new Hourly {
                    dt = _unix,
                    rain = new Rain
                    {
                        _1h = amountOfRain
                    }
                }}
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result.AmountRain);
            Console.WriteLine(DateTimeToUnixTime(dateTime));
            Console.WriteLine(dateTime);

            result.AmountRain.Should().Be(amountOfRain);
        }

        [Test]
        public void ShouldMapCloudAreaFraction()
        {
            var cloudAreaFraction = 100.0f;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    clouds = cloudAreaFraction
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(cloudAreaFraction);

            result.CloudAreaFraction.Should().Be(cloudAreaFraction);
        }

        [Test]
        public void ShouldMapFogAreaFraction() // Visiblity is max 10km (10 000) -> To get the range as 0-100% we need to divide på 100. This is done in the mapper config
        {
            var fogAreaFraction = 10000;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                current = new Current
                {
                    visibility = fogAreaFraction
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.FogAreaFraction.Should().Be((float)VisibilityConvertedToFogAreaFraction(fogAreaFraction));

            Console.WriteLine(VisibilityConvertedToFogAreaFraction(fogAreaFraction));
        }

        //-- OpenWeather doesn't have information about this
        //
        //[Test]
        //public void ShouldMapProbOfThunder()
        //{
        //    var probThunder = 50.0f;

        //    // Arrange
        //    var application = new ApplicationOpenWeather
        //    {
        //    };
        //    Mapper mapper = new Mapper(config);

        //    // Act
        //    var result = mapper.Map<WeatherForecastDto>(application);

        //    // Assert
        //    result.ProbOfThunder.Should().Be(probThunder);
        //}

        [Test]
        public void ShouldSetDataSourceName()
        {
            var application = new ApplicationOpenWeather();
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            Console.WriteLine(result?.Source?.DataProvider);

            result?.Source?.DataProvider.Should().Be("OpenWeather");
        }
    }
}
