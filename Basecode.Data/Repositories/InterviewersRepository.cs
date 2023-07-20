using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

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
        public IQueryable<Interviewers> RetrieveAll()
        {
            return this.GetDbSet<Interviewers>();
        }

        /// <summary>
        /// Adds a new Interviewers entity to the database.
        /// </summary>
        /// <param name="Interviewers">The Interviewers entity to be added.</param>
        public void Add(Interviewers Interviewers)
        {
            _context.Interviewers.Add(Interviewers);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves an Interviewers entity by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the Interviewers entity.</param>
        /// <returns>The Interviewers entity if found, otherwise null.</returns>
        public Interviewers? GetById(int id)
        {
            return _context.Interviewers.Find(id);
        }

        /// <summary>
        /// Updates an existing Interviewers entity in the database.
        /// </summary>
        /// <param name="Interviewers">The updated Interviewers entity.</param>
        public void Update(Interviewers Interviewers)
        {
            var existingInterviewers = _context.Interviewers.Find(Interviewers.Id);
            if (existingInterviewers != null)
            {
                // Update the properties of the existing entity
                existingInterviewers.Name = Interviewers.Name;

                // Save the changes
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an Interviewers entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Interviewers entity to be deleted.</param>
        public void Delete(int id)
        {
            var data = _context.Interviewers.Find(id);
            if (data != null)
            {
                _context.Interviewers.Remove(data);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves an Interviewers entity by its name from the database.
        /// </summary>
        /// <param name="name">The name of the Interviewers entity.</param>
        /// <returns>The Interviewers entity if found, otherwise null.</returns>
        public Interviewers? GetByName(string name)
        {
            return _context.Interviewers.FirstOrDefault(e => e.Name == name);
        }

    }
}
