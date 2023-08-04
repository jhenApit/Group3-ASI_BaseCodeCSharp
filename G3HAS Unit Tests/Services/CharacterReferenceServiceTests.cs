using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class CharacterReferenceServiceTests
	{
		private readonly Mock<ICharacterReferencesRepository> _repositoryMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly ICharacterReferencesService _service;

		public CharacterReferenceServiceTests()
		{
			_repositoryMock = new Mock<ICharacterReferencesRepository>();
			_mapperMock = new Mock<IMapper>();
			_service = new CharacterReferencesService(_repositoryMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_IsNotEmpty_ReturnListOfCharacterReferences()
		{
			// Arrange
			var characterReferencesList = new List<CharacterReferences>
			{
				new CharacterReferences { Id = 1, Name = "Ref 1" },
				new CharacterReferences { Id = 2, Name = "Ref 2" },
			};
			var queryable = characterReferencesList.AsQueryable();

			_repositoryMock.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(queryable);

			// Act
			var result = await _service.RetrieveAllAsync();

			// Assert
			Assert.Equal(characterReferencesList.Count, result.Count);
		}

		[Fact]
		public async Task AddAsync_ValidCreationDto_ReturnsTrue()
		{
			// Arrange
			var characterReferencesDto = new CharacterReferencesCreationDto
			{
				Name = "New Reference",
			};

			// Act
			await _service.AddAsync(characterReferencesDto);

			// Assert
			_repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CharacterReferences>()), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_IdExists_ReturnCharacterReference()
		{
			// Arrange
			var characterReference = new CharacterReferences { Id = 1, Name = "Ref 1" };

			_repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(characterReference);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(characterReference.Id, result.Id);
		}

		[Fact]
		public async Task GetByNameAsync_NameExists_ReturnCharacterReference()
		{
			// Arrange
			var characterReference = new CharacterReferences { Id = 1, Name = "Ref 1" };

			_repositoryMock.Setup(repo => repo.GetByNameAsync(characterReference.Name)).ReturnsAsync(characterReference);

			// Act
			var result = await _service.GetByNameAsync(characterReference.Name);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(characterReference.Name, result.Name);
		}

		[Fact]
		public async Task GetByApplicantIdAsync_ApplicantIdExists_ReturnCharacterReference()
		{
			// Arrange
			var characterReference = new List<CharacterReferences>
			{
				new CharacterReferences
				{
					Id = 1, ApplicantId = 1, Name = "Ref 1"
				},
				new CharacterReferences
				{
					Id = 2, ApplicantId = 1, Name = "Ref 2"
				},
			};

			var queryable = characterReference.AsQueryable();

			_repositoryMock.Setup(repo => repo.GetByApplicantIdAsync(1)).ReturnsAsync(queryable);

			// Act
			var result = await _service.GetByApplicantIdAsync(1);

			// Assert
			Assert.Equal(characterReference.Count, result.Count);
		}
	}
}
