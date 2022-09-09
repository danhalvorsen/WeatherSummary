using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory.Strategy.YR;

namespace Tests.Yr
{
    public class YrMapperTest
    {
        private DateTime _dateTime;
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;

        [SetUp]
        public void Setup()
        {
            _dateTime = new DateTime(1999, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") });
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Test]
        public void ShouldMapDateForecast()
        {

            // Arrange
            var application = new ApplicationYr
            {
                properties = new Properties
                {
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime
                        }
                    }
                }
            };


            // Act
            var result = _mapper?.Map<WeatherForecast>(application);  /*, context => context.Items["QueryDate"] = _dateTime);*/

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.DateForecast!.Equals(_dateTime)).First();
            Console.WriteLine(item.DateForecast);

            item.DateForecast.Should().Be(_dateTime);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                next_6_hours = new Next__hours6
                                {
                                    summary = new SummaryNext6h
                                    {
                                        symbol_code = weatherType
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.WeatherType!.Equals(weatherType)).First();
            Console.WriteLine(item!.WeatherType);

            item.WeatherType.Should().Be(weatherType);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        air_temperature = temp
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.Temperature!.Equals(temp)).First();
            Console.WriteLine(item.Temperature);

            item.Temperature.Should().Be(temp);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        wind_speed = windspeed
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.Windspeed!.Equals(windspeed)).First();
            Console.WriteLine(item.Windspeed);

            item.Windspeed.Should().Be(windspeed);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        wind_from_direction = windDirection
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.WindDirection!.Equals(windDirection)).First();
            Console.WriteLine(item.WindDirection);

            item.WindDirection.Should().Be(windDirection);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        wind_speed_of_gust = windspeedGust
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.WindspeedGust!.Equals(windspeedGust)).First();
            Console.WriteLine(item.WindspeedGust);

            item.WindspeedGust.Should().Be(windspeedGust);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        air_pressure_at_sea_level = pressure
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.Pressure!.Equals(pressure)).First();
            Console.WriteLine(item.Pressure);

            item.Pressure.Should().Be(pressure);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        relative_humidity = humidity
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.Humidity!.Equals(humidity)).First();
            Console.WriteLine(item.Humidity);

            item.Humidity.Should().Be(humidity);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                next_6_hours = new Next__hours6
                                {
                                    details = new DetailsNext6h
                                    {
                                        probability_of_precipitation = probOfRain
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.ProbOfRain!.Equals(probOfRain)).First();
            Console.WriteLine(item.ProbOfRain);

            item.ProbOfRain.Should().Be(probOfRain);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                next_6_hours = new Next__hours6
                                {
                                    details = new DetailsNext6h
                                    {
                                        precipitation_amount = amountOfRain
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.AmountRain!.Equals(amountOfRain)).First();
            Console.WriteLine(item.AmountRain);

            item.AmountRain.Should().Be(amountOfRain);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        cloud_area_fraction = cloudAreaFraction
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.CloudAreaFraction!.Equals(cloudAreaFraction)).First();
            Console.WriteLine(item.CloudAreaFraction);

            item.CloudAreaFraction.Should().Be(cloudAreaFraction);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                instant = new Instant
                                {
                                    details = new Details
                                    {
                                        fog_area_fraction = fogAreaFraction
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.FogAreaFraction!.Equals(fogAreaFraction)).First();
            Console.WriteLine(item.FogAreaFraction);

            item.FogAreaFraction.Should().Be(fogAreaFraction);
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
                    timeseries = new List<Timeseries>
                    {
                        new Timeseries
                        {
                            time = _dateTime,
                            data = new Data
                            {
                                next_1_hours = new Next__hours1
                                {
                                    details = new DetailsNext1h
                                    {
                                        probability_of_thunder = probThunder
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.ProbOfThunder!.Equals(probThunder)).First();
            Console.WriteLine(item.ProbOfThunder);

            item.ProbOfThunder.Should().Be(probThunder);
        }

        //[Test]
        //public void ShouldSetDataSourceName() // Remember to change strategy name to test for different forecast websites
        //{
        //    var application = new ApplicationYr();

        //    // Act
        //    var result = _mapper?.Map<WeatherForecast>(application);

        //    // Assert
        //    result?.Forecast?.ToList()?.First()?.Source?.DataProvider.Should().Be("Yr");
        //}

        //[Test]
        //public void ShouldSetDate() // Remember to change strategy name to test for different forecast websites
        //{
        //    var application = new ApplicationYr();

        //    // Act
        //    var result = _mapper?.Map<WeatherForecast>(application);

        //    // Assert
        //    var item = result!.Forecast.ToList().Single(i => i.Date!.Equals(DateTime.UtcNow.Date));
        //    Console.WriteLine(item.Date);

        //    item.Date.Should().Be(DateTime.UtcNow.Date);
        //}
    }
}
