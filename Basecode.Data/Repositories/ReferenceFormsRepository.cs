using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class ReferenceFormsRepository : BaseRepository, IReferenceFormsRepository
    {
        private readonly BasecodeContext _context;
        public ReferenceFormsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a reference form to the database.
        /// </summary>
        /// <param name="referenceForms">The reference form to be added.</param>
        public async Task AddAsync(ReferenceForms referenceForms)
        {
            await _context.ReferenceForms.AddAsync(referenceForms);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a reference form by character reference ID.
        /// </summary>
        /// <param name="characterReferenceId">The ID of the character reference.</param>
        /// <returns>The reference form associated with the character reference ID, or null if not found.</returns>
        public async Task<ReferenceForms?> GetByCharacterReferenceIdAsync(int characterReferenceId)
        {
            return await _context.ReferenceForms.FirstOrDefaultAsync(e => e.CharacterReferenceId == characterReferenceId);
        }

        /// <summary>
        /// Retrieves a reference form by ID.
        /// </summary>
        /// <param name="id">The ID of the reference form.</param>
        /// <returns>The reference form with the specified ID, or null if not found.</returns>
        public async Task<ReferenceForms?> GetByIdAsync(int id)
        {
            return await _context.ReferenceForms.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Retrieves all reference forms from the database.
        /// </summary>
        /// <returns>An IQueryable containing all reference forms.</returns>
        public async Task<IQueryable<ReferenceForms>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<ReferenceForms>());
        }

    }

}
