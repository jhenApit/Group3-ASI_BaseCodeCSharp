using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Services.Interfaces
{
    public interface IJobPostingsService
    {
        Task<List<JobPostings>> RetrieveAllAsync();
        Task<JobPostings?> GetByNameAsync(string name);
        Task AddAsync(JobPostingsCreationDto jobPostings, IdentityUser loggedUser);
        Task<JobPostings?> GetByIdAsync(int id);
        Task UpdateAsync(JobPostingsUpdationDto jobPostings, IdentityUser loggedUser);
        Task SemiDeleteAsync(int id);
        Task PermaDeleteAsync(int id);
    }
}
