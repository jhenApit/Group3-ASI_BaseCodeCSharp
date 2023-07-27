using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
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
        public void Add(Applicants applicant)
        {
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ApplicantID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>
        public Applicants? GetByApplicantId(string applicantId)
        {
            return _context.Applicants.SingleOrDefault(e => e.ApplicantId == applicantId);
        }
        public Applicants? GetById(int id)
        {
            return _context.Applicants.Find(id);
        }

        /// <summary>
        /// Retrieves an applicant from the database by their name.
        /// </summary>
        /// <param name="name">The name of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified name, or null if not found.</returns>
        public Applicants? GetByName(string fname, string mname, string lname)
        {
            return _context.Applicants.FirstOrDefault(e => e.FirstName == fname && e.MiddleName == mname && e.LastName == lname);
        }

        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable collection of all applicants.</returns>
        public IQueryable<Applicants> RetrieveAll()
        {
            return this.GetDbSet<Applicants>();
        }
        /// <summary>
        /// This update the applicant form the database
        /// </summary>
        /// <param name="applicant"> the applicant model to update</param>
		public void Update(Applicants applicant)
		{
            var existingApplicant = _context.Applicants.Find(applicant.Id);
            if(existingApplicant != null)
            {
                existingApplicant.ApplicationStatus = applicant.ApplicationStatus;
				_context.SaveChanges();
			}
		}

	}
}
