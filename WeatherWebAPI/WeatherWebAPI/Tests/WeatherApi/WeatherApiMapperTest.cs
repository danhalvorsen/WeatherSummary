using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WeatherWebAPI.Automapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory.Strategy.WeatherApi;

namespace Tests.WeatherApi
{
    public class WeatherApiMapperTest : BaseMapperConfigFunctions
    {
        private int _unix;
        private int _unixDate;
        private DateTime _dateTime;
        private DateTime _dateTimeDate;

        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;

        [SetUp]
        public void Setup()
        {
            _dateTime = DateTime.UtcNow;
            _dateTimeDate = DateTime.UtcNow.Date;
            _unix = DateTimeToUnixTime(_dateTime);
            _unixDate = DateTimeToUnixTime(_dateTimeDate);

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") });
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Test]
        public void ShouldMapDateForecast()
        {
            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.DateForecast!.Equals(UnixTimeStampToDateTime(_unix))).First();
            Console.WriteLine(item.DateForecast);

            item.DateForecast.Should().Be(UnixTimeStampToDateTime(_unix));
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
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    condition = new Condition
                                    {
                                        text = weatherType
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
            Console.WriteLine(item.WeatherType);

            item.WeatherType.Should().Be(weatherType);
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
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    temp_c = temp
                                }
                            }
                        }
                    }
                }
            };

            // Act
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
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    wind_kph = windspeed
                                }
                            }
                        }
                    }
                }
            };
            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            var returnValueFromMapper = (float)Math.Round(windspeed * 5 / 18, 2);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.Windspeed!.Equals(returnValueFromMapper)).First();
            Console.WriteLine(item.Windspeed);

            item.Windspeed.Should().Be((float)returnValueFromMapper);
        }

        [Test]
        public void ShouldMapWindDirection()
        {
            var windDirection = 101;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    wind_degree = windDirection
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
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    gust_kph = windspeedGust
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            var returnValueFromMapper = (float)Math.Round(windspeedGust * 5 / 18, 2);

            // Assert
            var item = result!.Forecast.ToList().Where(i => i.WindspeedGust!.Equals(returnValueFromMapper)).First();
            Console.WriteLine(item.WindspeedGust);

            item.WindspeedGust.Should().Be(returnValueFromMapper);
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
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    pressure_mb = pressure
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
            var humidity = 50;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    humidity = humidity
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
            var probOfRain = 50;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    chance_of_rain = probOfRain
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
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    precip_mm = amountOfRain
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
            var cloudAreaFraction = 100;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    cloud = cloudAreaFraction
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
            var visibility = 10;

            // Arrange
            var application = new ApplicationWeatherApi
            {
                forecast = new Forecast
                {
                    forecastday = new List<Forecastday>
                    {
                        new Forecastday
                        {
                            date_epoch = _unixDate,
                            hour = new List<Hour>
                            {
                                new Hour
                                {
                                    time_epoch = _unix,
                                    vis_km = visibility
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var result = _mapper?.Map<WeatherForecast>(application);

            var fogAreaF = (float)VisibilityConvertedToFogAreaFraction(visibility);
            
            // Assert
            var item = result!.Forecast.ToList().Where(i => i.FogAreaFraction!.Equals(fogAreaF)).First();
            Console.WriteLine(item.FogAreaFraction);

            item.FogAreaFraction.Should().Be((float)fogAreaF);
        }
    }
}
