using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ISendEmailService
    {
        Task SendHrDetailsEmail(HrEmployee hrEmployee, string password);
        Task SendApplicantApplicationRegretEmail(Applicants applicant, string job);
        Task SendNewApplicantEmail(Applicants applicant, string position);
        Task SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours);
        Task SendHrJobOfferConfirmationEmail(Applicants applicants, string jobTitle);
        Task SendApplicationStatusEmail(Applicants applicant, string job, string status);
        Task SendHrApplicationApprovalEmail(Applicants applicant, string jobTitle);
        Task SendForgotPasswordLink(IdentityUser user, string url);

        //Start here!
        Task SendSetInterviewScheduleEmail(Interviewers interviewer, Applicants applicant, string interviewType, string jobTitle, string date, string time); // done
        Task SendInterviewReminderEmail(string applicant, Interviewers interviewer, string interviewType, string dateAndTime); // done
        Task SendHrApprovedScheduleEmail(Applicants applicant, string date, string time); //done
        Task SendHrInterviewApprovalEmail(Applicants applicant, string interviewType); // done
        Task SendApplicantInterviewRegretEmail(Applicants applicant, string jobTitle, string interviewType); //done
        Task SendReferenceFormEmail(CharacterReferences characterReference, string applicant, string jobTitle); // done
        Task SendReferenceGratitudeEmail(CharacterReferences characterReference, string applicant); // done
        Task SendApplicantReferenceNotificationEmail(Applicants applicant, string jobTitle); // done
        Task SendHrAnsweredFormNotificationEmail(Applicants applicant); // done
        Task SendHrReferenceApprovalEmail(Applicants applicants); // done
        Task SendHrJobOfferApprovalEmail(Applicants applicant, string jobTitle); // done
        Task SendDtRequirementNotificationEmail(Applicants applicant, string jobTitle); // done
        Task SendDtConfirmationEmail(Applicants applicant); // done

    }
}
