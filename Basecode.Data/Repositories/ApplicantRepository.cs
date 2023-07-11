using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public void Add(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
        }

        public Applicant GetById(int id)
        {
            return _context.Applicants.Find(id);
        }

        public Applicant GetByName(string name)
        {
            return _context.Applicants.FirstOrDefault(e => e.Name == name);
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            return this.GetDbSet<Applicant>();
        }
    }
}
