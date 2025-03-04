﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;
using static Basecode.Data.Enums.Enums;

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
        public async Task AddAsync(Applicants applicant)
        {
            await _context.Applicants.AddAsync(applicant);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ApplicantID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>
        public async Task<Applicants?> GetByApplicantIdAsync(string applicantId)
        {
            return await _context.Applicants.SingleOrDefaultAsync(e => e.ApplicantId == applicantId);
        }

        /// <summary>
        /// Retrieves applicants from the database based on their email address.
        /// </summary>
        /// <param name="email">The email address of the applicant(s) to retrieve.</param>
        /// <returns>A queryable collection of applicants matching the provided email address.</returns>

        public async Task<IQueryable<Applicants>> GetByEmailAsync(string email)
        {
            return await Task.FromResult(this.GetDbSet<Applicants>().Where(e => e.Email == email));
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ApplicantID.
        /// </summary>
        /// <param name="id">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>

        public async Task<Applicants?> GetByIdAsync(int id)
        {
            return await _context.Applicants.FindAsync(id);
        }

        /// <summary>
        /// Retrieves an applicant from the database by their name.
        /// </summary>
        /// <param name="name">The name of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified name, or null if not found.</returns>
        public async Task<Applicants?> GetByNameAsync(string fname, string mname, string lname)
        {
            return await _context.Applicants.FirstOrDefaultAsync(e => e.FirstName == fname && e.MiddleName == mname && e.LastName == lname);
        }

        /// <summary>
        /// Retrieves all applicants from the database.
        /// </summary>
        /// <returns>An IQueryable collection of all applicants.</returns> 
        public async Task<IQueryable<Applicants>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Applicants>().Include(job => job.Job));
        }
        /// <summary>
        /// This update the applicant form the database
        /// </summary>
        /// <param name="applicant"> the applicant model to update</param>
		public async Task<bool> UpdateAsync(Applicants applicant)
		{
            var existingApplicant = _context.Applicants.Find(applicant.Id);
            if(existingApplicant != null)
            {
                existingApplicant.ApplicationStatus = applicant.ApplicationStatus;
				_context.SaveChanges();
                return true;
			}
            return false;
		}

	}
}
