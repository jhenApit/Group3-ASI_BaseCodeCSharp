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
    public class CharacterReferencesService : ICharacterReferencesService
    {
        private readonly ICharacterReferencesRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CharacterReferencesService(ICharacterReferencesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all character references from the repository.
        /// </summary>
        /// <returns>A list of character references.</returns>
        public async Task<List<CharacterReferences>> RetrieveAllAsync()
        {
            try
            {
                var characterRef = await _repository.RetrieveAllAsync();
                return characterRef.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("CharacterReferencesService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new character reference to the repository.
        /// </summary>
        /// <param name="characterReferencesDto">The data transfer object containing the information of the character reference to be added.</param>
        public async Task AddAsync(CharacterReferencesCreationDto characterReferencesDto)
        {
            try
            {
                var characterReferencesModel = _mapper.Map<CharacterReferences>(characterReferencesDto);
                await _repository.AddAsync(characterReferencesModel);
            }
            catch (Exception ex)
            {
                _logger.Error("CharacterReferencesService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a character reference by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the character reference.</param>
        /// <returns>The character reference with the specified ID.</returns>
        public async Task<CharacterReferences?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error("CharacterReferencesService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a character reference by its name from the repository.
        /// </summary>
        /// <param name="name">The name of the character reference.</param>
        /// <returns>The character reference with the specified name.</returns>
        public async Task<CharacterReferences?> GetByNameAsync(string name)
        {
            try
            {
                return await _repository.GetByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.Error("CharacterReferencesService > GetByNameAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This gets the list of character references of an applicant
        /// </summary>
        /// <param name="applicantId">the applicant id set to the character references</param>
        /// <returns>the iqueryable of result of getting the character references
        /// by it's applicantId</returns>
        public async Task<List<CharacterReferences>> GetByApplicantIdAsync(int applicantId)
        {
            try
            {
                var characRef = await _repository.GetByApplicantIdAsync(applicantId);
                return characRef.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("CharacterReferencesService > GetByApplicantIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

    }
}