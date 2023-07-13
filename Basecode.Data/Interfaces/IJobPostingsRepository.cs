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
        IQueryable<JobPostings> RetrieveAll();
        JobPostings? GetByName(string name);
        void Add(JobPostings jobPostings);
        JobPostings? GetById(int id);
        void Update(JobPostings jobPostings);
        void SemiDelete(JobPostings jobPostings);
        void PermaDelete(int id);
    }
}
