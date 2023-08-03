using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.RandomIDGenerator;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using static Basecode.Data.Enums.Enums;

namespace G3HAS_Unit_Tests.Services
{
	public class ApplicantServiceTests
	{
		private ApplicantService _applicantService;
		private Mock<IAddressService> _mockAddressService;
		private Mock<ICharacterReferencesService> _mockCharacterReferencesService;
		private Mock<IEmailService> _mockEmailService;
		private Mock<IApplicantRepository> _mockRepository;
		private Mock<IMapper> _mockMapper;
		private Mock<IDGenerator> _mockIdGenerator;

		public ApplicantServiceTests()
		{
			_mockAddressService = new Mock<IAddressService>();
			_mockCharacterReferencesService = new Mock<ICharacterReferencesService>();
			_mockEmailService = new Mock<IEmailService>();
			_mockRepository = new Mock<IApplicantRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockIdGenerator = new Mock<IDGenerator>();

			_applicantService = new ApplicantService(
				_mockAddressService.Object,
				_mockCharacterReferencesService.Object,
				_mockEmailService.Object,
				_mockCharacterReferencesService.Object,
				_mockRepository.Object,
				_mockMapper.Object
			);
		}

		[Fact]
		public async Task AddAsync_ValidApplicantCreationDto_ReturnsApplicantId()
		{
			// Arrange
			var applicantCreationDto = new ApplicantCreationDto
			{
				JobId = 1,
				FirstName = "John",
				MiddleName = "Doe",
				LastName = "Smith",
				BirthDate = new DateTime(1990, 5, 15),
				Resume = new byte[] { 0x12, 0x34, 0x56 },
				Photo = null,
				PhoneNumber = "09123456789",
				Email = "john@example.com",
				AdditionalInfo = new AdditionalInfo
				{
				},
				ApplicationStatus = ApplicationStatus.Received
			};


			var applicantModel = new Applicants
			{
				Id = 1,
				ApplicantId = "123DFGETB68BDS8",
				JobId = 2,
				ApplicationDate = DateTime.Now,
				FirstName = "John",
				MiddleName = "Doe",
				LastName = "Smith",
				BirthDate = new DateTime(1990, 5, 15),
				Resume = new byte[] { 0x12, 0x34, 0x56 },
				Photo = null,
				PhoneNumber = "09123456789",
				Email = "john@example.com",
				ModifiedBy = "Admin",
				ModifiedDate = DateTime.Now,
				AdditionalInfo = new AdditionalInfo
				{
				},
				ApplicationStatus = ApplicationStatus.Received,
				Requirements = Requirements.TBC
			};

			_mockMapper.Setup(mapper => mapper.Map<Applicants>(applicantCreationDto)).Returns(applicantModel);
			_mockRepository.Setup(repo => repo.AddAsync(applicantModel));

			// Act
			var result = await _applicantService.AddAsync(applicantCreationDto);

			// Assert
			Assert.Equal(applicantModel.Id, result);
		}

		[Fact]
		public async Task UpdateAsync_ValidStatus_ReturnsTrue()
		{
			// Arrange
			var applicantModel = new Applicants
			{
				Id = 1,
				ApplicantId = "123DFGETB68BDS8",
				JobId = 2,
				ApplicationDate = DateTime.Now,
				FirstName = "John",
				MiddleName = "Doe",
				LastName = "Smith",
				BirthDate = new DateTime(1990, 5, 15),
				Resume = new byte[] { 0x12, 0x34, 0x56 },
				Photo = null,
				PhoneNumber = "09123456789",
				Email = "john@example.com",
				ModifiedBy = "Admin",
				ModifiedDate = DateTime.Now,
				AdditionalInfo = new AdditionalInfo
				{
				},
				ApplicationStatus = ApplicationStatus.Received,
				Requirements = Requirements.TBC
			};
			string status = "Received";

			_mockRepository.Setup(repo => repo.GetByIdAsync(applicantModel.Id)).ReturnsAsync(applicantModel);

			// Act
			var result = await _applicantService.UpdateAsync(applicantModel.Id, status);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task UpdateAsync_InvalidStatus_ReturnsFalse()
		{
			// Arrange
			int id = 1;
			string status = "Rejected";

			_mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Applicants)null);

			// Act
			var result = await _applicantService.UpdateAsync(id, status);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public async Task GetByApplicantIdAsync_ValidId_ReturnsApplicant()
		{
			// Arrange
			string applicantId = "123DFGETB68BDS8";
			var expectedApplicant = new Applicants
			{
				Id = 1,
				ApplicantId = applicantId,
				JobId = 2,
				ApplicationDate = DateTime.Now,
				FirstName = "John",
				MiddleName = "Doe",
				LastName = "Smith",
				BirthDate = new DateTime(1990, 5, 15),
				Resume = new byte[] { 0x12, 0x34, 0x56 },
				Photo = null,
				PhoneNumber = "09123456789",
				Email = "john@example.com",
				ModifiedBy = "Admin",
				ModifiedDate = DateTime.Now,
				AdditionalInfo = new AdditionalInfo
				{
				},
				ApplicationStatus = ApplicationStatus.Received,
				Requirements = Requirements.TBC
			};

			_mockRepository.Setup(repo => repo.GetByApplicantIdAsync(expectedApplicant.ApplicantId)).ReturnsAsync(expectedApplicant);

			// Act
			var result = await _applicantService.GetByApplicantIdAsync(applicantId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedApplicant.Id, result.Id);
			Assert.Equal(expectedApplicant.ApplicantId, result.ApplicantId);
		}

		[Fact]
		public async Task GetByEmailAsync_ValidEmail_ReturnsListOfApplicants()
		{
			// Arrange
			string email = "john@example.com";
			var expectedApplicants = new List<Applicants>
			{
				new Applicants
				{
					Id = 1,
					ApplicantId = "123DFGETB68BDS8",
					JobId = 2,
					ApplicationDate = DateTime.Now,
					FirstName = "John",
					MiddleName = "Doe",
					LastName = "Smith",
					BirthDate = new DateTime(1990, 5, 15),
					Resume = new byte[] { 0x12, 0x34, 0x56 },
					Photo = null,
					PhoneNumber = "09123456789",
					Email = email,
					ModifiedBy = "Admin",
					ModifiedDate = DateTime.Now,
					AdditionalInfo = new AdditionalInfo
					{
					},
					ApplicationStatus = ApplicationStatus.Received,
					Requirements = Requirements.TBC
					},
				new Applicants
				{
					Id = 2,
					ApplicantId = "923DFGETB68BDS8",
					JobId = 3,
					ApplicationDate = DateTime.Now,
					FirstName = "John",
					MiddleName = "Doe",
					LastName = "Smith",
					BirthDate = new DateTime(1990, 5, 15),
					Resume = new byte[] { 0x12, 0x34, 0x56 },
					Photo = null,
					PhoneNumber = "09123456789",
					Email = email,
					ModifiedBy = "Admin",
					ModifiedDate = DateTime.Now,
					AdditionalInfo = new AdditionalInfo
					{
					},
					ApplicationStatus = ApplicationStatus.Received,
					Requirements = Requirements.TBC
					}
			};

			var queryableApplicants = expectedApplicants.AsQueryable();
			_mockRepository.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(queryableApplicants);

			// Act
			var result = await _applicantService.GetByEmailAsync(email);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedApplicants.Count, result.Count);
		}

		[Fact]
		public async Task GetByNameAsync_ValidName_ReturnsApplicant()
		{
			// Arrange
			var expectedApplicant = new Applicants
			{
				Id = 1,
				ApplicantId = "123DFGETB68BDS8",
				JobId = 2,
				ApplicationDate = DateTime.Now,
				FirstName = "John",
				MiddleName = "Doe",
				LastName = "Smith",
				BirthDate = new DateTime(1990, 5, 15),
				Resume = new byte[] { 0x12, 0x34, 0x56 },
				Photo = null,
				PhoneNumber = "09123456789",
				Email = "john@example.com",
				ModifiedBy = "Admin",
				ModifiedDate = DateTime.Now,
				AdditionalInfo = new AdditionalInfo
				{
				},
				ApplicationStatus = ApplicationStatus.Received,
				Requirements = Requirements.TBC
			};

			_mockRepository.Setup(repo => repo.GetByNameAsync(expectedApplicant.FirstName, expectedApplicant.MiddleName, expectedApplicant.LastName)).ReturnsAsync(expectedApplicant);

			// Act
			var result = await _applicantService.GetByNameAsync(expectedApplicant.FirstName, expectedApplicant.MiddleName, expectedApplicant.LastName);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedApplicant.Name, result.Name);
		}

	}
}
