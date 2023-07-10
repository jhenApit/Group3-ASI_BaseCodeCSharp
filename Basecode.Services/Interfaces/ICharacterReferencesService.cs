using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface ICharacterReferencesService
    {
        List<CharacterReferences> RetrieveAll();
        CharacterReferences GetByName(string name);
        void Add(CharacterReferencesCreationDto characterReferences);
        CharacterReferences GetById(int id);
    }
}
