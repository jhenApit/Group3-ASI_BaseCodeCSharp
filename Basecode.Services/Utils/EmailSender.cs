using Basecode.Data.Models;
using Basecode.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Services.Utils
{
    public class EmailSender
    {
        private readonly EmailService _emailService;
        private readonly HrEmployee _hrEmployee;
        private readonly Applicant _applicant;
        private readonly Interviewers _interviewers;
        public EmailSender(EmailService emailService, HrEmployee hrEmployee,Applicant applicant, Interviewers interviewers)
        {
            _emailService = emailService;
            _hrEmployee = hrEmployee;
            _applicant = applicant;
            _interviewers = interviewers;
        }

        public async Task SendEmailAsync(EmailType emailType, string recipient)
        {
            string subject;
            string body;

            try
            {
                switch (emailType)
                {
                    //Email for HR Creation Account
                    case EmailType.HRCreationAccountEmail:
                        subject = "Human Resource Account";
                        body = $"Dear Mr/Mrs {_hrEmployee.Name}, <br/> <br/> This is your human resource account. <br/>" +
                               $"<br/> Email: {_hrEmployee.Email} <br/> Password: {_hrEmployee.Password} <br/>" +
                               "<br/> You can edit your profile once you've logged in.";
                        break;

                    //Email for Screening
                    case EmailType.ScreeningEmailNotificationHR:
                        subject = "Applicant's Status";
                        body = $"Dear Mr/Mrs {_hrEmployee.Name}, <br/> <br/> " +
                               $"<p> Applicant {_applicant.Name} applicantion's status is {_applicant.Status}.</p>";
                        break;

                    case EmailType.ScreeningEmailNotificationApplicant:
                        subject = "Applicant's Status";
                        body = $"Dear Mr/Mrs {_applicant.Name}, <br/> <br/> <p>Your application status is {_applicant.Status}.</p>";
                        break;

                    case EmailType.ScreeningEmailApplicantID:
                        subject = "Applicantion Entry";
                        body = $"Dear Mr/Mrs {_applicant.Name},<br/> <br/> <p>Your applicant ID is {_applicant.ApplicantId}.</p>";
                        break;

                    case EmailType.ScreeningEmailofApproval: // contains 2 buttons Approve/Reject
                        subject = "Approval Email";
                        body = $"Dear Mr/Mrs {_hrEmployee.Name},<br/> <br/> <p>Congratulations!c Your application has been approved.</p>";
                        break;

                    case EmailType.ScreeningEmailofRegrets:
                        subject = "Email of Regrets";
                        body = $"Dear Mr/Mrs {_applicant.Name}, <br/> <br/> <p>We regret to inform you that your application was not successful.</p>";
                        break;

                    //Email for Interview
                    case EmailType.InterviewEmailNotificationInterviewer:
                        subject = "Interview Schedule";
                        body = $"Dear Mr/Mrs {_interviewers.Name},<br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailNotificationApplicant:
                        subject = "Interview Schedule";
                        body = $"Dear Mr/Mrs {_applicant.Name}, <br/> <br/><p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewReminderEmailInterviewer:
                        subject = "Schedule Reminder";
                        body = $"Dear Mr/Mrs {_interviewers.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewReminderEmailHR:
                        subject = "Schedule Reminder";
                        body = $"Dear Mr/Mrs {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailofApprovalApplicantSchedule:
                        subject = "Approved Schedule";
                        body = $"Dear Mr/Mrs {_applicant.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailNotificationAcceptedSchedule:
                        subject = "Accepted Schedule";
                        body = $"Dear Mr/Mrs {_interviewers.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailOfApproval: // contains 2 buttons Passed/Failed
                        subject = "Inteview/Examination Result";
                        body = $"Dear Mr/Mrs {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailOfRegrets:
                        subject = "Email of Regrets";
                        body = $"Dear Mr/Mrs {_applicant.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    default:
                        throw new ArgumentException("Invalid email type.");
                }

                await _emailService.SendEmail(recipient, subject, body);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERR! Failed to send email: " + ex.Message);
            }
           
        }
    }
}
