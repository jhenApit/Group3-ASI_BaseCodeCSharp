using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;
using Basecode.Services.Utils;

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
        public LogContent CreateJobPosting(JobPostingsCreationDto jobPostings);
        public LogContent UpdateJobPosting(JobPostingsUpdationDto jobPostings);
    }
}
