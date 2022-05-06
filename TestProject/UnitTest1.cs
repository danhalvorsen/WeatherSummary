using BasicWebAPI;
using BasicWebAPI.DAL;
using BasicWebAPI.Query;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject
{
    public class Tests
    {

        private Commands _command;

        private Mock<IConfigurationSection> configurationSectionMock;
        private Mock<IConfiguration> configurationMock;

        [SetUp]
        public void Setup()
        {
            //configurationSectionMock = new Mock<IConfigurationSection>();
            //configurationMock = new Mock<IConfiguration>();

            //configurationSectionMock
            //  .SetupGet(m => m[It.Is<string>(s => s == "WeatherForecastDatabase")]).Returns("Data Source = host.docker.internal,1433;Initial Catalog = DB; " +
            //    "User ID = sa; Password=123456a@;Connect Timeout = 20; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); // .Returns("mock value");
            //configurationMock.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(configurationSectionMock.Object);

        }

        [Test]
        public void Test1()
        {
            // Mocking the ASP.NET IConfiguration for getting the connection string from appsettings.json
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "WeatherForecastDatabase")]).Returns("Data Source=SqlServer;Initial Catalog=DB;User ID=sa;Password=123456a@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            TestContext.WriteLine(mockConfiguration.Object.GetConnectionString("WeatherForecastDatabase"));
            TestContext.WriteLine("test");
            // Assert
            Assert.IsTrue(mockConfiguration.Object.GetConnectionString("WeatherForecastDatabase") == "Data Source=SqlServer;Initial Catalog=DB;User ID=sa;Password=123456a@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


            _command = new Commands(mockConfiguration.Object);
            var weatherDataList = _command.GetWeatherForecastByDate(new DateQueryAndCity
            {
                CityQuery = new CityQuery
                {
                    City = "Stavanger"
                },
                DateQuery = new DateQuery
                {
                    Date = new System.DateTime(2022, 5, 4, 10, 0, 0)
                }
            });

            weatherDataList.Should().HaveCount(1000);


        }

        [Test]

        public async Task HelloWorldTest()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(
                    services => { 
                    
                    
                    }

                    
                    ).UseSetting("WeatherForecastDatabase", "Data Source=host.docker.internal,1433;Initial Catalog=DB;User ID=sa; Password=123456a@;Connect Timeout=20;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            });

            var client = application.CreateClient();

            var result = await client.GetAsync("/DateQueryAndCity?DateQuery.Date=2022-05-04&CityQuery.City=Stavanger");

            result.Should().NotBeNull();

        }

    }
}