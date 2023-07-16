using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

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
        public void Add(ReferenceForms referenceForms)
        {
            _context.ReferenceForms.Add(referenceForms);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves a reference form by character reference ID.
        /// </summary>
        /// <param name="characterReferenceId">The ID of the character reference.</param>
        /// <returns>The reference form associated with the character reference ID, or null if not found.</returns>
        public ReferenceForms? GetByCharacterReferenceId(int characterReferenceId)
        {
            return _context.ReferenceForms.FirstOrDefault(e => e.CharacterReferenceId == characterReferenceId);
        }

        /// <summary>
        /// Retrieves a reference form by ID.
        /// </summary>
        /// <param name="id">The ID of the reference form.</param>
        /// <returns>The reference form with the specified ID, or null if not found.</returns>
        public ReferenceForms? GetById(int id)
        {
            return _context.ReferenceForms.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Retrieves all reference forms from the database.
        /// </summary>
        /// <returns>An IQueryable containing all reference forms.</returns>
        public IQueryable<ReferenceForms> RetrieveAll()
        {
            return this.GetDbSet<ReferenceForms>();
        }

    }

}
