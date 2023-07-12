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

        public IQueryable<JobPostings> RetrieveAll()
        {
            return this.GetDbSet<JobPostings>().Where(e => !e.IsDeleted);
        }

        public void Add(JobPostings JobPostings)
        {
            _context.JobPostings.Add(JobPostings);
            _context.SaveChanges();
        }

        public JobPostings? GetById(int id)
        {
            return _context.JobPostings.Find(id);
        }

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
                existingJobPostings.IsActive = jobPostings.IsActive;
                existingJobPostings.Type = jobPostings.Type;
                existingJobPostings.UpdatedTime = jobPostings.UpdatedTime;
                existingJobPostings.UpdatedById = jobPostings.UpdatedById;
                existingJobPostings.IsDeleted = jobPostings.IsDeleted;

                // Save the changes
                _context.SaveChanges();
            }

        }

        public void SemiDelete(JobPostings JobPostings)
        {
            _context.JobPostings.Update(JobPostings);
            _context.SaveChanges();
        }

        public void PermaDelete(int id)
        {
            var data = _context.JobPostings.Find(id);
            if (data != null)
            {
                _context.JobPostings.Remove(data);
                _context.SaveChanges();
            }
        }

        public JobPostings? GetByName(string name)
        {
            return _context.JobPostings.FirstOrDefault(e => e.Name == name);
        }

    }
}
