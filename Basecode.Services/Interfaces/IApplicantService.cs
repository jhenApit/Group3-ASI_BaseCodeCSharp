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
        Applicants GetById(int id);
        List<Applicants> RetrieveAll();
        Applicants GetByName(string name);
        void Add(ApplicantCreationDto applicant);
        Applicants GetByApplicantId(string trackerId);
    }
}
