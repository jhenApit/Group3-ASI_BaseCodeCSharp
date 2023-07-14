using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantService
    {
        Applicant GetById(int id);
        List<Applicant> RetrieveAll();
        Applicant GetByName(string name);
        void Add(ApplicantCreationDto applicant);
        Applicant GetByApplicantId(string trackerId);
    }
}
