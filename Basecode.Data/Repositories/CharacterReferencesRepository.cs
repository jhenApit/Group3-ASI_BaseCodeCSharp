using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class CharacterReferencesRepository : BaseRepository, ICharacterReferencesRepository
    {
        private readonly BasecodeContext _context;
        public CharacterReferencesRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a new character reference to the database.
        /// </summary>
        /// <param name="characterReferences">The character reference to add.</param>
        public async Task AddAsync(CharacterReferences characterReferences)
        {
            await _context.CharacterReferences.AddAsync(characterReferences);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a character reference by its name.
        /// </summary>
        /// <param name="name">The name of the character reference.</param>
        /// <returns>The character reference matching the specified name, or null if not found.</returns>
        public async Task<CharacterReferences?> GetByNameAsync(string name)
        {
            return await _context.CharacterReferences.FirstOrDefaultAsync(e => e.Name == name)!;
        }

        /// <summary>
        /// Retrieves a character reference by its ID.
        /// </summary>
        /// <param name="id">The ID of the character reference.</param>
        /// <returns>The character reference matching the specified ID, or null if not found.</returns>
        public async Task<CharacterReferences?> GetByIdAsync(int id)
        {
            return await _context.CharacterReferences.FirstOrDefaultAsync(e => e.Id == id)!;
        }

        /// <summary>
        /// Retrieves all character references.
        /// </summary>
        /// <returns>An IQueryable of CharacterReferences representing all character references.</returns>
        public async Task<IQueryable<CharacterReferences>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<CharacterReferences>());
        }

    }
}
