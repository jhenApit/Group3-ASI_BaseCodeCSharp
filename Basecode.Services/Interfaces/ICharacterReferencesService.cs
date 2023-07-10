using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.CharacterReferences;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface ICharacterReferencesService
    {
        List<CharacterReferences> RetrieveAll();
        CharacterReferences GetByCity(string city);
        void Add(CharacterReferencesCreationDto characterReferences);
        CharacterReferences GetById(int id);
        void Update(CharacterReferencesUpdationDto characterReferences);
        void Delete(int id);
    }
}
