using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.CurrentHires;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Moq;
using static Basecode.Data.Enums.Enums;

namespace G3HAS_Unit_Tests.Services
{
	public class CurrentHiresServiceTests
	{
		private readonly Mock<ICurrentHiresRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly ICurrentHiresService _service;

		public CurrentHiresServiceTests()
		{
			_repositoryMock = new Mock<ICurrentHiresRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new CurrentHiresService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_IsNotEmpty_ReturnListOfCurrentHires()
		{
			// Arrange
			var currentHiresList = new List<CurrentHires>
			{
				new CurrentHires { Id = 1, PositionId = 1 },
				new CurrentHires { Id = 2, PositionId = 2 },
			};

			var queryable = currentHiresList.AsQueryable();
			_repositoryMock.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(queryable);

			// Act
			var result = await _service.RetrieveAllAsync();

			// Assert
			Assert.Equal(currentHiresList.Count, result.Count);
		}

		[Fact]
		public async Task AddAsync_ValidCreationDto_ReturnTrue()
		{
			// Arrange
			var currentHiresDto = new CurrentHiresCreationDto
			{
				PositionId = 1,
			};

			// Act
			await _service.AddAsync(currentHiresDto);

			// Assert
			_repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CurrentHires>()), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_IdExists_ReturnCurrentHire()
		{
			// Arrange
			var currentHire = new CurrentHires { Id = 1, PositionId = 1 };

			_repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(currentHire);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(currentHire.Id, result.Id);
		}

		[Fact]
		public async Task UpdateAsync_ValidUpdationDto_ReturnTrue()
		{
			// Arrange
			var currentHireDto = new CurrentHiresUpdationDto
			{
				Id = 1,
				HireDate = DateTime.Now
			};

			// Act
			await _service.UpdateAsync(currentHireDto);

			// Assert
			_repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<CurrentHires>()), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_ShouldDeleteCurrentHire()
		{
			// Arrange
			var idToDelete = 1;

			// Act
			await _service.DeleteAsync(idToDelete);

			// Assert
			_repositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
		}

		[Fact]
		public async Task GetByPositionIdAsync_PositionIdExists_ReturnCurrentHire()
		{
			// Arrange
			var currentHire = new CurrentHires { Id = 1, PositionId = 1 };

			_repositoryMock.Setup(repo => repo.GetByPositionIdAsync(1)).ReturnsAsync(currentHire);

			// Act
			var result = await _service.GetByPositionIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(currentHire.Id, result.Id);
		}

		[Fact]
		public async Task GetByHireStatusAsync_StatusExists_ReturnCurrentHire()
		{
			// Arrange
			var currentHire = new CurrentHires { Id = 1, PositionId = 1, HireStatus = HireStatus.Confirmed };

			_repositoryMock.Setup(repo => repo.GetByHireStatusAsync("Confirmed")).ReturnsAsync(currentHire);

			// Act
			var result = await _service.GetByHireStatusAsync("Confirmed");

			// Assert
			Assert.NotNull(result);
			Assert.Equal(currentHire.Id, result.Id);
		}
	}
}
