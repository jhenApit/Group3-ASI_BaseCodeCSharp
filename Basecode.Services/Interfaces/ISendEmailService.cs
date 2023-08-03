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
        Task SendForgotPasswordLink(IdentityUser user, string url);
        Task SendHrDetailsEmail(HrEmployee hrEmployee, string password);

        //Application and Screening
        Task SendApplicationStatusEmail(Applicants applicant, string status);
        Task SendNewApplicantEmail(Applicants applicant);
        Task SendHrApplicationApprovalEmail(Applicants applicant);
        Task SendApplicantApplicationRegretEmail(Applicants applicant);

        //Interviews
        Task SendSetInterviewScheduleEmail(Interviews interview, Applicants applicant);
        Task SendInterviewReminderEmail(Interviews interview, Applicants applicant);
        Task SendApprovedScheduleEmail(Applicants applicant, Interviews interview);
        Task SendHrInterviewApprovalEmail(Applicants applicant, string interviewType);
        Task SendApplicantInterviewRegretEmail(Applicants applicant, string jobTitle, string interviewType);

        //Background Check
        Task SendReferenceFormEmail(CharacterReferences characterReference, string applicant, string jobTitle);
        Task SendReferenceGratitudeEmail(CharacterReferences characterReference, string applicant);
        Task SendApplicantReferenceNotificationEmail(Applicants applicant, string jobTitle);
        Task SendHrAnsweredFormNotificationEmail(Applicants applicant);
        Task SendHrReferenceApprovalEmail(Applicants applicants);

        //Job Offer
        Task SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours);
        Task SendHrJobOfferApprovalEmail(Applicants applicant, string jobTitle);
        Task SendDtRequirementNotificationEmail(Applicants applicant, string jobTitle);
        Task SendDtConfirmationEmail(Applicants applicant);
        
        
        
        
        
        
        
        
        
        
        
        
        

    }
}
