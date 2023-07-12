using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Repositories
{
    public class JobPostingRepository : BaseRepository, IJobPostingRepository
    {
        private readonly BasecodeContext _context;
        public JobPostingRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable <JobPosting>RetrieveAll()
        {
            return this.GetDbSet<JobPosting>();
        }
    }
}
