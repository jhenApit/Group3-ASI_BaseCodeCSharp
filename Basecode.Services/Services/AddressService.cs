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
using Basecode.Services.Utils;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public AddressService(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all addresses from the repository.
        /// </summary>
        /// <returns>A list of Address objects.</returns>
        public List<Address> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new address to the repository.
        /// </summary>
        /// <param name="AddressDto">The AddressCreationDto object representing the address to be added.</param>
        public void Add(AddressCreationDto AddressDto)
        {
            var AddressModel = _mapper.Map<Address>(AddressDto);
            _repository.Add(AddressModel);
        }

        /// <summary>
        /// Retrieves an address by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the address.</param>
        /// <returns>The Address object matching the specified identifier, or null if not found.</returns>
        public Address GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Retrieves an address by its city name.
        /// </summary>
        /// <param name="city">The name of the city.</param>
        /// <returns>The Address object matching the specified city name, or null if not found.</returns>
        public Address GetByCity(string city)
        {
            return _repository.GetByCity(city);
        }

    }
}
