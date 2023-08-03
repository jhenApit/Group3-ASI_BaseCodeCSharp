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
        private readonly IMeetingLinkService _meetingLinkService;

        public SendEmailService(IEmailService emailService, IMeetingLinkService meetingLinkService)
        {
            _emailService = emailService;
            _meetingLinkService = meetingLinkService;
        }

        /// <summary>
        /// Sends an email containing HR account details to the specified HR employee.
        /// </summary>
        /// <param name="hrEmployee">The HR employee to whom the email will be sent.</param>
        /// <param name="password">The password associated with the HR account.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendHrDetailsEmail(HrEmployee hrEmployee, string password) //done
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
        /// Sends an email containing a password reset link to the specified user's email address.
        /// </summary>
        /// <param name="user">The IdentityUser for whom the password reset link is being sent.</param>
        /// <param name="url">The URL where the user can reset their password.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends email notifications regarding the application status to the applicant and HR Team.
        /// </summary>
        /// <param name="applicant">The applicant whose application status is being updated.</param>
        /// <param name="status">The updated application status.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendApplicationStatusEmail(Applicants applicant, string status) //done
        {
            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Application Status Update";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/ApplicantApplicationStatusNotif.html");

            applicantEmailBody = applicantEmailBody.Replace("{Name}", applicant.FirstName);
            applicantEmailBody = applicantEmailBody.Replace("{JobName}", applicant.Job!.Name);
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

            string hrEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRApplicationStatusNotif.html");

            hrEmailBody = hrEmailBody.Replace("{Name}", applicant.Name);
            hrEmailBody = hrEmailBody.Replace("{ApplicantID}", applicant.ApplicantId);
            hrEmailBody = hrEmailBody.Replace("{JobTitle}", applicant.Job!.Name);
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
        /// Sends email notifications when a new applicant submits an application form.
        /// </summary>
        /// <param name="applicant">The new applicant whose application form is submitted.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendNewApplicantEmail(Applicants applicant) // done
        {
            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Application Form Submitted";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/ApplicationFormSubmitted.html");

            applicantEmailBody = applicantEmailBody.Replace("{Name}", applicant.FirstName);
            applicantEmailBody = applicantEmailBody.Replace("{JobTitle}", applicant.Job!.Name);
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
            hrEmailBody = hrEmailBody.Replace("{JobTitle}", applicant.Job!.Name);
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
        /// Sends an email of approval to the HR team about the screening of a shortlisted applicant.
        /// </summary>
        /// <param name="applicant">The shortlisted applicant who is being screened.</param>
        /// <param name="jobTitle">The title of the job for which the applicant is shortlisted.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendHrApplicationApprovalEmail(Applicants applicant) //done
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            email.Subject = "Screening Applicant";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/HRScreeningApplicant.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", applicant.Job!.Name);
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

        /// <summary>
        /// Sends an email to the applicant to express apologies and regrets for the recent application rejection.
        /// </summary>
        /// <param name="applicant">The applicant who received the application rejection.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendApplicantApplicationRegretEmail(Applicants applicant)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Apologies and Regrets for Recent Application";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantApplicationRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Job}", applicant.Job!.Name);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        /// <summary>
        /// Sends email notifications to an interviewer and an applicant to inform them about the scheduled interview.
        /// </summary>
        /// <param name="interviewer">The interviewer for whom the interview schedule is being sent.</param>
        /// <param name="applicant">The applicant who is scheduled for an interview.</param>
        /// <param name="interviewType">The type of the interview (e.g., in-person, remote).</param>
        /// <param name="jobTitle">The title of the job for which the interview is scheduled.</param>
        /// <param name="date">The date of the scheduled interview.</param>
        /// <param name="time">The time of the scheduled interview.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendSetInterviewScheduleEmail(Interviews interview, Applicants applicant) // done
        {
            var interviewerEmail = new MimeMessage();

            interviewerEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            interviewerEmail.To.Add(new MailboxAddress(interview.Interviewer.Name, interview.Interviewer.Email));
            interviewerEmail.Subject = "Interview Notice";

            string interviewerEmailBody = File.ReadAllText("wwwroot/emailTemplates/InterviewerDateNotice.html");

            interviewerEmailBody = interviewerEmailBody.Replace("{Interviwername}", interview.Interviewer.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            interviewerEmailBody = interviewerEmailBody.Replace("{JobTitle}", applicant.Job!.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{ApplicantName}", applicant.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            interviewerEmailBody = interviewerEmailBody.Replace("{StartTime}", interview.TimeStart);
            interviewerEmailBody = interviewerEmailBody.Replace("{FinishTime}", interview.TimeEnd);
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
            applicantEmailBody = applicantEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            applicantEmailBody = applicantEmailBody.Replace("{JobTitle}", applicant.Job.Name);
            applicantEmailBody = applicantEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            applicantEmailBody = applicantEmailBody.Replace("{StartTime}", interview.TimeStart);
            applicantEmailBody = applicantEmailBody.Replace("{FinishTime}", interview.TimeEnd);
            applicantEmailBody = applicantEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            applicantEmailBody = applicantEmailBody.Replace("{Email}", "alliance.humanresourceteam@gmail.com");

            applicantEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applicantEmailBody
            };

            await _emailService.SendEmail(interviewerEmail);
            await _emailService.SendEmail(applicantEmail);
        }

        /// <summary>
        /// Sends email reminders to an interviewer and the HR team about an upcoming interview schedule.
        /// </summary>
        /// <param name="applicant">The name of the applicant scheduled for the interview.</param>
        /// <param name="interviewer">The interviewer who will conduct the interview.</param>
        /// <param name="interviewType">The type of the interview (e.g., in-person, remote).</param>
        /// <param name="dateAndTime">The date and time of the scheduled interview.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendInterviewReminderEmail(Interviews interview, Applicants applicant) 
        {
            var interviewerEmail = new MimeMessage();

            interviewerEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            interviewerEmail.To.Add(new MailboxAddress(interview.Interviewer.Name, interview.Interviewer.Email));
            interviewerEmail.Subject = "Upcoming Interview Schedcule";

            string interviewerEmailBody = File.ReadAllText("wwwroot/emailTemplates/InterviewerScheduleNotice.html");

            interviewerEmailBody = interviewerEmailBody.Replace("{Interviwername}", interview.Interviewer.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            interviewerEmailBody = interviewerEmailBody.Replace("{Example Applicant}", applicant.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            interviewerEmailBody = interviewerEmailBody.Replace("{StartTime}", interview.TimeStart);
            interviewerEmailBody = interviewerEmailBody.Replace("{EndTime}", interview.TimeEnd);
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

            hrEmailBody = hrEmailBody.Replace("{Example Applicant}", applicant.Name);
            hrEmailBody = hrEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            hrEmailBody = hrEmailBody.Replace("{Link}", "https://localhost:50140/Applicant/TrackApplication"); //add correct link
            hrEmailBody = hrEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            hrNotifEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            await _emailService.SendEmail(interviewerEmail);
            await _emailService.SendEmail(hrNotifEmail);
        }

        /// <summary>
        /// Sends an email confirmation to the team about the approved interview/examination schedule for an applicant.
        /// </summary>
        /// <param name="applicant">The applicant for whom the interview/examination is scheduled.</param>
        /// <param name="date">The date of the scheduled interview/examination.</param>
        /// <param name="time">The time of the scheduled interview/examination.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendApprovedScheduleEmail(Applicants applicant, Interviews interview)
        {
            List<string> attendeeEmails = new List<string>
            {
                "alliance.humanresourceteam@gmail.com",
                applicant.Email!,
                interview.Interviewer.Email!
            };
            var meetingLink = _meetingLinkService.GenerateLink(interview.InterviewType.ToString(), interview.InterviewDate,
                interview.TimeStart!, interview.TimeEnd!, attendeeEmails);

            var hrEmail = new MimeMessage();

            hrEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            hrEmail.To.Add(new MailboxAddress("HR Department", "alliance.humanresourceteam@gmail.com"));
            hrEmail.Subject = "Interview/Examination Confirmation";

            string hrEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRApplicantAcceptedDate.html");

            hrEmailBody = hrEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            hrEmailBody = hrEmailBody.Replace("{ApplicantName}", applicant.Name);
            hrEmailBody = hrEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            hrEmailBody = hrEmailBody.Replace("{StartTime}", interview.TimeStart);
            hrEmailBody = hrEmailBody.Replace("{EndTime}", interview.TimeEnd);
            hrEmailBody = hrEmailBody.Replace("{MeetingLink}", meetingLink);
            hrEmailBody = hrEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            hrEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = hrEmailBody
            };

            var interviewerEmail = new MimeMessage();

            interviewerEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            interviewerEmail.To.Add(new MailboxAddress(interview.Interviewer.Name, interview.Interviewer.Email));
            interviewerEmail.Subject = "Interview/Examination Confirmation";

            string interviewerEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRApplicantAcceptedDate.html");

            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewerName}", interview.Interviewer.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{InterviewType}", interview.InterviewType.ToString());
            interviewerEmailBody = interviewerEmailBody.Replace("{ApplicantName}", applicant.Name);
            interviewerEmailBody = interviewerEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            interviewerEmailBody = interviewerEmailBody.Replace("{StartTime}", interview.TimeStart);
            interviewerEmailBody = interviewerEmailBody.Replace("{EndTime}", interview.TimeEnd);
            interviewerEmailBody = interviewerEmailBody.Replace("{MeetingLink}", meetingLink);
            interviewerEmailBody = interviewerEmailBody.Replace("{Email}", "alliance.jobhiring@gmail.com");

            interviewerEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = interviewerEmailBody
            };


            var applicantEmail = new MimeMessage();

            applicantEmail.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            applicantEmail.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            applicantEmail.Subject = "Interview/Examination Confirmation";

            string applicantEmailBody = File.ReadAllText("wwwroot/emailTemplates/HRApplicantAcceptedDate.html");

            applicantEmailBody = applicantEmailBody.Replace("{ApplicantName}", applicant.Name);
            applicantEmailBody = applicantEmailBody.Replace("{JobTitle}", applicant.Job!.Name);
            applicantEmailBody = applicantEmailBody.Replace("{Date}", interview.InterviewDate.ToString("yyyy-MM-dd"));
            applicantEmailBody = applicantEmailBody.Replace("{StartTime}", interview.TimeStart);
            applicantEmailBody = applicantEmailBody.Replace("{EndTime}", interview.TimeEnd);
            applicantEmailBody = applicantEmailBody.Replace("{MeetingLink}", meetingLink);
            applicantEmailBody = applicantEmailBody.Replace("{Email}", "alliance.humanresourceteam@gmail.com");

            applicantEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applicantEmailBody
            };

            await _emailService.SendEmail(applicantEmail);
            await _emailService.SendEmail(interviewerEmail);
            await _emailService.SendEmail(hrEmail);
        }

        /// <summary>
        /// Sends an email of approval to the HR team about the interview/exam status for an applicant.
        /// </summary>
        /// <param name="applicant">The applicant for whom the interview/exam status is being updated.</param>
        /// <param name="interviewType">The type of the interview/exam (e.g., interview, examination).</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email to notify an applicant about the regret for a recent interview/examination.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <param name="jobTitle">The title of the job for which the applicant was interviewed.</param>
        /// <param name="interviewType">The type of interview/examination (e.g., initial interview, technical test).</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendApplicantInterviewRegretEmail(Applicants applicant, string jobTitle, string interviewType)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Apologies and Regrets for Recent Interview/Examination";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/ApplicantInterviewRejected.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{ApplicantName}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{JobTitle}", jobTitle);
            emailBodyTemplate = emailBodyTemplate.Replace("{InterviewType}", interviewType);
            emailBodyTemplate = emailBodyTemplate.Replace("{HR Team Email}", "alliance.humanresourceteam@gmail.com");

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailBodyTemplate
            };

            await _emailService.SendEmail(email);
        }

        /// <summary>
        /// Sends an email to request a character reference from a given character reference contact.
        /// </summary>
        /// <param name="characterReference">The character reference contact's information.</param>
        /// <param name="applicant">The name of the applicant for whom the character reference is requested.</param>
        /// <param name="jobTitle">The title of the job for which the character reference is requested.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email to express gratitude for providing a character reference to support an applicant's job application.
        /// </summary>
        /// <param name="characterReference">The character reference contact's information.</param>
        /// <param name="applicant">The name of the applicant for whom the character reference was provided.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email to notify an applicant that their provided references are absent or incomplete.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <param name="jobTitle">The title of the job for which the references are required.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email notification to the HR team about the completion of a reference form for applicant evaluation.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email notification to the HR department for character reference approval of an applicant.
        /// </summary>
        /// <param name="applicants">The applicant object containing applicant information.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email to an applicant containing a job offer with details about the job, work setup, and hours.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <param name="job">The title of the job being offered.</param>
        /// <param name="workSetup">The setup or type of work being offered (e.g., full-time, part-time).</param>
        /// <param name="hours">The expected working hours for the job.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendApplicantJobOfferEmail(Applicants applicant, string job, string workSetup, string hours)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Alliance HR Automation System", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress(applicant.Name, applicant.Email));
            email.Subject = "Job Offer";

            string emailBodyTemplate = File.ReadAllText("wwwroot/emailTemplates/JobOffer.html");

            emailBodyTemplate = emailBodyTemplate.Replace("{Name}", applicant.FirstName);
            emailBodyTemplate = emailBodyTemplate.Replace("{Job}", applicant.Job!.Name);
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
        /// Sends an email notification to the HR team for job offer approval of an applicant.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <param name="jobTitle">The title of the job being offered.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email notification to the Deployment Team regarding deployment requirements for an applicant.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <param name="jobTitle">The title of the job the applicant is being deployed for.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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

        /// <summary>
        /// Sends an email confirmation to the Deployment Team about the successful onboarding of an applicant.
        /// </summary>
        /// <param name="applicant">The applicant object containing applicant information.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
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
