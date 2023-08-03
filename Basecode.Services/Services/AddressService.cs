using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<List<Addresses>> RetrieveAllAsync()
        {
            var addresses = await _repository.RetrieveAllAsync();
            return addresses.ToList();
        }

        /// <summary>
        /// Adds a new address to the repository.
        /// </summary>
        /// <param name="AddressDto">The AddressCreationDto object representing the address to be added.</param>
        public async Task AddAsync(AddressCreationDto AddressDto)
        {
            var AddressModel = _mapper.Map<Addresses>(AddressDto);
            await _repository.AddAsync(AddressModel);
        }

        /// <summary>
        /// Retrieves an address by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the address.</param>
        /// <returns>The Address object matching the specified identifier, or null if not found.</returns>
        public async Task<Addresses?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// this gets the address by applicantid
        /// </summary>
        /// <param name="applicantId">the applicant id set to an address</param>
        /// <returns>returns the address model</returns>
        public async Task<Addresses?> GetByApplicantIdAsync(int applicantId)
        {
            return await _repository.GetByApplicantIdAsync(applicantId);
        }
    }
}
