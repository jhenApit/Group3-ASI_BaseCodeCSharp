using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.CharacterReferences;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;

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

        public List<CharacterReferences> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(CharacterReferencesCreationDto characterReferencesDto)
        {
            var characterReferencesModel = _mapper.Map<CharacterReferences>(characterReferencesDto);
            _repository.Add(characterReferencesModel);
        }

        public CharacterReferences GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(CharacterReferencesUpdationDto characterReferences)
        {
            var CharacterReferencesModel = _mapper.Map<CharacterReferences>(characterReferences);

            // Update only the properties that should be modified
            CharacterReferencesModel.Name = characterReferences.Name;
            CharacterReferencesModel.Relationship = characterReferences.Relationship;
            CharacterReferencesModel.Email = characterReferences.Email;
            CharacterReferencesModel.MobileNumber = characterReferences.MobileNumber;

            _repository.Update(CharacterReferencesModel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CharacterReferences GetByName(string name)
        {
            return _repository.GetByName(name);
        }
    }
}
