using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Models;
using Basecode.Services.Utils;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantService
    {
        Applicants GetById(int id);
        List<Applicants> RetrieveAll();
        Applicants GetByName(string fname, string mname, string lname);
        int Add(ApplicantCreationDto applicant);
        int Update(ApplicantsUpdationDto applicant);
		Applicants GetByApplicantId(string trackerId);
        public LogContent AddApplicantLogContent(ApplicantCreationDto applicant);
    }
}
