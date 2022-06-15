using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests.Yr
{
    public class YrMapperTest
    {
        private DateTime dateTime;
        private MapperConfiguration? config;
        private static MapperConfiguration CreateConfig(DateTime queryDate)
        {
            var config = new MapperConfiguration(
             cfg => cfg.CreateMap<ApplicationYr, WeatherForecastDto>()
             .ForPath(dest => dest.Date, opt => opt         // date
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                        .Single(i => i.time.Equals(queryDate)).time)) // TIMESERIES OVER UPDATED AT; WEATHER MORE EQUAL TO REAL TIME (properties.meta.updated_at))
             .ForPath(dest => dest.WeatherType, opt => opt  // weathertype
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.next_6_hours.summary.symbol_code))
             .ForPath(dest => dest.Temperature, opt => opt  // temperature
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                        .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.air_temperature))
             .ForPath(dest => dest.Windspeed, opt => opt    // windspeed
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.wind_speed))
             .ForPath(dest => dest.WindDirection, opt => opt // wind direction
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.wind_from_direction))
             .ForPath(dest => dest.WindspeedGust, opt => opt // windspeed gust
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.wind_speed_of_gust))
             .ForPath(dest => dest.Pressure, opt => opt     // pressure
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.air_pressure_at_sea_level))
             .ForPath(dest => dest.Humidity, opt => opt     // humidity
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.relative_humidity))
             .ForPath(dest => dest.ProbOfRain, opt => opt   // probability of percipitation (probability of rain)
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.next_6_hours.details.probability_of_precipitation))
             .ForPath(dest => dest.AmountRain, opt => opt   // percipitation amount (amount of rain)
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.next_6_hours.details.precipitation_amount))
             .ForPath(dest => dest.CloudAreaFraction, opt => opt // cloud area fraction
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.cloud_area_fraction))
             .ForPath(dest => dest.FogAreaFraction, opt => opt // fog area fraction
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.instant.details.fog_area_fraction))
             .ForPath(dest => dest.ProbOfThunder, opt => opt // probabiliy of thunder
                .MapFrom(src => src.properties.timeseries
                    .ToList()
                    .Single(i => i.time.Equals(queryDate))
                        .data.next_1_hours.details.probability_of_thunder))
              .AfterMap((s, d) => d.Source.DataProvider = "Yr") // DataSource.ToString().Replace("Strategy", "")) // Adding the datasource name to weatherforceastdto
             );

            return config;
        }

        [SetUp]
        public void Setup()
        {
            dateTime = new DateTime(1999, 1, 1, 0, 0, 0);
            config = CreateConfig(dateTime);
        }

        [Test]
        public void ShouldMapDate()
        {
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime

                    }}
                    //meta = new Meta
                    //{
                    //    updated_at = dateTime
                    //}
                }
            };


            IMapper mapper = new Mapper(config);

            var result = mapper.Map<WeatherForecastDto>(application);

            Console.WriteLine(result.Date);

            result.Date.Should().Be(dateTime);
        }

        [Test]
        public void ShouldMapWeatherType()
        {
            var weatherType = "sunny";

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            next_6_hours = new Next__hours6 {
                                summary = new SummaryNext6h {
                                    symbol_code = weatherType
                                }
                            }
                        }
                    }}
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.WeatherType.Should().Be(weatherType);
        }

        [Test]
        public void ShouldMapTemp()
        {
            var temp = 100.0f;

            //Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant = new Instant {
                                details = new Details {
                                    air_temperature = temp
                                }
                            }
                        }
                    }}
                }
            };

            IMapper mapper = new Mapper(config);

            //Act
            var result = mapper.Map<WeatherForecastDto>(application);

            //Assert
            result.Temperature.Should().Be(temp);


        }

        [Test]
        public void ShouldMapWindspeed()
        {
            var windspeed = 15.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant = new Instant {
                                details = new Details {
                                    wind_speed = windspeed
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.Windspeed.Should().Be(windspeed);

        }

        [Test]
        public void ShouldMapWindDirection()
        {
            var windDirection = 100.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant= new Instant {
                                details = new Details {
                                    wind_from_direction = windDirection
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.WindDirection.Should().Be(windDirection);

        }

        [Test]
        public void ShouldMapWindspeedGust()
        {
            var windspeedGust = 20.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant = new Instant {
                                details = new Details {
                                    wind_speed_of_gust = windspeedGust
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.WindspeedGust.Should().Be(windspeedGust);
        }

        [Test]
        public void ShouldMapPressure()
        {
            var pressure = 1000.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant = new Instant {
                                details = new Details {
                                    air_pressure_at_sea_level = pressure
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.Pressure.Should().Be(pressure);
        }

        [Test]
        public void ShouldMapHumidity()
        {
            var humidity = 50.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            instant = new Instant {
                                details = new Details {
                                    relative_humidity = humidity
                                }
                            }
                        }
                    }}
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.Humidity.Should().Be(humidity);
        }

        [Test]
        public void ShouldMapProbOfRain()
        {
            var probOfRain = 50.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            next_6_hours = new Next__hours6 {
                                details = new DetailsNext6h {
                                    probability_of_precipitation = probOfRain
                                }
                            }
                        }
                    }}
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.ProbOfRain.Should().Be(probOfRain);
        }

        [Test]
        public void ShouldMapAmountOfRain()
        {
            var amountOfRain = 10.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            next_6_hours = new Next__hours6 {
                                details = new DetailsNext6h {
                                    precipitation_amount = amountOfRain
                                }
                            }
                        }
                    }}
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.AmountRain.Should().Be(amountOfRain);
        }

        [Test]
        public void ShouldMapCloudAreaFraction()
        {
            var cloudAreaFraction = 100.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data{
                            instant = new Instant {
                                details = new Details {
                                    cloud_area_fraction = cloudAreaFraction
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.CloudAreaFraction.Should().Be(cloudAreaFraction);
        }

        [Test]
        public void ShouldMapFogAreaFraction()
        {
            var fogAreaFraction = 100.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data{
                            instant = new Instant {
                                details = new Details {
                                    fog_area_fraction = fogAreaFraction
                                }
                            }
                        }
                    }}
                }
            };
            IMapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.FogAreaFraction.Should().Be(fogAreaFraction);
        }

        [Test]
        public void ShouldMapProbOfThunder()
        {
            var probThunder = 50.0f;

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries> { new Timeseries {
                        time = dateTime,
                        data = new Data {
                            next_1_hours = new Next__hours1 {
                                details = new DetailsNext1h {
                                    probability_of_thunder = probThunder
                                }
                            }
                        }
                    }}
                }
            };
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.ProbOfThunder.Should().Be(probThunder);
        }

        [Test]
        public void ShouldSetDataSourceName() // Remember to change strategy name to test for different forecast websites
        {
            var application = new ApplicationYr();
            Mapper mapper = new Mapper(config);

            // Act
            var result = mapper.Map<WeatherForecastDto>(application);

            // Assert
            result.Source.DataProvider.Should().Be("Yr"); // Change here aswell
        }
    }
}
