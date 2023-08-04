using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.Exams;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class ExamServiceTests
	{
		private readonly Mock<IExamsRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly IExamsService _service;

		public ExamServiceTests()
		{
			_repositoryMock = new Mock<IExamsRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new ExamsService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_IsNotEmpty_ReturnListOfExams()
		{
			// Arrange
			var examsList = new List<Exams>
			{
				new Exams { Id = 1, Results = true },
				new Exams { Id = 2, Results = true },
			};
			var queryable = examsList.AsQueryable();
			_repositoryMock.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(queryable);

			// Act
			var result = await _service.RetrieveAllAsync();

			// Assert
			Assert.Equal(examsList.Count, result.Count);
		}

		[Fact]
		public async Task AddAsync_ValidCreationDto_ReturnTrue()
		{
			// Arrange
			var examDto = new ExamCreationDto
			{
				ApplicantId = 1,
				ProctorId = 1,
				ExamType = "Sample",
				ExamDate = DateTime.Now
			};

			// Act
			await _service.AddAsync(examDto);

			// Assert
			_repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Exams>()), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_ShouldReturnExam()
		{
			// Arrange
			var exam = new Exams { Id = 1, Results = true };

			_repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(exam);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(exam.Id, result.Id);
		}

		[Fact]
		public async Task UpdateAsync_ValidUpdationDto_ShouldUpdateExam()
		{
			// Arrange
			var examDto = new ExamUpdationDto
			{
				Id = 1,
				Results = true
			};

			// Act
			await _service.UpdateAsync(examDto);

			// Assert
			_repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Exams>()), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_IdExists_ShouldDeleteExam()
		{
			// Arrange
			var idToDelete = 1;

			// Act
			await _service.DeleteAsync(idToDelete);

			// Assert
			_repositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
		}

		[Fact]
		public async Task GetByApplicantIdAsync_ApplicantIdExists_ShouldReturnExam()
		{
			// Arrange
			var exam = new Exams { Id = 1, Results = true };

			_repositoryMock.Setup(repo => repo.GetByApplicantIdAsync(1)).ReturnsAsync(exam);

			// Act
			var result = await _service.GetByApplicantIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(exam.Id, result.Id);
		}
	}
}
