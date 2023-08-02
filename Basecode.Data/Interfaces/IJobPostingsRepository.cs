using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IJobPostingsRepository
    {
        Task<IQueryable<JobPostings>> RetrieveAllAsync();
        Task<JobPostings?> GetByNameAsync(string name);
        Task AddAsync(JobPostings jobPostings);
        Task<JobPostings?> GetByIdAsync(int id);
        Task UpdateAsync(JobPostings jobPostings);
        Task SemiDeleteAsync(JobPostings jobPostings);
        Task PermaDeleteAsync(int id);
    }
}
