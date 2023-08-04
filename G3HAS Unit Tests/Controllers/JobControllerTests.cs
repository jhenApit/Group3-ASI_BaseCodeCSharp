using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Basecode.WebApp.Tests
{
    public class JobControllerTests
    {
        private readonly Mock<IJobPostingsService> _mockJobService;
        private readonly JobController _controller;

        public JobControllerTests()
        {
            _mockJobService = new Mock<IJobPostingsService>();
            _controller = new JobController(_mockJobService.Object);
        }

        [Fact]
        public async Task FindJobs_ReturnsViewWithData()
        {
            // Arrange
            var mockData = new List<JobPostings>() { new JobPostings() { Id = 1, Name = "Job 1" }, new JobPostings() { Id = 2, Name = "Job 2" } };
            _mockJobService.Setup(service => service.RetrieveAllAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _controller.FindJobs() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockData, result.Model);
        }

        [Fact]
        public async Task FindJobs_ReturnsBadRequestOnError()
        {
            // Arrange
            _mockJobService.Setup(service => service.RetrieveAllAsync()).ThrowsAsync(new Exception("Test error"));

            // Act
            var result = await _controller.FindJobs() as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test error", result.Value);
        }

        [Fact]
        public async Task JobDescription_ReturnsViewWithData()
        {
            // Arrange
            int jobId = 1;
            var mockData = new JobPostings() { Id = jobId, Name = "Job 1" };
            _mockJobService.Setup(service => service.GetByIdAsync(jobId)).ReturnsAsync(mockData);

            // Act
            var result = await _controller.JobDescription(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockData, result.Model);
        }

        [Fact]
        public async Task JobDescription_ReturnsBadRequestOnError()
        {
            // Arrange
            int jobId = 1;
            _mockJobService.Setup(service => service.GetByIdAsync(jobId)).ThrowsAsync(new Exception("Test error"));

            // Act
            var result = await _controller.JobDescription(jobId) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test error", result.Value);
        }
    }
}
