using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.WeatherApi;


namespace Tests.WeatherApi
{
    public class WeatherApiMapperTest : BaseMapperConfigFunctions
    {
        private int _unix;
        private int _unixDate;
        private DateTime _dateTime;
        private DateTime _dateTimeDate;
        private MapperConfiguration? _config;

        private static MapperConfiguration CreateConfig(DateTime queryDate)
        {
            //if (queryDate.Date >= DateTime.UtcNow.Date)
            //    queryDate = queryDate.Date + new TimeSpan(12, 0, 0);


#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var config = new MapperConfiguration(
        cfg => cfg.CreateMap<ApplicationWeatherApi, WeatherForecast>()
            .ForPath(dest => dest.DateForecast, opt => opt
                .MapFrom(src => UnixTimeStampToDateTime(src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).time_epoch)))
            .ForPath(dest => dest.WeatherType, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).condition.text))
            .ForPath(dest => dest.Temperature, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).temp_c))
            .ForPath(dest => dest.Windspeed, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).wind_kph))
            .ForPath(dest => dest.WindDirection, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).wind_degree))
            .ForPath(dest => dest.WindspeedGust, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).gust_kph))
            .ForPath(dest => dest.Pressure, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).pressure_mb))
            .ForPath(dest => dest.Humidity, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).humidity))
            .ForPath(dest => dest.ProbOfRain, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).chance_of_rain))
            .ForPath(dest => dest.AmountRain, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).precip_mm))
            .ForPath(dest => dest.CloudAreaFraction, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).cloud))
            .ForPath(dest => dest.FogAreaFraction, opt => opt
                .MapFrom(src => src.forecast.forecastday
                    .ToList()
                    .Single(i => i.date_epoch.Equals(DateTimeToUnixTime(queryDate.Date))).hour
                                                                                            .ToList()
                                                                                            .Single(i => i.time_epoch.Equals(DateTimeToUnixTime(queryDate))).vis_km))
            .AfterMap((s, d) => d.Source.DataProvider = "WeatherApi")
            .AfterMap((s, d) => d.FogAreaFraction = (float)VisibilityConvertedToFogAreaFraction(d.FogAreaFraction))
            .AfterMap((s, d) => d.Date = DateTime.UtcNow.Date)
            .AfterMap((s, d) => d.Windspeed = (float)Math.Round(d.Windspeed * 5/18, 2))
            .AfterMap((s, d) => d.WindspeedGust = (float)Math.Round(d.WindspeedGust * 5/18, 2))
            );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

            return config;
        }

        [SetUp]
        public void Setup()
        {
            IGetWeatherDataStrategy<WeatherForecast> strategy = new WeatherApiStrategy(new WeatherApiConfig());
            _dateTime = DateTime.UtcNow;
            _dateTimeDate = DateTime.UtcNow.Date;
            _unix = DateTimeToUnixTime(_dateTime);
            _unixDate = DateTimeToUnixTime(_dateTimeDate);
            _config = CreateConfig(_dateTime);
        }

        [Test]
        public void ShouldMapDateForecast()
        {
            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);

            Console.WriteLine(result.DateForecast);
            
            // Assert
            result.DateForecast.Should().Be(UnixTimeStampToDateTime(_unix));
        }

        [Test]
        public void ShouldMapWeatherType()
        {
            var weatherType = "sunny";

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            condition = new Condition {
                                text = weatherType
                            }
                        }}
                    }}
                }
            };
            Mapper mapper = new Mapper(_config);
            
            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.WeatherType);

            // Assert
            result.WeatherType.Should().Be(weatherType);
        }

        [Test]
        public void ShouldMapTemp()
        {
            var temp = 100.0f;

            //Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            temp_c = temp
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            //Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Temperature);


            //Assert
            result.Temperature.Should().Be(temp);


        }

        [Test]
        public void ShouldMapWindspeed()
        {
            var windspeed = 15.0f;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            wind_kph = windspeed
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Windspeed);

            // Assert
            result.Windspeed.Should().Be((float)Math.Round(windspeed * 5/18, 2));

        }

        [Test]
        public void ShouldMapWindDirection()
        {
            var windDirection = 100;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            wind_degree = windDirection
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.WindDirection);

            // Assert
            result.WindDirection.Should().Be(windDirection);

        }

        [Test]
        public void ShouldMapWindspeedGust()
        {
            var windspeedGust = 20.0f;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            gust_kph = windspeedGust
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.WindspeedGust);

            // Assert
            result.WindspeedGust.Should().Be((float)Math.Round(windspeedGust * 5 / 18, 2));
        }

        [Test]
        public void ShouldMapPressure()
        {
            var pressure = 1000.0f;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            pressure_mb = pressure
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Pressure);

            // Assert
            result.Pressure.Should().Be(pressure);
        }

        [Test]
        public void ShouldMapHumidity()
        {
            var humidity = 50;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            humidity = humidity
                        }}
                    }}
                }
            };
            Mapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Humidity);

            // Assert
            result.Humidity.Should().Be(humidity);
        }

        [Test]
        public void ShouldMapProbOfRain()
        {
            var probOfRain = 50;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            chance_of_rain = probOfRain
                        }}
                    }}
                }
            };
            Mapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.ProbOfRain);

            // Assert
            result.ProbOfRain.Should().Be(probOfRain);
        }

        [Test]
        public void ShouldMapAmountOfRain()
        {
            var amountOfRain = 10.0f;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            precip_mm = amountOfRain
                        }}
                    }}
                }
            };
            Mapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.AmountRain);

            // Assert
            result.AmountRain.Should().Be(amountOfRain);
        }

        [Test]
        public void ShouldMapCloudAreaFraction()
        {
            var cloudAreaFraction = 100;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            cloud = cloudAreaFraction
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.CloudAreaFraction);

            // Assert
            result.CloudAreaFraction.Should().Be(cloudAreaFraction);
        }

        [Test]
        public void ShouldMapFogAreaFraction()
        {
            var fogAreaFraction = 100;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday> { new Forecastday {
                        date_epoch = _unixDate,
                        hour = new List<Hour> { new Hour {
                            time_epoch = _unix,
                            vis_km = fogAreaFraction
                        }}
                    }}
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.FogAreaFraction);

            // Assert
            result.FogAreaFraction.Should().BeLessThanOrEqualTo(100).And.BeGreaterThanOrEqualTo(0);
        }

        [Test]
        public void ShouldSetDataSourceName() // Remember to change strategy name to test for different forecast websites
        {
            var application = new ApplicationWeatherApi();
            Mapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Source.DataProvider);

            // Assert
            result.Source.DataProvider.Should().Be("WeatherApi"); // Change here aswell
        }

        [Test]
        public void ShouldSetDate()
        {
            var application = new ApplicationWeatherApi();
            Mapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<WeatherForecast>(application);
            Console.WriteLine(result.Date);


            // Assert
            result.Date.Should().Be(DateTime.UtcNow.Date);
        }
    }
}
