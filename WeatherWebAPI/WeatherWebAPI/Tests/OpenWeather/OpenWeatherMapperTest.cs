using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Factory.Strategy;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherMapperTest : BaseMapperConfigFunctions
    {
        private int _unix;
        private DateTime _dateTime;
        private ServiceProvider? _serviceProvider;
        private IMapper? _mapper;

        [SetUp]
        public void Setup()
        {
            _dateTime = DateTime.UtcNow;
            _unix = DateTimeToUnixTime(_dateTime);

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(new List<Assembly> { Assembly.LoadFrom("WeatherWebAPI.dll") });
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Test]
        public void ShouldMapDateForecast()
        {
            // Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
                {
                   new Daily
                   {
                       dt = _unix
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

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
                {
                   new Daily
                   {
                       dt = _unix,
                       weather = new List<WeatherDaily>
                       {
                           new WeatherDaily
                           {
                               description = weatherType
                           }
                       }
                   }
                }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.WeatherType!.Equals(weatherType)).First();
            Console.WriteLine(item.WeatherType);

            item.WeatherType.Should().Be(weatherType);
        }

        [Test]
        public void ShouldMapTemperature()
        {
            var temp = 100.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
                {
                   new Daily
                   {
                       temp = new Temp
                       {
                           day = temp
                       }
                   }
                }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.Temperature!.Equals(temp)).First();
            Console.WriteLine(item.Temperature);

            item.Temperature.Should().Be(temp);
        }

        [Test]
        public void ShouldMapWindspeed()
        {
            var windspeed = 15.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
                {
                   new Daily
                   {
                       wind_speed = windspeed
                   }
                }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.Windspeed!.Equals(windspeed)).First();
            Console.WriteLine(item.Windspeed);

            item.Windspeed.Should().Be(windspeed);
        }

        [Test]
        public void ShouldMapWindDirection()
        {
            var windDirection = 100.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       wind_deg = windDirection
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.WindDirection!.Equals(windDirection)).First();
            Console.WriteLine(item.WindDirection);

            item.WindDirection.Should().Be(windDirection);
        }

        [Test]
        public void ShouldMapWindspeedGust()
        {
            var windspeedGust = 20.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       wind_gust = windspeedGust
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.WindspeedGust!.Equals(windspeedGust)).First();
            Console.WriteLine(item.WindspeedGust);

            item.WindspeedGust.Should().Be(windspeedGust);
        }

        [Test]
        public void ShouldMapPressure()
        {
            var pressure = 1000.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       pressure = pressure
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.Pressure!.Equals(pressure)).First();
            Console.WriteLine(item.Pressure);

            item.Pressure.Should().Be(pressure);
        }

        [Test]
        public void ShouldMapHumidity()
        {
            var humidity = 50.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       humidity = humidity
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.Humidity!.Equals(humidity)).First();
            Console.WriteLine(item.Humidity);

            item.Humidity.Should().Be(humidity);
        }

        [Test]
        public void ShouldMapProbOfRain() // Prob of rain is 0-1, where 0 is 0% and 1 is 100%
        {
            var probOfRain = 0.5f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       pop = probOfRain
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.ProbOfRain!.Equals(probOfRain * 100)).First();
            Console.WriteLine(item.ProbOfRain);

            item.ProbOfRain.Should().Be(probOfRain * 100); // * 100 because of percentage
        }

        [Test]
        public void ShouldMapAmountOfRain()
        {
            var amountOfRain = 10.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       rain = amountOfRain
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.AmountRain!.Equals(amountOfRain)).First();
            Console.WriteLine(item.AmountRain);

            item.AmountRain.Should().Be(amountOfRain);
        }

        [Test]
        public void ShouldMapCloudAreaFraction()
        {
            var cloudAreaFraction = 100.0f;

            //Arrange
            var application = new ApplicationOpenWeather
            {
                daily = new List<Daily>
               {
                   new Daily
                   {
                       clouds = cloudAreaFraction
                   }
               }
            };

            //Act
            var result = _mapper?.Map<WeatherForecast>(application);

            //Assert
            var item = result!.Forecast.ToList().Where(i => i.CloudAreaFraction!.Equals(cloudAreaFraction)).First();
            Console.WriteLine(item.CloudAreaFraction);

            item.CloudAreaFraction.Should().Be(cloudAreaFraction);
        }

        //[Test]
        //public void ShouldMapFogAreaFraction() // Visiblity is max 10km (10 000) -> To get the range as 0-100% we need to divide på 100. This is done in the mapper config
        //{
        //    // var fogAreaFraction = 1000;

        //    // Arrange
        //    //var application = new ApplicationOpenWeather
        //    //{
        //    //    current = new Current
        //    //    {
        //    //        visibility = fogAreaFraction
        //    //    }
        //    //};
        //    // IMapper mapper = new Mapper(_config);

        //    // Act
        //    //var result = mapper.Map<WeatherForecast>(application);

        //    // Assert
        //    // result.FogAreaFraction.Should().Be((float)VisibilityConvertedToFogAreaFraction(fogAreaFraction));

        //    // Console.WriteLine(VisibilityConvertedToFogAreaFraction(fogAreaFraction));
        //}

        //[Test]
        //public void ShouldSetDataSourceName()
        //{
        //    // var application = new ApplicationOpenWeather();
        //    // Mapper mapper = new Mapper(_config);

        //    // Act
        //    //var result = mapper.Map<WeatherForecast>(application);

        //    // Assert
        //    // Console.WriteLine(result?.Source?.DataProvider);

        //    // result?.Source?.DataProvider.Should().Be("OpenWeather");
        //}

        //[Test]
        //public void ShouldSetDate()
        //{
        //    // var application = new ApplicationOpenWeather();
        //    // Mapper mapper = new Mapper(_config);

        //    // Act
        //    //var result = mapper.Map<WeatherForecast>(application);

        //    // Assert
        //    // Console.WriteLine(result.Date);

        //    // result.Date.Should().Be(DateTime.UtcNow.Date);
        //}
    }
}
