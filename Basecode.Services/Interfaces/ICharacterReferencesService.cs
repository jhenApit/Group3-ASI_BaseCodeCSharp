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
        Task<List<CharacterReferences>> RetrieveAllAsync();
        Task<CharacterReferences?> GetByNameAsync(string name);
        Task AddAsync(CharacterReferencesCreationDto characterReferences);
        Task<CharacterReferences?> GetByIdAsync(int id);
    }
}
