using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Data.Repositories
{
    public class JobPostingsRepository : BaseRepository, IJobPostingsRepository
    {
        private readonly BasecodeContext _context;
        public JobPostingsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all job postings that are not marked as deleted.
        /// </summary>
        /// <returns>An IQueryable of JobPostings.</returns>
        public IQueryable<JobPostings> RetrieveAll()
        {
            return this.GetDbSet<JobPostings>().Where(e => !e.IsDeleted);
        }

        /// <summary>
        /// Adds a new job posting to the context and saves changes.
        /// </summary>
        /// <param name="jobPostings">The JobPostings object to be added.</param>
        public void Add(JobPostings jobPostings)
        {
            _context.JobPostings.Add(jobPostings);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves a job posting by its ID.
        /// </summary>
        /// <param name="id">The ID of the job posting.</param>
        /// <returns>The JobPostings object with the specified ID, or null if not found.</returns>
        public JobPostings? GetById(int id)
        {
            return _context.JobPostings.Find(id);
        }

        /// <summary>
        /// Updates an existing job posting with the provided values and saves changes.
        /// </summary>
        /// <param name="jobPostings">The updated JobPostings object.</param>
        public void Update(JobPostings jobPostings)
        {
            var existingJobPostings = _context.JobPostings.Find(jobPostings.Id);
            if (existingJobPostings != null)
            {
                // Update the properties of the existing entity
                existingJobPostings.Name = jobPostings.Name;
                existingJobPostings.Description = jobPostings.Description;
                existingJobPostings.Responsibilities = jobPostings.Responsibilities;
                existingJobPostings.Qualifications = jobPostings.Qualifications;
                existingJobPostings.WorkSetup = jobPostings.WorkSetup;
                existingJobPostings.Hours = jobPostings.Hours;
                existingJobPostings.JobStatus = jobPostings.JobStatus;
                existingJobPostings.EmploymentType = jobPostings.EmploymentType;
                existingJobPostings.UpdatedTime = jobPostings.UpdatedTime;
                existingJobPostings.UpdatedBy = jobPostings.UpdatedBy;
                existingJobPostings.IsDeleted = jobPostings.IsDeleted;

                // Save the changes
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Marks a job posting as semi-deleted by updating its properties and saving changes.
        /// </summary>
        /// <param name="jobPostings">The JobPostings object to be semi-deleted.</param>
        public void SemiDelete(JobPostings jobPostings)
        {
            _context.JobPostings.Update(jobPostings);
            _context.SaveChanges();
        }

        /// <summary>
        /// Permanently deletes a job posting by its ID from the context and saves changes.
        /// </summary>
        /// <param name="id">The ID of the job posting to be permanently deleted.</param>
        public void PermaDelete(int id)
        {
            var data = _context.JobPostings.Find(id);
            if (data != null)
            {
                _context.JobPostings.Remove(data);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves a job posting by its name.
        /// </summary>
        /// <param name="name">The name of the job posting.</param>
        /// <returns>The JobPostings object with the specified name, or null if not found.</returns>
        public JobPostings? GetByName(string name)
        {
            return _context.JobPostings.FirstOrDefault(e => e.Name == name);
        }

    }
}
