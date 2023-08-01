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
        void SendHrDetailsEmail(HrEmployee hrEmployee, string password);
        void SendApplicantApplicationRegretEmail(Applicants applicant, string job);
        void SendNewApplicantEmail(Applicants applicant, string position);
        void SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours);
        void SendHrJobOfferConfirmationEmail(Applicants applicants, string jobTitle);
        void SendApplicationStatusEmail(Applicants applicant, string job, string status);
        void SendHrApplicationApprovalEmail(Applicants applicant, string jobTitle);
        void SendForgotPasswordLink(IdentityUser user, string url);

        //Start here!
        void SendSetInterviewScheduleEmail(Applicants applicant, string schedule);
        void SendInterviewReminderEmail(string applicant, Interviewers interviewer, string interviewType, string dateAndTime); // done
        void SendHrApprovedScheduleEmail(Applicants applicant);
        void SendHrInterviewApprovalEmail(Applicants applicant, string interviewType); // done
        void SendApplicantInterviewRegretEmail(Applicants applicant, string jobTitle, string interviewType); //done
        void SendReferenceFormEmail(CharacterReferences characterReference, string applicant, string jobTitle); // done
        void SendReferenceGratitudeEmail(CharacterReferences characterReference, string applicant); // done
        void SendApplicantReferenceNotificationEmail(Applicants applicant, string jobTitle); // done
        void SendHrAnsweredFormNotificationEmail(Applicants applicants, CharacterReferences characterReferences);
        void SendHrReferenceApprovalEmail(Applicants applicants); // done
        void SendHrJobOfferApprovalEmail(Applicants applicant, string jobTitle); // done
        void SendDtRequirementNotificationEmail(Applicants applicant, string jobTitle); // done
        void SendDtConfirmationEmail(Applicants applicant); // done

    }
}
