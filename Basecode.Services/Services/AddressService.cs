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
using Microsoft.Extensions.Logging;
using NLog;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
            try
            {
                var addresses = await _repository.RetrieveAllAsync();
                _logger.Info("Address data retrieved successfully");
                return addresses.ToList();
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Failed to Retrieve Addresses");
                throw;
            }
        }

        /// <summary>
        /// Adds a new address to the repository.
        /// </summary>
        /// <param name="AddressDto">The AddressCreationDto object representing the address to be added.</param>
        public async Task AddAsync(AddressCreationDto AddressDto)
        {
            try
            {
                var AddressModel = _mapper.Map<Addresses>(AddressDto);
                await _repository.AddAsync(AddressModel);
                _logger.Info("Address data added successfully");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add address");
                throw;
            }
        }

        /// <summary>
        /// Retrieves an address by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the address.</param>
        /// <returns>The Address object matching the specified identifier, or null if not found.</returns>
        public async Task<Addresses?> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                _logger.Info("Address retrieved successfully");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get address");
                throw;
            }
        }

        /// <summary>
        /// this gets the address by applicantid
        /// </summary>
        /// <param name="applicantId">the applicant id set to an address</param>
        /// <returns>returns the address model</returns>
        public async Task<Addresses?> GetByApplicantIdAsync(int applicantId)
        {
            try
            {
                return await _repository.GetByApplicantIdAsync(applicantId);
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Failed to get address");
                throw;
            }
        }
    }
}
