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
using NLog;

namespace Basecode.Services.Services
{
    public class ReferenceFormsService : IReferenceFormsService
    {
        private readonly IReferenceFormsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ReferenceFormsService(IReferenceFormsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all reference forms.
        /// </summary>
        /// <returns>A list of ReferenceForms.</returns>
        public async Task<List<ReferenceForms>> RetrieveAllAsync()
        {
            try
            {
                var referenceForm = await _repository.RetrieveAllAsync();
                return referenceForm.ToList();
            }
            catch (System.Exception ex)
            {
                _logger.Error("ReferenceFormsService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new reference form.
        /// </summary>
        /// <param name="referenceFormsDto">The ReferenceFormsCreationDto object containing the data for the new reference form.</param>
        public async Task AddAsync(ReferenceFormsCreationDto referenceFormsDto)
        {
            try
            {
                var referenceFormsModel = _mapper.Map<ReferenceForms>(referenceFormsDto);
                referenceFormsModel.AnsweredDate = DateTime.Now.Date;
                await _repository.AddAsync(referenceFormsModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("ReferenceFormsService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a reference form by its ID.
        /// </summary>
        /// <param name="id">The ID of the reference form to retrieve.</param>
        /// <returns>The ReferenceForms object with the specified ID, or null if not found.</returns>
        public async Task<ReferenceForms?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("ReferenceFormsService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a reference form by a character reference ID.
        /// </summary>
        /// <param name="characterReferenceId">The ID of the character reference associated with the reference form.</param>
        /// <returns>The ReferenceForms object with the specified character reference ID, or null if not found.</returns>
        public async Task<ReferenceForms?> GetByCharacterReferenceIdAsync(int characterReferenceId)
        {
            try
            {
                return await _repository.GetByCharacterReferenceIdAsync(characterReferenceId);
            }
            catch (System.Exception ex)
            {
                _logger.Error("ReferenceFormsService > GetByCharacterReferenceIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

    }

}
