using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace G3HAS_Unit_Tests.Services
{

	public class JobPostingsServiceTests
	{
		private JobPostingsService _jobPostingsService;
		private Mock<IJobPostingsRepository> _mockRepository;
		private Mock<IMapper> _mockMapper;
		private Mock<ILogger<JobPostingsService>> _mockLogger;

		public JobPostingsServiceTests()
		{
			_mockRepository = new Mock<IJobPostingsRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockLogger = new Mock<ILogger<JobPostingsService>>();
			_jobPostingsService = new JobPostingsService(_mockRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_ReturnsListOfJobPostings()
		{
			// Arrange
			var expectedJobPostings = new List<JobPostings>
			{
				new JobPostings { Id = 1, Name = "Job 1" },
				new JobPostings { Id = 2, Name = "Job 2" }
			};
			var expectedQueryable = expectedJobPostings.AsQueryable();
			_mockRepository.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(expectedQueryable);

			// Act
			var jobPostings = await _jobPostingsService.RetrieveAllAsync();

			// Assert
			Assert.NotNull(jobPostings);
			Assert.Equal(expectedJobPostings.Count, jobPostings.Count);
		}

		[Fact]
		public async Task AddAsync_ValidJobPostingDto_AddsJobPostingToRepository()
		{
			// Arrange
			var loggedUser = new IdentityUser { UserName = "testUser" };
			var jobPostingsDto = new JobPostingsCreationDto
			{
				Name = "Test Job",
				QualificationList = new List<string>(), 
				ResponsibilityList = new List<string>(),
			};

			var expectedJobPostingsModel = new JobPostings
			{
				Name = jobPostingsDto.Name,
			};
			_mockMapper.Setup(mapper => mapper.Map<JobPostings>(jobPostingsDto)).Returns(expectedJobPostingsModel);
			_mockRepository.Setup(repo => repo.AddAsync(It.IsAny<JobPostings>())).Verifiable();

			// Act
			await _jobPostingsService.AddAsync(jobPostingsDto, loggedUser);

			// Assert
			_mockMapper.Verify(mapper => mapper.Map<JobPostings>(jobPostingsDto), Times.Once);
			_mockRepository.Verify(repo => repo.AddAsync(expectedJobPostingsModel), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_ValidId_ReturnsJobPosting()
		{
			// Arrange
			int jobId = 1;
			var expectedJobPosting = new JobPostings
			{
				Id = jobId,
				Name = "Test Job"
			};
			_mockRepository.Setup(repo => repo.GetByIdAsync(jobId)).ReturnsAsync(expectedJobPosting);

			// Act
			var jobPosting = await _jobPostingsService.GetByIdAsync(jobId);

			// Assert
			Assert.NotNull(jobPosting);
			Assert.Equal(expectedJobPosting.Id, jobPosting.Id);
			Assert.Equal(expectedJobPosting.Name, jobPosting.Name);
		}

		[Fact]
		public async Task GetByIdAsync_NonExistentId_ReturnsNull()
		{
			// Arrange
			int nonExistentId = 999;
			_mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync((JobPostings)null);

			// Act
			var jobPosting = await _jobPostingsService.GetByIdAsync(nonExistentId);

			// Assert
			Assert.Null(jobPosting);
		}

		[Fact]
		public async Task UpdateAsync_ValidJobPosting_ReturnsUpdatedJobPosting()
		{
			// Arrange
			var jobPostingsDto = new JobPostingsUpdationDto
			{
				Id = 1,
				Name = "Updated Job",
				QualificationList = new List<string>(),
				ResponsibilityList = new List<string>(),
			};

			var loggedUser = new IdentityUser { UserName = "testUser" };

			var jobPostingsModel = new JobPostings
			{
				Id = jobPostingsDto.Id,
				Name = "Existing Job",
			};

			_mockMapper.Setup(mapper => mapper.Map<JobPostings>(jobPostingsDto)).Returns(() =>
			{
				var updatedModel = new JobPostings
				{
					Id = jobPostingsDto.Id,
					Name = jobPostingsDto.Name,
					UpdatedBy = loggedUser.UserName,
					UpdatedTime = DateTime.Now
				};
				return updatedModel;
			});

			_mockRepository.Setup(repo => repo.GetByIdAsync(jobPostingsDto.Id)).ReturnsAsync(jobPostingsModel);

			// Act
			var updatedJobPostingsModel = _mockMapper.Object.Map<JobPostings>(jobPostingsDto);
			await _jobPostingsService.UpdateAsync(jobPostingsDto, loggedUser);

			// Assert
			_mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<JobPostings>()), Times.Once);
			Assert.Equal(jobPostingsDto.Name, updatedJobPostingsModel.Name);
			Assert.Equal(loggedUser.UserName, updatedJobPostingsModel.UpdatedBy);
			Assert.True(updatedJobPostingsModel.UpdatedTime > DateTime.MinValue);
		}

		[Fact]
		public async Task SemiDeleteAsync_ExistingJobPosting_SetsIsDeletedToTrue()
		{
			// Arrange
			int jobId = 1;
			var jobPosting = new JobPostings
			{
				Id = jobId,
				Name = "Test Job",
				IsDeleted = false,
			};

			_mockRepository.Setup(repo => repo.GetByIdAsync(jobId)).ReturnsAsync(jobPosting);

			// Act
			await _jobPostingsService.SemiDeleteAsync(jobId);

			// Assert
			Assert.True(jobPosting.IsDeleted);
		}

		[Fact]
		public async Task PermaDeleteAsync_ExistingJobPosting_DeletesFromRepository()
		{
			// Arrange
			int jobId = 1;
			var jobPosting = new JobPostings
			{
				Id = jobId,
				Name = "Test Job",
			};

			_mockRepository.Setup(repo => repo.GetByIdAsync(jobId)).ReturnsAsync(jobPosting);

			// Act
			await _jobPostingsService.PermaDeleteAsync(jobId);

			// Assert
			_mockRepository.Verify(repo => repo.PermaDeleteAsync(jobId), Times.Once);
		}

		[Fact]
		public async Task GetByNameAsync_ExistingJobPosting_ReturnsJobPosting()
		{
			// Arrange
			var jobPostingName = "Test Job";
			var expectedJobPosting = new JobPostings { Id = 1, Name = jobPostingName };
			_mockRepository.Setup(repo => repo.GetByNameAsync(jobPostingName)).ReturnsAsync(expectedJobPosting);

			// Act
			var result = await _jobPostingsService.GetByNameAsync(jobPostingName);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedJobPosting.Id, result.Id);
			Assert.Equal(expectedJobPosting.Name, result.Name);
		}

		[Fact]
		public async Task GetByNameAsync_NonExistentJobPosting_ReturnsNull()
		{
			// Arrange
			var jobPostingName = "NonExistentJob";
			_mockRepository.Setup(repo => repo.GetByNameAsync(jobPostingName)).ReturnsAsync((JobPostings)null);

			// Act
			var result = await _jobPostingsService.GetByNameAsync(jobPostingName);

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public async Task CreateJobPosting_JobAlreadyPosted_ReturnsLogContentWithErrorCode400()
		{
			// Arrange
			var jobPostingDto = new JobPostingsCreationDto
			{
				Name = "Existing Job",
			};

			var existingJob = new JobPostings { Id = 1, Name = jobPostingDto.Name };
			_mockRepository.Setup(repo => repo.GetByNameAsync(jobPostingDto.Name)).ReturnsAsync(existingJob);

			// Act
			var logContent = await _jobPostingsService.CreateJobPosting(jobPostingDto);

			// Assert
			Assert.NotNull(logContent);
			Assert.False(logContent.Result);
			Assert.Equal("400", logContent.ErrorCode);
			Assert.Equal("Job already posted!", logContent.Message);
		}

		[Fact]
		public async Task UpdateJobPosting_SameNameAsCurrent_ReturnsLogContentWithResultTrue()
		{
			// Arrange
			var jobPostingsDto = new JobPostingsUpdationDto
			{
				Id = 1,
				Name = "Existing Job",
				QualificationList = new System.Collections.Generic.List<string>(),
				ResponsibilityList = new System.Collections.Generic.List<string>(),
			};

			var existingJob = new JobPostings { Id = 1, Name = jobPostingsDto.Name };
			_mockRepository.Setup(repo => repo.GetByIdAsync(jobPostingsDto.Id)).ReturnsAsync(existingJob);

			// Act
			var logContent = await _jobPostingsService.UpdateJobPosting(jobPostingsDto);

			// Assert
			Assert.NotNull(logContent);
			Assert.True(logContent.Result);
			Assert.True(string.IsNullOrEmpty(logContent.ErrorCode));
			Assert.True(string.IsNullOrEmpty(logContent.Message));
		}


		[Fact]
		public async Task UpdateJobPosting_JobWithIdDoesNotExist_ReturnsLogContentWithErrorCode400()
		{
			// Arrange
			var jobPostingsDto = new JobPostingsUpdationDto
			{
				Id = 999,
				Name = "Updated Job",
				QualificationList = new System.Collections.Generic.List<string>(),
				ResponsibilityList = new System.Collections.Generic.List<string>(),
			};

			_mockRepository.Setup(repo => repo.GetByIdAsync(jobPostingsDto.Id)).ReturnsAsync((JobPostings)null);

			// Act
			var logContent = await _jobPostingsService.UpdateJobPosting(jobPostingsDto);

			// Assert
			Assert.NotNull(logContent);
			Assert.False(logContent.Result);
			Assert.Equal("400. Edit Failed!", logContent.ErrorCode);
			Assert.Equal("Job with ID doesn't exist.", logContent.Message);
		}
	}
}
