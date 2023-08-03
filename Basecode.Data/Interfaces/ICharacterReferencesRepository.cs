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
        Task<IQueryable<CharacterReferences>> RetrieveAllAsync();
        Task<CharacterReferences?> GetByNameAsync(string name);
        Task AddAsync(CharacterReferences characterReferences);
        Task<CharacterReferences?> GetByIdAsync(int id);
        IQueryable<CharacterReferences> GetByApplicantId(int applicantId);
    }
}
