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
    public class InterviewersRepository : BaseRepository, IInterviewersRepository
    {
        private readonly BasecodeContext _context;
        public InterviewersRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all interviewers from the database.
        /// </summary>
        /// <returns>An IQueryable collection of Interviewers.</returns>
        public async Task<IQueryable<Interviewers>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Interviewers>());
        }

        /// <summary>
        /// Adds a new Interviewers entity to the database.
        /// </summary>
        /// <param name="Interviewers">The Interviewers entity to be added.</param>
        public async Task AddAsync(Interviewers Interviewers)
        {
            await _context.Interviewers.AddAsync(Interviewers);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an Interviewers entity by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the Interviewers entity.</param>
        /// <returns>The Interviewers entity if found, otherwise null.</returns>
        public async Task<Interviewers?> GetByIdAsync(int id)
        {
            return await _context.Interviewers.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing Interviewers entity in the database.
        /// </summary>
        /// <param name="Interviewers">The updated Interviewers entity.</param>
        public async Task UpdateAsync(Interviewers Interviewers)
        {
            var existingInterviewers = await _context.Interviewers.FindAsync(Interviewers.Id);
            if (existingInterviewers != null)
            {
                // Update the properties of the existing entity
                existingInterviewers.Name = Interviewers.Name;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes an Interviewers entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Interviewers entity to be deleted.</param>
        public async Task DeleteAsync(int id)
        {
            var data = await _context.Interviewers.FindAsync(id);
            if (data != null)
            {
                _context.Interviewers.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves an Interviewers entity by its name from the database.
        /// </summary>
        /// <param name="name">The name of the Interviewers entity.</param>
        /// <returns>The Interviewers entity if found, otherwise null.</returns>
        public async Task<Interviewers?> GetByNameAsync(string name)
        {
            return await _context.Interviewers.FirstOrDefaultAsync(e => e.Name == name);
        }

    }
}
