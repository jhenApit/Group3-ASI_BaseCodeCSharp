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
        Applicant? GetById(int id);
        IQueryable<Applicant> RetrieveAll();
        Applicant? GetByName(string name);
        void Add(Applicant applicant);
        Applicant? GetByApplicantId(string trackerId);
    }
}
