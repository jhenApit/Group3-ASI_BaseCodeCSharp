using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class AddressServiceTests
	{
		private AddressService _addressService;
        private Mock<IAddressRepository> _mockRepository;
		private Mock<IMapper> _mockMapper;

		public AddressServiceTests()
		{
			_mockRepository = new Mock<IAddressRepository>();
			_mockMapper = new Mock<IMapper>();

			_addressService = new AddressService(_mockRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task RetrieveAllAsync_AddressIsNotEmpty_ReturnsAllAddresses()
		{
			// Arrange
			var expectedAddresses = new List<Addresses>
			{
				new Addresses
				{
					Id = 1,
					ApplicantId = 1,
					Street = "123 Main St",
					City = "Example City",
					Province = "Example Province",
					ZipCode = "1234"
				},
				new Addresses
				{
					Id = 2,
					ApplicantId = 4,
					Street = "123 Main St",
					City = "Example City",
					Province = "Example Province",
					ZipCode = "1234"
				}
		};

			var expectedQueryable = expectedAddresses.AsQueryable();
			_mockRepository.Setup(repo => repo.RetrieveAllAsync()).ReturnsAsync(expectedQueryable);

			// Act
			var addresses = await _addressService.RetrieveAllAsync();

			// Assert
			Assert.Equal(expectedAddresses.Count, addresses.Count);
		}

		[Fact]
		public async Task AddAsync_ValidAddressModel_ReturnsAddress()
		{
			// Arrange
			var addressDto = new AddressCreationDto
			{
				ApplicantId = 1,
				Street = "123 Main St",
				City = "Example City",
				Province = "Example Province",
				ZipCode = "1234"
			};
			var addressModel = new Addresses 
			{
				Id = 1,
				ApplicantId = 1,
				Street = "123 Main St",
				City = "Example City",
				Province = "Example Province",
				ZipCode = "1234"
			};

			_mockMapper.Setup(mapper => mapper.Map<Addresses>(addressDto)).Returns(addressModel);

			// Act
			await _addressService.AddAsync(addressDto);

			// Assert
			_mockRepository.Verify(repo => repo.AddAsync(addressModel), Times.Once);
		}

		[Fact]
		public async Task GetByIdAsync_IDExists_ReturnsAddress()
		{
			// Arrange
			int addressId = 1;
			var expectedAddress = new Addresses
			{
				Id = addressId,
				ApplicantId = 1,
				Street = "123 Main St",
				City = "Example City",
				Province = "Example Province",
				ZipCode = "1234"
			};

			_mockRepository.Setup(repo => repo.GetByIdAsync(addressId)).ReturnsAsync(expectedAddress);

			// Act
			var address = await _addressService.GetByIdAsync(addressId);

			// Assert
			Assert.NotNull(address);
			Assert.Equal(expectedAddress.Id, address.Id);
		}

		[Fact]
		public async Task GetByApplicantIdAsync_ApplicantIdExists_ReturnsAddress()
		{
			// Arrange
			int applicantId = 1;
			var expectedAddress = new Addresses
			{
				Id = 1,
				ApplicantId = applicantId,
				Street = "123 Main St",
				City = "Example City",
				Province = "Example Province",
				ZipCode = "1234"
			};

			_mockRepository.Setup(repo => repo.GetByApplicantIdAsync(applicantId)).ReturnsAsync(expectedAddress);

			// Act
			var address = await _addressService.GetByApplicantIdAsync(applicantId);

			// Assert
			Assert.NotNull(address);
			Assert.Equal(expectedAddress.ApplicantId, address.ApplicantId);

		}

		[Fact]
		public async Task GetByIdAsync_IDDoesNotExist_ReturnsNull()
		{
			// Arrange
			int nonExistentAddressId = 999;

			_mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentAddressId)).ReturnsAsync((Addresses)null);

			// Act
			var address = await _addressService.GetByIdAsync(nonExistentAddressId);

			// Assert
			Assert.Null(address);
		}

		[Fact]
		public async Task GetByApplicantIdAsync_ApplicantIdDoesNotExist_ReturnsNull()
		{
			// Arrange
			int nonExistentApplicantId = 999;

			_mockRepository.Setup(repo => repo.GetByApplicantIdAsync(nonExistentApplicantId)).ReturnsAsync((Addresses)null);

			// Act
			var address = await _addressService.GetByApplicantIdAsync(nonExistentApplicantId);

			// Assert
			Assert.Null(address);
		}
	}
}

