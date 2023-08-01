using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Models;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Interfaces
{
    public interface IApplicantRepository
    {
        Applicants? GetById(int id);
        IQueryable<Applicants> RetrieveAll();
        Applicants? GetByName(string fname, string mname, string lname);
        IQueryable<Applicants> GetByEmail(string email);
        void Add(Applicants applicant);
        Applicants? GetByApplicantId(string applicantId);
        public bool Update(Applicants applicant);


    }
}
