using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IJobPostingRepository _repository;
        public JobPostingService(IJobPostingRepository repository)
        {
            _repository = repository;
        }

        public List<JobPosting> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }
    }
}
