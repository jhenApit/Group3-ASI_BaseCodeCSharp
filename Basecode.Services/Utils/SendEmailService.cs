using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Services.Utils
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IEmailService _emailService;

        public SendEmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Send an email to new HR with their credentials upon creating an account 
        /// </summary>
        /// <param name="hrEmployee">HR Employee</param>
        /// <param name="password">Account password</param>
        public void SendHrDetailsEmail(HrEmployee hrEmployee, string password)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(hrEmployee.Name, hrEmployee.Email));
            email.Subject = "HR Account Details";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRNewAccount.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", hrEmployee.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{Username}", hrEmployee.User!.UserName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Password}", password);
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            _emailService.SendEmail(email);
        }

        /// <summary>
        /// Send an email to Applicant and Hr when an applicant apply a job 
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="position">Job position</param>
        public void SendNewApplicantEmail(Applicants applicant, string position)
        {
            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Application Form Submitted";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/ApplicationFormSubmitted.html");

            applicantEmailBody = applicantEmailBody.Replace("{Name}", applicant.FirstName);
            applicantEmailBody = applicantEmailBody.Replace("{JobTitle}", position);
            applicantEmailBody = applicantEmailBody.Replace("{ApplicationID}", applicant.ApplicantId);
            applicantEmailBody = applicantEmailBody.Replace("{DateSubmitted}", applicant.ApplicationDate.ToString());
            applicantEmailBody = applicantEmailBody.Replace("{Company Email}", "alliance.jobhiring@gmail.com");

            applicantEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applicantEmailBody
            };

            var hrNotifEmail = new MimeMessage();
            
            hrNotifEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            hrNotifEmail.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            hrNotifEmail.Subject = "New Applicant";

            string hrEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRNewApplicant.html");

            hrEmailBody = hrEmailBody.Replace("{Name}", applicant.FirstName);
            hrEmailBody = hrEmailBody.Replace("{JobTitle}", position);
            hrEmailBody = hrEmailBody.Replace("{DateSubmitted}", applicant.ApplicationDate.ToString());
            hrEmailBody = hrEmailBody.Replace("{Link}", "https://localhost:50140/Hr/JobApplicantsOverview");
            hrEmailBody = hrEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            hrNotifEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            _emailService.SendEmail(applicantEmail);
            _emailService.SendEmail(hrNotifEmail);
        }

        /// <summary>
        /// Send email of regrets to Applicant if application was rejected
        /// </summary>
        /// <param name="applicant"></param>
        /// <param name="job"></param>
        public void SendApplicantApplicationRegretEmail(Applicants applicant, string job)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Apologies and Regrets for Recent Application";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Job}", job);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            _emailService.SendEmail(email);
        }

        /// <summary>
        /// Send a job offer email to the Applicant
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="job">Job name</param>
        /// <param name="workSetup">Work Setup</param>
        /// <param name="hours">Work shift</param>
        public void SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Job Offer";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/JobOffer.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Job}", job);
            emailBodyTemplate = emailBodyTemplate.Replace("{WorkSetup}", workSetup);
            emailBodyTemplate = emailBodyTemplate.Replace("{Hours}", hours);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            _emailService.SendEmail(email);
        }

        /// <summary>
        /// Send a confirmation email to the HR if an Applicant accepted the job offer
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="jobTitle">Job Title</param>
        public void SendHrJobOfferConfirmationEmail(Applicants applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Job Offer Confirmation ";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRJobOfferAccepted.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{CompanyEmail}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            _emailService.SendEmail(email);
        }
    }
}
