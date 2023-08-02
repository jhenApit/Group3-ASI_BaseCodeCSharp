using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IQueryable<JobPostings>> RetrieveAllAsync()
        {
            return this.GetDbSet<JobPostings>().Where(e => !e.IsDeleted);
        }

        /// <summary>
        /// Adds a new job posting to the context and saves changes.
        /// </summary>
        /// <param name="jobPostings">The JobPostings object to be added.</param>
        public async Task AddAsync(JobPostings jobPostings)
        {
            await _context.JobPostings.AddAsync(jobPostings);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a job posting by its ID.
        /// </summary>
        /// <param name="id">The ID of the job posting.</param>
        /// <returns>The JobPostings object with the specified ID, or null if not found.</returns>
        public async Task<JobPostings?> GetByIdAsync(int id)
        {
            return await _context.JobPostings.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing job posting with the provided values and saves changes.
        /// </summary>
        /// <param name="jobPostings">The updated JobPostings object.</param>
        public async Task UpdateAsync(JobPostings jobPostings)
        {
            var existingJobPostings = await _context.JobPostings.FindAsync(jobPostings.Id);
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
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Marks a job posting as semi-deleted by updating its properties and saving changes.
        /// </summary>
        /// <param name="jobPostings">The JobPostings object to be semi-deleted.</param>
        public async Task SemiDeleteAsync(JobPostings jobPostings)
        {
            _context.JobPostings.Update(jobPostings);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Permanently deletes a job posting by its ID from the context and saves changes.
        /// </summary>
        /// <param name="id">The ID of the job posting to be permanently deleted.</param>
        public async Task PermaDeleteAsync(int id)
        {
            var data = await _context.JobPostings.FindAsync(id);
            if (data != null)
            {
                _context.JobPostings.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a job posting by its name.
        /// </summary>
        /// <param name="name">The name of the job posting.</param>
        /// <returns>The JobPostings object with the specified name, or null if not found.</returns>
        public async Task<JobPostings?> GetByNameAsync(string name)
        {
            return await _context.JobPostings.FirstOrDefaultAsync(e => e.Name == name);
        }

    }
}
