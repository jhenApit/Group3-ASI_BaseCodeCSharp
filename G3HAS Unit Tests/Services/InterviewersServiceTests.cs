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
    public class InterviewersServiceTests
    {
        private readonly Mock<IInterviewersRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly InterviewersService _service;

        public InterviewersServiceTests()
        {
            _repositoryMock = new Mock<IInterviewersRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new InterviewersService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ShouldReturnListOfInterviewers()
        {
            // Arrange
            var interviewersList = new List<Interviewers>
        {
            new Interviewers { Id = 1, Name = "John", Email = "john@example.com" },
            new Interviewers { Id = 2, Name = "Jane", Email = "jane@example.com" }
        };
            _repositoryMock.Setup(repo => repo.RetrieveAll()).Returns(interviewersList.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Equal(interviewersList, result);
        }

        [Fact]
        public void Add_ShouldAddNewInterviewerToRepository()
        {
            // Arrange
            var interviewerToAdd = new Interviewers { Name = "John", Email = "john@example.com" };

            _repositoryMock.Setup(repo => repo.Add(It.IsAny<Interviewers>())).Verifiable();

            // Act
            _service.Add(interviewerToAdd);

            // Assert
            _repositoryMock.Verify(repo => repo.Add(It.IsAny<Interviewers>()), Times.Once);
        }

        [Fact]
        public void GetById_ExistingId_ShouldReturnInterviewer()
        {
            // Arrange
            var interviewerId = 1;
            var expectedInterviewer = new Interviewers { Id = 1, Name = "John", Email = "john@example.com" };
            _repositoryMock.Setup(repo => repo.GetById(interviewerId)).Returns(expectedInterviewer);

            // Act
            var result = _service.GetById(interviewerId);

            // Assert
            Assert.Equal(expectedInterviewer, result);
        }

        [Fact]
        public void GetById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var interviewerId = 100;
            _repositoryMock.Setup(repo => repo.GetById(interviewerId)).Returns(null as Interviewers);

            // Act
            var result = _service.GetById(interviewerId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_ShouldUpdateInterviewerInRepository()
        {
            // Arrange
            var interviewerId = 1;
            var interviewersDto = new InterviewersUpdationDto { Name = "John Doe" };
            var expectedInterviewer = new Interviewers { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _mapperMock.Setup(mapper => mapper.Map<Interviewers>(interviewersDto)).Returns(expectedInterviewer);

            // Act
            _service.Update(interviewersDto);

            // Assert
            _repositoryMock.Verify(repo => repo.Update(expectedInterviewer), Times.Once);
        }

        [Fact]
        public void Delete_ShouldDeleteInterviewerFromRepository()
        {
            // Arrange
            var interviewerId = 1;

            // Act
            _service.Delete(interviewerId);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(interviewerId), Times.Once);
        }

        [Fact]
        public void GetByName_ExistingName_ShouldReturnInterviewer()
        {
            // Arrange
            var interviewerName = "John";
            var expectedInterviewer = new Interviewers { Id = 1, Name = "John", Email = "john@example.com" };
            _repositoryMock.Setup(repo => repo.GetByName(interviewerName)).Returns(expectedInterviewer);

            // Act
            var result = _service.GetByName(interviewerName);

            // Assert
            Assert.Equal(expectedInterviewer, result);
        }

        [Fact]
        public void GetByName_NonExistingName_ShouldReturnNull()
        {
            // Arrange
            var interviewerName = "Unknown";
            _repositoryMock.Setup(repo => repo.GetByName(interviewerName)).Returns(null as Interviewers);

            // Act
            var result = _service.GetByName(interviewerName);

            // Assert
            Assert.Null(result);
        }
    }

}
