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
        public EmailSender(EmailService emailService, HrEmployee hrEmployee)
        {
            _emailService = emailService;
            _hrEmployee = hrEmployee;
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
                    case EmailType.ScreeningEmailNotification:
                        subject = "Applicant's Status";
                        body = "<h1>Dear New User,</h1><p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.ScreeningEmailApplicantID:
                        subject = "Applicantion Entry";
                        body = "<h1>Dear Applicant,</h1><p>Your applicant ID is XYZ123.</p>";
                        break;

                    case EmailType.ScreeningEmailofApproval:
                        subject = "Congratulations!";
                        body = "<h1>Dear New User,</h1><p>Congratulations! Your application has been approved.</p>";
                        break;

                    case EmailType.ScreeningEmailofRegrets:
                        subject = "Regarding Your Application";
                        body = "<h1>Dear New User,</h1><p>We regret to inform you that your application was not successful.</p>";
                        break;

                    //Email for Interview
                    case EmailType.InterviewEmailNotificationSchedule:
                        subject = "Welcome to Our Service";
                        body = "<h1>Dear New User,</h1><p>Thank you for joining our service.</p>";
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
