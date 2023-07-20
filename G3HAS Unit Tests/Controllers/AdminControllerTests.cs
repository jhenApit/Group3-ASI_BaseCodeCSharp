using Basecode.Data.Dtos;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Basecode.Services.Utils.ErrorHandling;

namespace G3HAS_Unit_Tests.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IHrEmployeeService> _serviceMock;
        private readonly Mock<IErrorHandling> _ehMock;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _serviceMock = new Mock<IHrEmployeeService>();
            _ehMock = new Mock<IErrorHandling>();
            _controller = new AdminController(_serviceMock.Object, _ehMock.Object);
        }

        [Fact]
        public void AdminDashboard_ValidEmail_ReturnsViewResult()
        {
            // Arrange
            var email = "test@example.com";
            var hrEmployee = new HrEmployee();
            _serviceMock.Setup(x => x.GetByEmail(email)).Returns(hrEmployee);

            // Act
            var result = _controller.AdminDashboard(email);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(hrEmployee, viewResult.Model);
        }

        [Fact]
        public void HrList_ReturnsViewResult()
        {
            // Arrange
            var hrEmployees = new List<HrEmployee>();
            _serviceMock.Setup(x => x.RetrieveAll()).Returns(hrEmployees);

            // Act
            var result = _controller.HrList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(hrEmployees, viewResult.Model);
        }

        [Fact]
        public void CreateHrAccount_WithValidModel_ReturnsRedirectToHrList()
        {
            // Arrange
            var hrEmployee = new HREmployeeCreationDto(); // Create a sample HREmployeeCreationDto object
            _serviceMock.Setup(s => s.CreateHrAccount(hrEmployee)).Returns(new LogContent { Result = true });

            // Act
            var result = _controller.CreateHrAccount(hrEmployee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void CreateHrAccount_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var hrEmployee = new HREmployeeCreationDto(); // Create a sample HREmployeeCreationDto object
            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = _controller.CreateHrAccount(hrEmployee);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HREmployeeCreationDto>(viewResult.Model);
            Assert.Equal(hrEmployee, model);
        }

        [Fact]
        public void EditHrAccountView_ReturnsViewWithHREmployeeUpdationDto()
        {
            // Arrange
            int id = 1;
            var hrEmployee = new HrEmployee(); // Create a sample HREmployee object
            _serviceMock.Setup(s => s.GetById(id)).Returns(hrEmployee);

            // Act
            var result = _controller.EditHrAccountView(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HREmployeeUpdationDto>(viewResult.Model);
            Assert.Equal(hrEmployee.Name, model.Name);
            Assert.Equal(hrEmployee.Email, model.Email);
            Assert.Equal(hrEmployee.Password, model.Password);
            Assert.Equal(hrEmployee.Id, model.Id);
        }

        [Fact]
        public void EditHrAccount_WithValidModel_ReturnsRedirectToHrList()
        {
            // Arrange
            var hrEmployee = new HREmployeeUpdationDto(); // Create a sample HREmployeeUpdationDto object
            _serviceMock.Setup(s => s.EditHrAccount(hrEmployee)).Returns(new LogContent { Result = true });

            // Act
            var result = _controller.EditHrAccount(hrEmployee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void EditHrAccount_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var hrEmployee = new HREmployeeUpdationDto(); // Create a sample HREmployeeUpdationDto object
            _controller.ModelState.AddModelError("Email", "Email is required");
            _serviceMock.Setup(s => s.EditHrAccount(hrEmployee)).Returns(new LogContent { Result = false });

            // Act
            var result = _controller.EditHrAccount(hrEmployee);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HREmployeeUpdationDto>(viewResult.Model);
            Assert.Equal(hrEmployee, model);
        }

        [Fact]
        public void DeleteHrAccount_ReturnsRedirectToHrList()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.DeleteHrAccount(id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Update_ReturnsRedirectToHrList()
        {
            // Arrange
            var hrEmployee = new HREmployeeUpdationDto(); // Create a sample HREmployeeUpdationDto object

            // Act
            var result = _controller.Update(hrEmployee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Add_ReturnsRedirectToHrList()
        {
            // Arrange
            var hrEmployee = new HREmployeeCreationDto(); // Create a sample HREmployeeCreationDto object

            // Act
            var result = _controller.Add(hrEmployee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("HrList", redirectToActionResult.ActionName);
        }
    }

}
