using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IJobPostingsService
    {
        List<JobPostings> RetrieveAll();
        JobPostings? GetByName(string name);
        void Add(JobPostingsCreationDto jobPostings);
        JobPostings? GetById(int id);
        void Update(JobPostingsUpdationDto jobPostings);
        void SemiDelete(int id);
        void PermaDelete(int id);
    }
}
