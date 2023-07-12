using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Basecode.Services.Utils.ErrorHandling;

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
        public void AdminDashboard_ReturnsViewWithHrEmployee()
        {
            // Arrange
            string email = "test@example.com";
            var hrEmployee = new HrEmployee(); // Create a sample HrEmployee object
            _serviceMock.Setup(s => s.GetByEmail(email)).Returns(hrEmployee);

            // Act
            var result = _controller.AdminDashboard(email);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HrEmployee>(viewResult.Model);
            Assert.Equal(hrEmployee, model);
        }

        [Fact]
        public void HrList_ReturnsViewWithData()
        {
            // Arrange
            var hrEmployees = new List<HrEmployee>(); // Create a list of HrEmployee objects
            _serviceMock.Setup(s => s.RetrieveAll()).Returns(hrEmployees);

            // Act
            var result = _controller.HrList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<HrEmployee>>(viewResult.Model);
            Assert.Equal(hrEmployees, model);
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
