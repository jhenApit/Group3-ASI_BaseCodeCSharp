using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class HrEmployeeServiceTests
	{
		private readonly Mock<IHrEmployeeRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
		private readonly IHrEmployeeService _service;

		public HrEmployeeServiceTests()
		{
			_repositoryMock = new Mock<IHrEmployeeRepository>();
			_mapperMock = new Mock<IMapper>();
			_userManagerMock = new Mock<UserManager<IdentityUser>>(
				Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

			_service = new HrEmployeeService(_repositoryMock.Object, _mapperMock.Object, _userManagerMock.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_IsNotEmpty_ReturnListOfHrEmployees()
		{
			// Arrange
			var hrList = new List<HrEmployee>
			{
				new HrEmployee { Id = 1, Name = "Hr 1" },
				new HrEmployee { Id = 2, Name = "Hr 2" },
			};
			var queryable = hrList.AsQueryable();
			_repositoryMock.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(queryable);

			// Act
			var result = await _service.RetrieveAllAsync();

			// Assert
			Assert.Equal(hrList.Count, result.Count);
		}

		[Fact]
		public async Task AddAsync_ValidCreationDto_ShouldAddHrEmployee()
		{
			// Arrange
			var hrEmployeeDto = new HREmployeeCreationDto
			{
				Name = "Test Employee",
			};

			var hrEmployeeModel = new HrEmployee();
			_mapperMock.Setup(mapper => mapper.Map<HrEmployee>(hrEmployeeDto)).Returns(hrEmployeeModel);

			// Act
			await _service.AddAsync(hrEmployeeDto);

			// Assert
			_repositoryMock.Verify(repo => repo.AddAsync(hrEmployeeModel), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_IdExists_ReturnHrEmployee()
		{
			// Arrange
			var hrEmployeeId = 1;
			var hrEmployee = new HrEmployee { Id = hrEmployeeId, Name = "Test Employee" };
			_repositoryMock.Setup(repo => repo.GetByIdAsync(hrEmployeeId)).ReturnsAsync(hrEmployee);

			// Act
			var result = await _service.GetByIdAsync(hrEmployeeId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(hrEmployeeId, result.Id);
		}

		[Fact]
		public async Task GetByUserIdAsync_UserIdExists_ShouldReturnHrEmployee()
		{
			// Arrange
			var userId = "user123";
			var hrEmployee = new HrEmployee { Id = 1, Name = "Test Employee" };
			_repositoryMock.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync(hrEmployee);

			// Act
			var result = await _service.GetByUserIdAsync(userId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(hrEmployee.Id, result.Id);
		}

		[Fact]
		public async Task UpdateAsync_ValidUpdationDto_ShouldUpdateHrEmployee()
		{
			// Arrange
			var hrEmployeeDto = new HREmployeeUpdationDto
			{
				Id = 1,
				Name = "Updated Employee"
			};

			var hrEmployeeModel = new HrEmployee();
			_mapperMock.Setup(mapper => mapper.Map<HrEmployee>(hrEmployeeDto)).Returns(hrEmployeeModel);

			// Act
			await _service.UpdateAsync(hrEmployeeDto);

			// Assert
			_repositoryMock.Verify(repo => repo.UpdateAsync(hrEmployeeModel), Times.Once);
		}
	}
}
