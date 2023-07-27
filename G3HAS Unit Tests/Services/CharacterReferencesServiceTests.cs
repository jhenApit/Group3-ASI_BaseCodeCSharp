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
    public class CharacterReferencesServiceTests
    {
        private readonly Mock<ICharacterReferencesRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CharacterReferencesService _service;

        public CharacterReferencesServiceTests()
        {
            _repositoryMock = new Mock<ICharacterReferencesRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new CharacterReferencesService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ReturnListOfCharacterReferences()
        {
            // Arrange
            var characterReferencesList = new List<CharacterReferences>();
            _repositoryMock.Setup(repo => repo.RetrieveAll()).Returns(characterReferencesList.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Equal(characterReferencesList, result);
        }

        [Fact]
        public void Add_AddCharacterReferenceToRepository()
        {
            // Arrange
            var characterReferencesDto = new CharacterReferencesCreationDto();
            var characterReferencesModel = new CharacterReferences();
            _mapperMock.Setup(mapper => mapper.Map<CharacterReferences>(characterReferencesDto)).Returns(characterReferencesModel);

            // Act
            _service.Add(characterReferencesDto);

            // Assert
            _repositoryMock.Verify(repo => repo.Add(characterReferencesModel), Times.Once);
        }

        [Fact]
        public void GetById_ExistingId_ReturnCharacterReference()
        {
            // Arrange
            var id = 1;
            var characterReference = new CharacterReferences();
            _repositoryMock.Setup(repo => repo.GetById(id)).Returns(characterReference);

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.Equal(characterReference, result);
        }

        [Fact]
        public void GetByName_ExistingName_ReturnCharacterReference()
        {
            // Arrange
            var name = "John Doe";
            var characterReference = new CharacterReferences();
            _repositoryMock.Setup(repo => repo.GetByName(name)).Returns(characterReference);

            // Act
            var result = _service.GetByName(name);

            // Assert
            Assert.Equal(characterReference, result);
        }
    }

}
