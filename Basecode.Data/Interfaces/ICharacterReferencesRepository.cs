using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface ICharacterReferencesRepository
    {
        IQueryable<CharacterReferences> RetrieveAll();
        CharacterReferences GetByName(string name);
        void Add(CharacterReferences characterReferences);
        CharacterReferences GetById(int id);
    }
}
