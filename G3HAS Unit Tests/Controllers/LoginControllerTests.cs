using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<IHrEmployeeService> _serviceMock;
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _serviceMock = new Mock<IHrEmployeeService>();
            _controller = new LoginController(_serviceMock.Object);
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
        public async Task LoginAuth_RedirectsToAdminDashboard()
        {
            // Arrange
            var loginModel = new LoginModel { Email = "test@example.com", Password = "password" };

            // Act
            var result = await _controller.LoginAuth(loginModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AdminDashboard", redirectToActionResult.ActionName);
            Assert.Equal("Admin", redirectToActionResult.ControllerName);
            Assert.NotNull(redirectToActionResult.RouteValues["Email"]);
            Assert.NotNull(redirectToActionResult.RouteValues["Password"]);
            Assert.Equal(loginModel.Email, redirectToActionResult.RouteValues["Email"]);
            Assert.Equal(loginModel.Password, redirectToActionResult.RouteValues["Password"]);
        }

        [Fact]
        public void Login_RedirectsToAdminDashboard()
        {
            // Arrange
            var email = "test@example.com";
            HrEmployee employeeData = null!;
            _serviceMock.Setup(s => s.GetByEmail(email)).Returns(employeeData);

            // Act
            var result = _controller.Login(email);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AdminDashboard", redirectToActionResult.ActionName);
            Assert.Equal("Admin", redirectToActionResult.ControllerName);
            Assert.Equal(employeeData, redirectToActionResult.RouteValues?["data"]);
        }


        [Fact]
        public void ForgotPassword_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.ForgotPassword();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ResetPassword_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.ResetPassword();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
