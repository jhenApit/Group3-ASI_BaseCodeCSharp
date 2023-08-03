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
        Task SendHrInterviewApprovalEmail(Interviews interview);
        Task SendApplicantInterviewRegretEmail(Interviews interview);

        //Background Check
        Task SendReferenceFormEmail(CharacterReferences characterReference, Applicants applicant);
        Task SendReferenceGratitudeEmail(CharacterReferences characterReference);
        Task SendApplicantReferenceNotificationEmail(Applicants applicant);
        Task SendHrAnsweredFormNotificationEmail(Applicants applicant);
        Task SendHrReferenceApprovalEmail(Applicants applicants);

        //Job Offer
        Task SendApplicantJobOfferEmail(Applicants applicant);
        Task SendHrJobOfferApprovalEmail(Applicants applicant);
        Task SendDtRequirementNotificationEmail(Applicants applicant);
        Task SendDtConfirmationEmail(Applicants applicant);
        
        
        
        
        
        
        
        
        
        
        
        
        

    }
}
