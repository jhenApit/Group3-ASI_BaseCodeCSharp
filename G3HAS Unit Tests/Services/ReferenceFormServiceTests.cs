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
	public class ReferenceFormsServiceTests
	{
		private ReferenceFormsService _referenceFormsService;
		private Mock<IReferenceFormsRepository> _mockRepository;
		private Mock<IMapper> _mockMapper;

		public ReferenceFormsServiceTests()
		{
			_mockRepository = new Mock<IReferenceFormsRepository>();
			_mockMapper = new Mock<IMapper>();
			_referenceFormsService = new ReferenceFormsService(_mockRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_ReturnsListOfReferenceForms()
		{
			// Arrange
			var expectedReferenceForms = new List<ReferenceForms>
			{
				new ReferenceForms { Id = 1, CharacterReferenceId = 1 },
				new ReferenceForms { Id = 2, CharacterReferenceId = 1 }
			};
			var expectedQueryable = expectedReferenceForms.AsQueryable();
			_mockRepository.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(expectedQueryable);

			// Act
			var referenceForms = await _referenceFormsService.RetrieveAllAsync();

			// Assert
			Assert.NotNull(referenceForms);
			Assert.Equal(expectedReferenceForms.Count, referenceForms.Count);
		}

		[Fact]
		public async Task AddAsync_ValidReferenceFormsDto_CallsRepositoryAddAsync()
		{
			// Arrange
			var referenceFormsDto = new ReferenceFormsCreationDto
			{
				CharacterReferenceId = 101,
				// Add other properties as required for the test
			};
			var expectedReferenceFormsModel = new ReferenceForms
			{
				CharacterReferenceId = referenceFormsDto.CharacterReferenceId,
				// Set other properties as expected in the model
			};
			_mockMapper.Setup(mapper => mapper.Map<ReferenceForms>(referenceFormsDto)).Returns(expectedReferenceFormsModel);

			// Act
			await _referenceFormsService.AddAsync(referenceFormsDto);

			// Assert
			_mockRepository.Verify(repo => repo.AddAsync(expectedReferenceFormsModel), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_ValidId_ReturnsReferenceForms()
		{
			// Arrange
			int referenceFormsId = 1;
			var expectedReferenceForms = new ReferenceForms { Id = referenceFormsId };
			_mockRepository.Setup(repo => repo.GetByIdAsync(referenceFormsId)).ReturnsAsync(expectedReferenceForms);

			// Act
			var referenceForms = await _referenceFormsService.GetByIdAsync(referenceFormsId);

			// Assert
			Assert.NotNull(referenceForms);
			Assert.Equal(referenceFormsId, referenceForms.Id);
		}

		[Fact]
		public async Task GetByCharacterReferenceIdAsync_ValidCharacterReferenceId_ReturnsReferenceForms()
		{
			// Arrange
			int characterReferenceId = 101;
			var expectedReferenceForms = new ReferenceForms { CharacterReferenceId = characterReferenceId };
			_mockRepository.Setup(repo => repo.GetByCharacterReferenceIdAsync(characterReferenceId)).ReturnsAsync(expectedReferenceForms);

			// Act
			var referenceForms = await _referenceFormsService.GetByCharacterReferenceIdAsync(characterReferenceId);

			// Assert
			Assert.NotNull(referenceForms);
			Assert.Equal(characterReferenceId, referenceForms.CharacterReferenceId);
		}
	}
}