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
    public class CharacterReferencesService : ErrorHandling, ICharacterReferencesService
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
        public List<CharacterReferences> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new character reference to the repository.
        /// </summary>
        /// <param name="characterReferencesDto">The data transfer object containing the information of the character reference to be added.</param>
        public void Add(CharacterReferencesCreationDto characterReferencesDto)
        {
            var characterReferencesModel = _mapper.Map<CharacterReferences>(characterReferencesDto);
            _repository.Add(characterReferencesModel);
        }

        /// <summary>
        /// Retrieves a character reference by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the character reference.</param>
        /// <returns>The character reference with the specified ID.</returns>
        public CharacterReferences GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Retrieves a character reference by its name from the repository.
        /// </summary>
        /// <param name="name">The name of the character reference.</param>
        /// <returns>The character reference with the specified name.</returns>
        public CharacterReferences GetByName(string name)
        {
            return _repository.GetByName(name);
        }

    }
}
