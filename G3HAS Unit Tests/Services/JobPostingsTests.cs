using AutoMapper;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Services
{
    public class JobPostingsServiceTests
    {
        private readonly Mock<IJobPostingsRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly JobPostingsService _service;

        public JobPostingsServiceTests()
        {
            _repositoryMock = new Mock<IJobPostingsRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _service = new JobPostingsService(_repositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ReturnsListOfJobPostings()
        {
            // Arrange
            var jobPostings = new List<JobPostings>();
            _repositoryMock.Setup(r => r.RetrieveAll()).Returns(jobPostings.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Equal(jobPostings, result);
        }

        [Fact]
        public void Add_CallsRepositoryAddWithMappedModel()
        {
            // Arrange
            var jobPostingsDto = new JobPostingsCreationDto();
            var jobPostingsModel = new JobPostings();
            _mapperMock.Setup(m => m.Map<JobPostings>(jobPostingsDto)).Returns(jobPostingsModel);

            // Act
            _service.Add(jobPostingsDto);

            // Assert
            _repositoryMock.Verify(r => r.Add(jobPostingsModel), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsJobPostingsFromRepository()
        {
            // Arrange
            var jobId = 1;
            var jobPostings = new JobPostings();
            _repositoryMock.Setup(r => r.GetById(jobId)).Returns(jobPostings);

            // Act
            var result = _service.GetById(jobId);

            // Assert
            Assert.Equal(jobPostings, result);
        }

        [Fact]
        public void Update_CallsRepositoryUpdateWithMappedModel()
        {
            // Arrange
            var jobPostingsDto = new JobPostingsUpdationDto();
            var jobPostingsModel = new JobPostings();
            _mapperMock.Setup(m => m.Map<JobPostings>(jobPostingsDto)).Returns(jobPostingsModel);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = principal
            };
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            // Act
            _service.Update(jobPostingsDto);

            // Assert
            Assert.Equal(1, jobPostingsModel.UpdatedById);
            _repositoryMock.Verify(r => r.Update(jobPostingsModel), Times.Once);
        }


        [Fact]
        public void SemiDelete_CallsRepositorySemiDeleteAndUpdateJobPostings()
        {
            // Arrange
            var jobId = 1;
            var jobPostings = new JobPostings();
            _repositoryMock.Setup(r => r.GetById(jobId)).Returns(jobPostings);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            // Act
            _service.SemiDelete(jobId);

            // Assert
            Assert.True(jobPostings.IsDeleted);
            Assert.Equal(1, jobPostings.UpdatedById);
            _repositoryMock.Verify(r => r.SemiDelete(jobPostings), Times.Once);
        }


        [Fact]
        public void PermaDelete_CallsRepositoryPermaDelete()
        {
            // Arrange
            var jobId = 1;

            // Act
            _service.PermaDelete(jobId);

            // Assert
            _repositoryMock.Verify(r => r.PermaDelete(jobId), Times.Once);
        }

        [Fact]
        public void GetByName_ReturnsJobPostingsFromRepository()
        {
            // Arrange
            var jobName = "Test Job";
            var jobPostings = new JobPostings();
            _repositoryMock.Setup(r => r.GetByName(jobName)).Returns(jobPostings);

            // Act
            var result = _service.GetByName(jobName);

            // Assert
            Assert.Equal(jobPostings, result);
        }
    }

}
