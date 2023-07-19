using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        private readonly BasecodeContext _context;
        public ApplicantRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new applicant to the database.
        /// </summary>
        /// <param name="applicant">The applicant to be added.</param>
        public void Add(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>
        public Applicant? GetByApplicantId(string applicantId)
        {
            return _context.Applicants.FirstOrDefault(e => e.ApplicantId == applicantId);
        }
        public Applicant? GetById(int id)
        {
            return _context.Applicants.Find(id)!;
        }

        /// <summary>
        /// Retrieves an applicant from the database by their name.
        /// </summary>
        /// <param name="name">The name of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified name, or null if not found.</returns>
        public Applicant? GetByName(string name)
        {
            return _context.Applicants.FirstOrDefault(e => e.Name == name)!;
        }

        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable collection of all applicants.</returns>
        public IQueryable<Applicant> RetrieveAll()
        {
            return this.GetDbSet<Applicant>();
        }

    }
}
