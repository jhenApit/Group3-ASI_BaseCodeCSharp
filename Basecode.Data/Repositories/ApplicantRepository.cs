using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        private readonly BasecodeContext _context;
        public ApplicantRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public Applicant GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
