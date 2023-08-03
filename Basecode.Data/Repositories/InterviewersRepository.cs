using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    /// <summary>
    /// Represents a repository for managing interviewers data.
    /// </summary>
    public class InterviewersRepository : BaseRepository, IInterviewersRepository
    {
        private readonly BasecodeContext _context;

        /// <summary>
        /// Initializes a new instance of the InterviewersRepository class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to be used for database operations.</param>
        /// <param name="context">The database context.</param>
        public InterviewersRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all interviewers from the database asynchronously.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation, containing an IQueryable collection of Interviewers.</returns>
        public async Task<IQueryable<Interviewers>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Interviewers>());
        }

        /// <summary>
        /// Adds a new Interviewers entity to the database asynchronously.
        /// </summary>
        /// <param name="interviewer">The Interviewers entity to be added.</param>
        public async Task AddAsync(Interviewers interviewer)
        {
            await _context.Interviewers.AddAsync(interviewer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an Interviewers entity by its ID from the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the Interviewers entity.</param>
        /// <returns>An asynchronous task that represents the operation, containing the Interviewers entity if found, otherwise null.</returns>
        public async Task<Interviewers?> GetByIdAsync(int id)
        {
            return await _context.Interviewers.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing Interviewers entity in the database asynchronously.
        /// </summary>
        /// <param name="interviewer">The updated Interviewers entity.</param>
        public async Task UpdateAsync(Interviewers interviewer)
        {
            var existingInterviewers = await _context.Interviewers.FindAsync(interviewer.Id);
            if (existingInterviewers != null)
            {
                // Update the properties of the existing entity
                existingInterviewers.Name = interviewer.Name;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes an Interviewers entity from the database by its ID asynchronously.
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
        /// Retrieves an Interviewers entity by its name from the database asynchronously.
        /// </summary>
        /// <param name="name">The name of the Interviewers entity.</param>
        /// <returns>An asynchronous task that represents the operation, containing the Interviewers entity if found, otherwise null.</returns>
        public async Task<Interviewers?> GetByNameAsync(string name)
        {
            return await _context.Interviewers.FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}