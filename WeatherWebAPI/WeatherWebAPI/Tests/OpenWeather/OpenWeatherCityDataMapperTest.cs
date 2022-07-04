using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.OpenWeather;

namespace Tests.OpenWeather
{
    public class OpenWeatherCityDataMapperTest // Not getting used because of GetSteamAsync.
    {
        private MapperConfiguration ?_config;

        private MapperConfiguration CreateConfigForFetchingCityCoordinates()
        {
            MapperConfiguration config;

            config = new MapperConfiguration(

            cfg => cfg.CreateMap<ApplicationOpenWeather, CityDto>()
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.city!.name)) // name
            .ForPath(dest => dest.Longitude, opt => opt.MapFrom(src => src.city!.lon))
            .ForPath(dest => dest.Latitude, opt => opt.MapFrom(src => src.city!.lat))
            .ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.city!.country)));
            return config;
        }

        [SetUp]
        public void Setup()
        {
            IGetCityDataStrategy<CityDto> strategy = new OpenWeatherStrategy(new OpenWeatherConfig());
            _config = CreateConfigForFetchingCityCoordinates();
        }

        [Test]
        public void ShouldMapName()
        {
            var name = "Stavanger";

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    name = name
                }
            };
            IMapper mapper = new Mapper(_config);

            // Act
            var result = mapper.Map<CityDto>(application);


            //Assert
            result.Name.Should().Be(name);
        }

        [Test]
        public void ShouldMapCountry()
        {
            var country = "Norway";

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    country = country
                }
            };
            IMapper mapper = new Mapper(_config);

            //Act
            var result = mapper.Map<CityDto>(application);

            // Assert
            result.Country.Should().Be(country);
        }

        [Test]
        public void ShouldMapLongitude()
        {
            var lon = 12.121212;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    lon = lon
                }
            };
            IMapper mapper = new Mapper(_config);

            //Act
            var result = mapper.Map<CityDto>(application);

            // Assert
            result.Longitude.Should().Be(lon);
        }


        [Test]
        public void ShouldMapLatitude()
        {
            var lat = 12.121212;

            // Arrange
            var application = new ApplicationOpenWeather
            {
                city = new City
                {
                    lat = lat
                }
            };
            IMapper mapper = new Mapper(_config);

            //Act
            var result = mapper.Map<CityDto>(application);

            // Assert
            result.Latitude.Should().Be(lat);
        }
    }
}
