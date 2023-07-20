using Basecode.WebApp.Controllers;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace G3HAS_Unit_Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_loggerMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsViewWithModelErrorViewModel()
        {
            // Arrange
            var requestId = "1";
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.TraceIdentifier = requestId;

            // Act
            var result = _controller.Error() as ViewResult;
            var model = result?.ViewData.Model as ErrorViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(requestId, model.RequestId);
        }
    }
}
