using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Basecode.Services.Services.ErrorHandling;

namespace G3HAS_Unit_Tests.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IHrEmployeeService> _serviceMock;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _serviceMock = new Mock<IHrEmployeeService>();
            _controller = new AdminController(_serviceMock.Object);
        }

        [Fact]
        public void AdminDashboard_ValidEmail_ReturnsViewWithHrEmployee()
        {
            // Arrange
            var email = "test@example.com";
            var expectedHrEmployee = new HrEmployee();

            _serviceMock.Setup(s => s.GetByEmail(email)).Returns(expectedHrEmployee);

            // Act
            var result = _controller.AdminDashboard(email) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedHrEmployee, result.Model);
        }

        [Fact]
        public void HrList_ReturnsViewWithData()
        {
            // Arrange
            var expectedData = new List<HrEmployee>();

            _serviceMock.Setup(s => s.RetrieveAll()).Returns(expectedData);

            // Act
            var result = _controller.HrList() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result.Model);
        }

        [Fact]
        public void CreateHrAccount_ValidHrEmployee_ReturnsRedirectToAction()
        {
            // Arrange
            var hrEmployee = new HREmployeeCreationDto();

            var logContent = new LogContent
            {
                Result = true
            };

            _serviceMock.Setup(s => s.CreateHrAccount(hrEmployee)).Returns(logContent);

            // Act
            var result = _controller.CreateHrAccount(hrEmployee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HrList", result.ActionName);
        }

        [Fact]
        public void EditHrAccount_ValidId_ReturnsViewWithHrEmployeeDto()
        {
            // Arrange
            int id = 1;
            var expectedHrEmployee = new HrEmployee
            {
                Id = id,
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password"
            }; // Replace with the appropriate HREmployee instance

            _serviceMock.Setup(s => s.GetById(id)).Returns(expectedHrEmployee);

            // Act
            var result = _controller.EditHrAccount(id) as ViewResult;
            var model = result.Model as HREmployeeUpdationDto;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(expectedHrEmployee.Name, model.Name);
            Assert.Equal(expectedHrEmployee.Email, model.Email);
            Assert.Equal(expectedHrEmployee.Password, model.Password);
            Assert.Equal(expectedHrEmployee.Id, model.Id);
        }

        [Fact]
        public void EditHrAccount_ValidHrEmployeeDto_ReturnsRedirectToAction()
        {
            // Arrange
            var hrEmployee = new HREmployeeUpdationDto(); // Replace with the appropriate HREmployeeUpdationDto instance

            // Act
            var result = _controller.EditHrAccount(hrEmployee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HrList", result.ActionName);
        }

        [Fact]
        public void DeleteHrAccount_ValidId_ReturnsRedirectToAction()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.DeleteHrAccount(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HrList", result.ActionName);
        }

        [Fact]
        public void Update_ValidHrEmployeeDto_ReturnsRedirectToAction()
        {
            // Arrange
            var hrEmployee = new HREmployeeUpdationDto(); // Replace with the appropriate HREmployeeUpdationDto instance

            // Act
            var result = _controller.Update(hrEmployee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HrList", result.ActionName);
        }

        [Fact]
        public void Add_ValidHrEmployeeDto_ReturnsRedirectToAction()
        {
            // Arrange
            var hrEmployee = new HREmployeeCreationDto(); // Replace with the appropriate HREmployeeCreationDto instance

            // Act
            var result = _controller.Add(hrEmployee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HrList", result.ActionName);
        }


    }

}
