using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Controllers
{
    public class JobControllerTests
    {
        private readonly Mock<IJobPostingsService> _serviceMock;
        private readonly JobController _controller;

        public JobControllerTests()
        {
            _serviceMock = new Mock<IJobPostingsService>();
            _controller = new JobController(_serviceMock.Object);
        }

        [Fact]
        public void FindJobs_ReturnsViewWithJobData()
        {
            // Arrange
            var expectedData = new List<JobPostings> 
            { 
                new JobPostings 
                { 
                    Id = 1,
                    Name = "Job 1" 
                },
                new JobPostings
                {
                    Id = 2,
                    Name = "Job 2" 
                } 
            };

            _serviceMock.Setup(s => s.RetrieveAll()).Returns(expectedData);

            // Act
            var result = _controller.FindJobs() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result.Model);
            Assert.Equal(null!, result.ViewName);
        }

        [Fact]
        public void JobDescription_WithValidId_ReturnsViewWithJobData()
        {
            // Arrange
            int jobId = 1;
            var expectedData = new JobPostings 
            { 
                Id = jobId, 
                Name = "Job 1" 
            };
            _serviceMock.Setup(s => s.GetById(jobId)).Returns(expectedData);

            // Act
            var result = _controller.JobDescription(jobId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result.Model);
            Assert.Equal(null!, result.ViewName);
        }

        [Fact]
        public void JobDescription_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int jobId = 10;
            _serviceMock.Setup(s => s.GetById(jobId)).Returns((JobPostings)null!);

            // Act
            var result = _controller.JobDescription(jobId);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
