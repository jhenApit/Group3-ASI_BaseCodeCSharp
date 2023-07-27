using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IApplicantRepository
    {
        Applicants? GetById(int id);
        IQueryable<Applicants> RetrieveAll();
        Applicants? GetByName(string fname, string mname, string lname);
        void Add(Applicants applicant);
        Applicants? GetByApplicantId(string applicantId);
        public void Update(Applicants applicant);

	}
}
