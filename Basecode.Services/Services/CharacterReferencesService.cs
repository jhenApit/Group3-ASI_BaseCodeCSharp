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

namespace Basecode.Services.Services
{
    public class CharacterReferencesService : ICharacterReferencesService
    {
        private readonly ICharacterReferencesRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

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
            var characterRef = await _repository.RetrieveAllAsync();
            return characterRef.ToList();
        }

        /// <summary>
        /// Adds a new character reference to the repository.
        /// </summary>
        /// <param name="characterReferencesDto">The data transfer object containing the information of the character reference to be added.</param>
        public async Task AddAsync(CharacterReferencesCreationDto characterReferencesDto)
        {
            var characterReferencesModel = _mapper.Map<CharacterReferences>(characterReferencesDto);
            await _repository.AddAsync(characterReferencesModel);
        }

        /// <summary>
        /// Retrieves a character reference by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the character reference.</param>
        /// <returns>The character reference with the specified ID.</returns>
        public async Task<CharacterReferences?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieves a character reference by its name from the repository.
        /// </summary>
        /// <param name="name">The name of the character reference.</param>
        /// <returns>The character reference with the specified name.</returns>
        public async Task<CharacterReferences?> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        /// <summary>
        /// Retrieves the references of an applicant using its Id.
        /// </summary>
        /// <param applicantId="applicantId">The id of the applicant the reference belongs to.</param>
        /// <returns>The list of reference of the applicant.</returns>
        public List<CharacterReferences> GetByApplicantId(int applicantId)
        {
            return _repository.GetByApplicantId(applicantId).ToList();
        }
    }
}