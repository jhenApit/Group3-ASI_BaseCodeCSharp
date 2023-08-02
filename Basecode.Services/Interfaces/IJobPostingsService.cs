using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;
using Basecode.Services.Utils;

namespace Basecode.Services.Interfaces
{
    public interface IJobPostingsService
    {
        Task<List<JobPostings>> RetrieveAllAsync();
        Task<JobPostings?> GetByNameAsync(string name);
        Task AddAsync(JobPostingsCreationDto jobPostings);
        Task<JobPostings?> GetByIdAsync(int id);
        Task UpdateAsync(JobPostingsUpdationDto jobPostings);
        Task SemiDeleteAsync(int id);
        Task PermaDeleteAsync(int id);
    }
}
