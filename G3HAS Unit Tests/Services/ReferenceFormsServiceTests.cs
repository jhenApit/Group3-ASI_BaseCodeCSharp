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
    public class ReferenceFormsServiceTests
    {
        private readonly Mock<IReferenceFormsRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReferenceFormsService _service;

        public ReferenceFormsServiceTests()
        {
            _repositoryMock = new Mock<IReferenceFormsRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ReferenceFormsService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ShouldReturnListOfReferenceForms()
        {
            // Arrange
            var referenceFormsList = new List<ReferenceForms>();
            _repositoryMock.Setup(r => r.RetrieveAll()).Returns(referenceFormsList.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Equal(referenceFormsList, result);
        }

        [Fact]
        public void Add_ShouldAddNewReferenceForm()
        {
            // Arrange
            var referenceFormsDto = new ReferenceFormsCreationDto ();
            var referenceFormsModel = new ReferenceForms ();
            _mapperMock.Setup(m => m.Map<ReferenceForms>(referenceFormsDto)).Returns(referenceFormsModel);

            // Act
            _service.Add(referenceFormsDto);

            // Assert
            _repositoryMock.Verify(r => r.Add(referenceFormsModel), Times.Once);
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnReferenceForm()
        {
            // Arrange
            var id = 1;
            var referenceForm = new ReferenceForms ();
            _repositoryMock.Setup(r => r.GetById(id)).Returns(referenceForm);

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.Equal(referenceForm, result);
        }

        [Fact]
        public void GetById_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var id = 1;
            _repositoryMock.Setup(r => r.GetById(id)).Returns((ReferenceForms)null!);

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetByCharacterReferenceId_WithValidId_ShouldReturnReferenceForm()
        {
            // Arrange
            var characterReferenceId = 1;
            var referenceForm = new ReferenceForms ();
            _repositoryMock.Setup(r => r.GetByCharacterReferenceId(characterReferenceId)).Returns(referenceForm);

            // Act
            var result = _service.GetByCharacterReferenceId(characterReferenceId);

            // Assert
            Assert.Equal(referenceForm, result);
        }

        [Fact]
        public void GetByCharacterReferenceId_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var characterReferenceId = 1;
            _repositoryMock.Setup(r => r.GetByCharacterReferenceId(characterReferenceId)).Returns((ReferenceForms)null!);

            // Act
            var result = _service.GetByCharacterReferenceId(characterReferenceId);

            // Assert
            Assert.Null(result);
        }
    }

}
