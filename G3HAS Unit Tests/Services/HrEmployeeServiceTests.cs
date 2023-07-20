using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Services
{
    public class HrEmployeeServiceTests
    {
        private readonly Mock<IHrEmployeeRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly HrEmployeeService _service;

        public HrEmployeeServiceTests()
        {
            _repositoryMock = new Mock<IHrEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new HrEmployeeService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ReturnListOfHrEmployees()
        {
            // Arrange
            var expectedEmployees = new List<HrEmployee>();
            _repositoryMock.Setup(r => r.RetrieveAll()).Returns(expectedEmployees.AsQueryable());

            // Act
            var actualEmployees = _service.RetrieveAll();

            // Assert
            Assert.Equal(expectedEmployees, actualEmployees);
        }

        [Fact]
        public void Add_AddNewHrEmployee()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeCreationDto ();
            var hrEmployeeModel = new HrEmployee ();
            _mapperMock.Setup(m => m.Map<HrEmployee>(hrEmployeeDto)).Returns(hrEmployeeModel);

            // Act
            _service.Add(hrEmployeeDto);

            // Assert
            _repositoryMock.Verify(r => r.Add(hrEmployeeModel), Times.Once);
        }

        [Fact]
        public void GetById_ReturnHrEmployeeWithMatchingId()
        {
            // Arrange
            var expectedEmployee = new HrEmployee ();
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(expectedEmployee);
            var id = 1;

            // Act
            var actualEmployee = _service.GetById(id);

            // Assert
            Assert.Equal(expectedEmployee, actualEmployee);
        }

        [Fact]
        public void Update_UpdateExistingHrEmployee()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeUpdationDto ();
            var hrEmployeeModel = new HrEmployee ();
            _mapperMock.Setup(m => m.Map<HrEmployee>(hrEmployeeDto)).Returns(hrEmployeeModel);

            // Act
            _service.Update(hrEmployeeDto);

            // Assert
            _repositoryMock.Verify(r => r.Update(hrEmployeeModel), Times.Once);
        }

        [Fact]
        public void SemiDelete_MarkHrEmployeeAsDeleted()
        {
            // Arrange
            var hrEmployee = new HrEmployee ();
            var id = 1;
            _repositoryMock.Setup(r => r.GetById(id)).Returns(hrEmployee);

            // Act
            _service.SemiDelete(id);

            // Assert
            Assert.True(hrEmployee.IsDeleted);
            _repositoryMock.Verify(r => r.SemiDelete(hrEmployee), Times.Once);
        }

        [Fact]
        public void PermaDelete_PermanentlyDeleteHrEmployee()
        {
            // Arrange
            var id = 1;

            // Act
            _service.PermaDelete(id);

            // Assert
            _repositoryMock.Verify(r => r.PermaDelete(id), Times.Once);
        }

        [Fact]
        public void GetByEmail_ReturnHrEmployeeWithMatchingEmail()
        {
            // Arrange
            var expectedEmployee = new HrEmployee ();
            var email = "test@example.com";
            _repositoryMock.Setup(r => r.GetByEmail(email)).Returns(expectedEmployee);

            // Act
            var actualEmployee = _service.GetByEmail(email);

            // Assert
            Assert.Equal(expectedEmployee, actualEmployee);
        }

        [Fact]
        public void CreateHrAccount_WithExistingEmail_ReturnErrorLogContent()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeCreationDto ();
            var existingHrEmployee = new HrEmployee ();
            _repositoryMock.Setup(r => r.GetByEmail(hrEmployeeDto.Email)).Returns(existingHrEmployee);

            // Act
            var logContent = _service.CreateHrAccount(hrEmployeeDto);

            // Assert
            Assert.False(logContent.Result);
            Assert.Equal("400", logContent.ErrorCode);
            Assert.Equal("Email already registered.", logContent.Message);
        }

        [Fact]
        public void CreateHrAccount_WithNonExistingEmail_ReturnSuccessLogContent()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeCreationDto ();
            _repositoryMock.Setup(r => r.GetByEmail(hrEmployeeDto.Email)).Returns((HrEmployee)null!);

            // Act
            var logContent = _service.CreateHrAccount(hrEmployeeDto);

            // Assert
            Assert.True(logContent.Result);
        }

        [Fact]
        public void EditHrAccount_WithExistingEmailAndSameId_ReturnSuccessLogContent()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeUpdationDto ();
            var existingHrEmployee = new HrEmployee ();
            existingHrEmployee.Id = hrEmployeeDto.Id;
            _repositoryMock.Setup(r => r.GetByEmail(hrEmployeeDto.Email)).Returns(existingHrEmployee);

            // Act
            var logContent = _service.EditHrAccount(hrEmployeeDto);

            // Assert
            Assert.True(logContent.Result);
        }

        [Fact]
        public void EditHrAccount_WithNonExistingEmail_ReturnSuccessLogContent()
        {
            // Arrange
            var hrEmployeeDto = new HREmployeeUpdationDto ();
            _repositoryMock.Setup(r => r.GetByEmail(hrEmployeeDto.Email)).Returns((HrEmployee)null!);

            // Act
            var logContent = _service.EditHrAccount(hrEmployeeDto);

            // Assert
            Assert.True(logContent.Result);
        }
    }

}
