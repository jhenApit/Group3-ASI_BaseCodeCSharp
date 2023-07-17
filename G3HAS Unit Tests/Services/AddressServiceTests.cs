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
    public class AddressServiceTests
    {
        private readonly Mock<IAddressRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddressService _service;

        public AddressServiceTests()
        {
            _repositoryMock = new Mock<IAddressRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new AddressService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void RetrieveAll_ReturnAllAddresses()
        {
            // Arrange
            var addresses = new List<Address>
            {
                new Address { Id = 1, City = "City1" },
                new Address { Id = 2, City = "City2" }
            };
            _repositoryMock.Setup(r => r.RetrieveAll()).Returns(addresses.AsQueryable());

            // Act
            var result = _service.RetrieveAll();

            // Assert
            Assert.Equal(addresses.Count, result.Count);
            Assert.Equal(addresses.First().City, result.First().City);
        }

        [Fact]
        public void Add_ValidAddressDto_AddAddressToRepository()
        {
            // Arrange
            var addressDto = new AddressCreationDto { City = "New City" };
            var addressModel = new Address { City = "New City" };
            _mapperMock.Setup(m => m.Map<Address>(addressDto)).Returns(addressModel);

            // Act
            _service.Add(addressDto);

            // Assert
            _repositoryMock.Verify(r => r.Add(addressModel), Times.Once);
        }

        [Fact]
        public void GetById_ExistingId_ReturnMatchingAddress()
        {
            // Arrange
            int id = 1;
            var address = new Address { Id = id, City = "City1" };
            _repositoryMock.Setup(r => r.GetById(id)).Returns(address);

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.Equal(address.City, result.City);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnNull()
        {
            // Arrange
            int id = 999;
            _repositoryMock.Setup(r => r.GetById(id)).Returns((Address)null!);

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetByCity_ExistingCity_ReturnMatchingAddress()
        {
            // Arrange
            string city = "City1";
            var address = new Address { Id = 1, City = city };
            _repositoryMock.Setup(r => r.GetByCity(city)).Returns(address);

            // Act
            var result = _service.GetByCity(city);

            // Assert
            Assert.Equal(address.Id, result.Id);
        }

        [Fact]
        public void GetByCity_NonExistingCity_ReturnNull()
        {
            // Arrange
            string city = "NonExistingCity";
            _repositoryMock.Setup(r => r.GetByCity(city)).Returns((Address)null!);

            // Act
            var result = _service.GetByCity(city);

            // Assert
            Assert.Null(result);
        }
    }
}
