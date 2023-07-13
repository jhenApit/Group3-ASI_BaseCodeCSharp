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

        public CharacterReferences GetByName(string name)
        {
            return _repository.GetByName(name);
        }
    }
}
