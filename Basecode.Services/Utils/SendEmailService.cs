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
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

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
        public async Task SendHrDetailsEmail(HrEmployee hrEmployee, string password)
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

            await _emailService.SendEmail(email);
        }

        /// <summary>
        /// Send an email to Applicant and Hr when an applicant apply a job 
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="position">Job position</param>
        public async Task SendNewApplicantEmail(Applicants applicant, string position)
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

            await _emailService.SendEmail(applicantEmail);
            await _emailService.SendEmail(hrNotifEmail);
        }

        /// <summary>
        /// Send email of regrets to Applicant if application was rejected
        /// </summary>
        /// <param name="applicant"></param>
        /// <param name="job"></param>
        public async Task SendApplicantApplicationRegretEmail(Applicants applicant, string job)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Apologies and Regrets for Recent Application";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantApplicationRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Job}", job);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        /// <summary>
        /// Send a job offer email to the Applicant
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="job">Job name</param>
        /// <param name="workSetup">Work Setup</param>
        /// <param name="hours">Work shift</param>
        public async Task SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours)
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

            await _emailService.SendEmail(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicant"></param>
        /// <param name="job">Job title</param>
        /// <param name="status">Application status</param>
        public async Task SendApplicationStatusEmail(Applicants applicant, string job, string status)
        {
            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Application Status Update";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/ApplicantApplicationStatusNotif.html");

            applicantEmailBody = applicantEmailBody.Replace("{Name}", applicant.FirstName);
            applicantEmailBody = applicantEmailBody.Replace("{JobName}", job);
            applicantEmailBody = applicantEmailBody.Replace("{Status}", status);
            applicantEmailBody = applicantEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication");
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

            hrEmailBody = hrEmailBody.Replace("{Name}", applicant.Name);
            hrEmailBody = hrEmailBody.Replace("{ApplicantID}", applicant.ApplicantId);
            hrEmailBody = hrEmailBody.Replace("{JobTitle}", job);
            hrEmailBody = hrEmailBody.Replace("{Status}", status);
            hrEmailBody = hrEmailBody.Replace("{Date}", "ModifiedDate");
            hrEmailBody = hrEmailBody.Replace("{Company Email}", "alliance.jobhiring@gmail.com");

            hrNotifEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            await _emailService.SendEmail(applicantEmail);
            await _emailService.SendEmail(hrNotifEmail);
        }

        /// <summary>
        /// Send notification email to HR to review Applicant's application
        /// </summary>
        /// <param name="applicant">Applicant</param>
        /// <param name="jobTitle">Job title</param>
        public async Task SendHrApplicationApprovalEmail(Applicants applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Shortlisted Applicants";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRScreeningApplicant.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantID}", applicant.ApplicantId);
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "alliance.jobhiring@gmail.com"); // link to the applicant's profile
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendForgotPasswordLink(IdentityUser user, string url)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(user.UserName, user.Email));
            email.Subject = "Account Password Reset";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRForgotPassword.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", user.UserName); 
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");
            emailBodyTemplate = emailBodyTemplate.Replace("{ResetPasswordUrl}", url);

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendSetInterviewScheduleEmail(Interviewers interviewer, Applicants applicant, string interviewType, string jobTitle, string date, string time)
        {
            var interviewerEmail = new MimeMessage();

            interviewerEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            interviewerEmail.To.Add(new MailboxAddress(interviewer.Name, interviewer.Email));
            interviewerEmail.Subject = "Interview Notice";

            string interviewerEmailBody = File.ReadAllText("wwwroot/emailTemplates/InterviewerDateNotice.html");

            interviewerEmailBody = interviewerEmailBody.Replace("{Interviwername}", interviewer.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewType}", interviewType);
            interviewerEmailBody = interviewerEmailBody.Replace("{JobTitle}", jobTitle);
            interviewerEmailBody = interviewerEmailBody.Replace("{ApplicantName}", applicant.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{Date}", date);
            interviewerEmailBody = interviewerEmailBody.Replace("{Time}", time);
            interviewerEmailBody = interviewerEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            interviewerEmailBody = interviewerEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");


            interviewerEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = interviewerEmailBody
            };

            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Interview Notice";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/ApplicantDateNotice.html");

            applicantEmailBody = applicantEmailBody.Replace("{ApplicantName}", applicant.FirstName);
            applicantEmailBody = applicantEmailBody.Replace("{InterviewType}", interviewType);
            applicantEmailBody = applicantEmailBody.Replace("{JobTitle}", jobTitle);
            applicantEmailBody = applicantEmailBody.Replace("{Date}", date);
            applicantEmailBody = applicantEmailBody.Replace("{Time}", time);
            applicantEmailBody = applicantEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            applicantEmailBody = applicantEmailBody.Replace("{Email}", "alliance.humanresourceteam@gmail.com");

            applicantEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applicantEmailBody
            };

            await _emailService.SendEmail(interviewerEmail);
            await _emailService.SendEmail(applicantEmail);
        }

        public async Task SendInterviewReminderEmail(string applicant, Interviewers interviewer, string interviewType, string dateAndTime)
        {
            var interviewerEmail = new MimeMessage();

            interviewerEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            interviewerEmail.To.Add(new MailboxAddress(interviewer.Name, interviewer.Email));
            interviewerEmail.Subject = "Upcoming Interview Schedcule";

            string interviewerEmailBody = File.ReadAllText("wwwroot/emailTemplates/InterviewerScheduleNotice.html");

            interviewerEmailBody = interviewerEmailBody.Replace("{Interviwername}", interviewer.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewType}", interviewType);
            interviewerEmailBody = interviewerEmailBody.Replace("{Example Applicant}", applicant);
            interviewerEmailBody = interviewerEmailBody.Replace("{DateAndTime}", dateAndTime);
            interviewerEmailBody = interviewerEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            interviewerEmailBody = interviewerEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");


            interviewerEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = interviewerEmailBody
            };

            var hrNotifEmail = new MimeMessage();

            hrNotifEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            hrNotifEmail.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            hrNotifEmail.Subject = "Plot Interview Schedule";

            string hrEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRPlotScheduleNotice.html");

            hrEmailBody = hrEmailBody.Replace("{Example Applicant}", applicant);
            hrEmailBody = hrEmailBody.Replace("{InterviewType}", interviewType);
            hrEmailBody = hrEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            hrEmailBody = hrEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            hrNotifEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            await _emailService.SendEmail(interviewerEmail);
            await _emailService.SendEmail(hrNotifEmail);
        }

        public async Task SendHrApprovedScheduleEmail(Applicants applicant, string date, string time)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Interview/Examination Confirmation";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantInterviewRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{Date}", date);
            emailBodyTemplate = emailBodyTemplate.Replace("{Time}", time);
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendHrInterviewApprovalEmail(Applicants applicant, string interviewType)
        {
            var hrNotifEmail = new MimeMessage();

            hrNotifEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            hrNotifEmail.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            hrNotifEmail.Subject = "Interview/Exam Status";

            string hrEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRInterviewStatusReview.html");

            hrEmailBody = hrEmailBody.Replace("{Name}", applicant.Name);
            hrEmailBody = hrEmailBody.Replace("{InterviewTypee}", interviewType);
            hrEmailBody = hrEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            hrNotifEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            await _emailService.SendEmail(hrNotifEmail);
           
        }

        public async Task SendApplicantInterviewRegretEmail(Applicants applicant, string jobTitle, string interviewType)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Apologies and Regrets for Recent Application";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantInterviewRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{InterviewType}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendReferenceFormEmail(CharacterReferences characterReference, string applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(characterReference.Name, characterReference.Email));
            email.Subject = "Request for Character Reference";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ReferenceRequestForm.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", characterReference.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "alliance.jobhiring@gmail.com"); // add reference form link
            emailBodyTemplate = emailBodyTemplate.Replace("{Company Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendReferenceGratitudeEmail(CharacterReferences characterReference, string applicant)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(characterReference.Name, characterReference.Email));
            email.Subject = "Request for Character Reference";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ReferenceGratitude.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", characterReference.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant);

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendApplicantReferenceNotificationEmail(Applicants applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Reference Request Follow-Up";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantReferenceAbsent.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendHrAnsweredFormNotificationEmail(Applicants applicant)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Completion of Reference Form for Applicant Evaluation";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantReferenceAbsent.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendHrReferenceApprovalEmail(Applicants applicants)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Character Reference Approval";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ReferenceCheck.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicants.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "ReferenceFormLink1"); //add link for reference 1
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "ReferenceFormLink2"); //add link for reference 2
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendHrJobOfferApprovalEmail(Applicants applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Job Offer Approval";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRJobOfferAccepted.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{CompanyEmail}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendDtRequirementNotificationEmail(Applicants applicant, string jobTitle)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("Deployment Team", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Deployment Requirements";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/DTRequirements.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{Link}", "link for input requirements"); // add link
            emailBodyTemplate = emailBodyTemplate.Replace("{Email}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        public async Task SendDtConfirmationEmail(Applicants applicant)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("Deployment Team", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Onboarding Confirmation";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/DTConfirmation.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{CompanyEmail}", "alliance.jobhiring@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }
    }
}
