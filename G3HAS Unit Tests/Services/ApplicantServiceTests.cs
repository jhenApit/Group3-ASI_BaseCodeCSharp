using AutoMapper;
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
    public class ApplicantServiceTests
    {
        private readonly Mock<IApplicantRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicantService _service;

        public ApplicantServiceTests()
        {
            _repositoryMock = new Mock<IApplicantRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ApplicantService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetById_ValidId_ReturnsApplicant()
        {
            // Arrange
            int applicantId = 1;
            Applicants expectedApplicant = new Applicants { Id = applicantId };
            _repositoryMock.Setup(repo => repo.GetById(applicantId)).Returns(expectedApplicant);

            // Act
            Applicants actualApplicant = _service.GetById(applicantId);

            // Assert
            Assert.Equal(expectedApplicant, actualApplicant);
        }

        [Fact]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            int invalidId = -1;
            _repositoryMock.Setup(repo => repo.GetById(invalidId)).Returns((Applicants)null!);

            // Act
            Applicants actualApplicant = _service.GetById(invalidId);

            // Assert
            Assert.Null(actualApplicant);
        }
    }

}
