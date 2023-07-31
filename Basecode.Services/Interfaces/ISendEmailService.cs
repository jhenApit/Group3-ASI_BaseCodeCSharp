using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ISendEmailService
    {
        void SendHrDetailsEmail(HrEmployee hrEmployee, string password);
        void SendApplicantApplicationRegretEmail(Applicants applicant, string job);
        void SendNewApplicantEmail(Applicants applicant, string position);
        void SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours);
    }
}
