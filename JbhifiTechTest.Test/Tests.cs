using Moq;
using JbhifiTechTest.Server.Services;
using JbhifiTechTest.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JbhifiTechTest.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Melbourne", "Australia")]
        [TestCase("Chicago", "America")]
        [TestCase("Islamabad", "Pakistan")]
        [TestCase("Berlin", "Germany")]
        public async Task ValidateServiceReturnsSuccess(string city, string country)
        {
            var mockService = new Mock<IOpenWeatherMapService>();
            var controller = new WeatherForecastController(mockService.Object);
            var description = await controller.Get(city, country);
            Assert.That(description, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [TestCase("", "Australia")]
        [TestCase("Melbourne", "")]
        [TestCase("", "")]
        public async Task ValidateServiceReturnsBadRequest(string city, string country)
        {
            var mockService = new Mock<IOpenWeatherMapService>();
            var controller = new WeatherForecastController(mockService.Object);
            var description = await controller.Get(city, country);
            Assert.That(description, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        [TestCase("Rawalpindi", "Pakistan", "clear sky")]
        [TestCase("Melbourne", "Australia", "cloudy sky")]
        [TestCase("Hollywood", "America", "scattered clouds")]
        public async Task ValidateDescriptionOnSuccess(string city, string country, string desc)
        {
            var mockService = new Mock<IOpenWeatherMapService>();
            var description = string.Format("The weather forecast for {0}, {1} is {2}", city, country, desc);
            mockService.Setup(x => x.GetWeatherDetailsAsync(city, country)).ReturnsAsync(description);
            var controller = new WeatherForecastController(mockService.Object);
            var returnedDescription = await controller.Get(city, country);
            var realDescription = (returnedDescription as OkObjectResult).Value;
            Assert.That(description, Is.EqualTo(realDescription));
        }

        [Test]
        [TestCase("xzyewsd", "qweqwe")]
        [TestCase("qwerty", "qwerty")]
        [TestCase("plihgf", "bqpsez")]
        public async Task ValidateDescriptionOnFailure(string city, string country)
        {
            var description = string.Format("City ({0},{1}) not found", city, country);
            var mockService = new Mock<IOpenWeatherMapService>();
            mockService.Setup(x => x.GetWeatherDetailsAsync(city, country)).ReturnsAsync(description);
            var controller = new WeatherForecastController(mockService.Object);
            var returnedDescription = await controller.Get(city, country);
            var realDescription = (returnedDescription as OkObjectResult).Value;
            Assert.That(description, Is.EqualTo(realDescription));
        }
    }
}
