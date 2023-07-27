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
        private readonly CharacterReferences _characterReferences;
        public EmailSender(EmailService emailService, HrEmployee hrEmployee,Applicant applicant, 
                           Interviewers interviewers, CharacterReferences characterReferences)
        {
            _emailService = emailService;
            _hrEmployee = hrEmployee;
            _applicant = applicant;
            _interviewers = interviewers;
            _characterReferences = characterReferences;
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
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> This is your human resource account. <br/>" +
                               $"<br/> Email: {_hrEmployee.Email} <br/> Password: {_hrEmployee.Password} <br/>" +
                               "<br/> You can edit your profile once you've logged in.";
                        break;

                    //Email for Screening
                    case EmailType.ScreeningEmailNotificationHR:
                        subject = "Applicant's Status";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> " + 
                               $"<p>We wanted to notify you of a recent applicant status change in our Applicant Database.</p> <br/> <br/>" + 
                               $"Applicant:{_applicant.Name} <br/> Application ID: {_applicant.ApplicantId} <br/> Status: {_applicant.Status} <br/> <br/>" + 
                               $"<p>Please ensure you review the updated status and take any necessary actions accordingly <br/> <br/> Thank you.</p>";
                        break;

                    case EmailType.ScreeningEmailNotificationApplicant:
                        subject = "Applicant Status Change";
                        body = $"Dear {_applicant.Name}, <br/> <br/> " +
                               $"<p>We are writing to inform you of a recent update regarding your application with us:.</p> <br/> <br/>" +
                               $"Application ID: {_applicant.ApplicantId} <br/> Status: {_applicant.Status} <br/> <br/>" +
                               $"<p>If you have any questions or need further information, please feel free to contact us.</p> <br/> <br/>" +
                               $"<p>Thank you for your interest, and we wish you the best of luck with your application.</p>";
                        break;

                    case EmailType.ScreeningEmailApplicantID:
                        subject = "Your Unique Applicant ID for your Application";
                        body = $"Dear {_applicant.Name},<br/> <br/> " + 
                               $"<p>Thank you for your recent application. We are pleased to provide you with your unique Applicant ID: {_applicant.ApplicantId}.</p> <br/> <br/>" + 
                               $"<p>Should you have any inquiries or require updates on your application status, please feel free to reach out.</p>";
                        break;

                    case EmailType.ScreeningEmailofApproval: // contains 2 buttons Approve/Reject
                        subject = "Approval Email";
                        body = $"Dear {_hrEmployee.Name},<br/> <br/> <p>Congratulations!c Your application has been approved.</p>";
                        break;

                    case EmailType.ScreeningEmailofRegrets:
                        subject = "Email of Regrets";
                        body = $"Dear {_applicant.Name}, <br/> <br/> " +
                               $"<p>Thank you for your recent application. We wanted to inform you that after careful consideration, we " +
                               $"have decided not to move forward with your application at this time. We appreciate your interest in our company and wish you success in your " +
                               $"job search.</p>";
                        break;

                    //Email for Interview
                    case EmailType.InterviewEmailNotificationInterviewer:
                        subject = "Interview Schedule";
                        body = $"Dear {_interviewers.Name},<br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailNotificationApplicant:
                        subject = "Interview Schedule";
                        body = $"Dear {_applicant.Name}, <br/> <br/><p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewReminderEmailInterviewer:
                        subject = "Schedule Reminder";
                        body = $"Dear {_interviewers.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewReminderEmailHR:
                        subject = "Schedule Reminder";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailofApprovalApplicantSchedule:
                        subject = "Approved Schedule";
                        body = $"Dear {_applicant.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailNotificationAcceptedSchedule:
                        subject = "Interview/Examination Schedule Confirmation";
                        body = $"Dear {_interviewers.Name}, <br/> <br/> " +
                               $"<p>Just a quick note to confirm the schedule for the upcoming interview/examination with the applicant:</p> <br/> <br/>" +
                               $"Applicant: {_applicant.Name} <br/> Date: <br/> Time: <br/> <br/> <p>The applicant has accepted the provided schedule.</p>" +
                               $"<br/> <br/> Thank you.";
                        break;

                    case EmailType.InterviewEmailOfApproval: // contains 2 buttons Passed/Failed
                        subject = "Inteview/Examination Result";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.InterviewEmailOfRegrets:
                        subject = "Outcome of the Interview/Exam Application";
                        body = $"Dear {_applicant.Name}, <br/> <br/> " +
                               $"<p>Thank you for your recent application. We sincerely appreciate the effort and dedication you demonstrated " +
                               $"throughout the interview/exam process.</p> <br/> <br/> <p>While we carefully review all applications, we regret to inform you that we have chosen to " +
                               $"proceed with other candidates who closely align with our specific requirements.</p> <br/> <br/> <p>Please know that your interest in our organization is " +
                               $"valued, and we encourage you to continue pursuing opportunities that match your skills and aspirations.</p> <br/> <br/> <p>Thank you once again for " +
                               $"considering us as part of your journey. We wish you every success in your future endeavors.</p>";
                        break;

                    //Email for Background Check
                    case EmailType.BackgroundCheckEmailofGratitude:
                        subject = "Gratitude for Completing the Reference Form";
                        body = $"Dear {_characterReferences.Name}, <br/> <br/> " +
                               $"<p> We want to express our heartfelt gratitude for completing the reference form. " +
                               $"Your support and kind words to the applicant mean a lot to us.</p> <br/> <br/> <p>Your thoughtful recommendation holds great value, and we are " +
                               $"humbled by the trust you've shown in the applicant's abilities. We believe it will positively impact the applicant's future opportunities.</p>" +
                               $"<br/> <br/> <p>Thank you once again for your valuable assistance.</p>";
                        break;

                    case EmailType.BackgroundCheckReminderEmail:
                        subject = "Gentle Reminder: Character Reference Response for your Application";
                        body = $"Dear {_applicant.Name}, <br/> <br/> " + 
                               $"<p>We hope you're doing well. We're grateful for your application and the character references you provided.</p> <br/> <br/>" + 
                               $"<p>We have received responses from the following references:</p> <br/> <br/> Reference: {_characterReferences.Name} <br/> <br/>" +
                               $"<p>However, we haven't received responses from the following references yet: </p> <br/> <br/> Reference: {_characterReferences.Name} <br/> <br/>" +
                               $"<p>Please remind them to submit their feedback at their earliest convenience.</p> <br/> <br/> <p>Thank you for your prompt attention.</p>";
                        break;

                    case EmailType.BackgroundCheckEmailNotification:
                        subject = "Reference Form Successfully Submitted - Applicant Identification";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> " +
                               $"<p>We're pleased to inform you that a reference successfully completed the reference form for an applicant.</p> <br/> <br/>" +
                               $"Reference Information: <br/> <br/> Name: {_characterReferences.Name} <br/> Email: {_characterReferences.Email} <br/> <br/>" +
                               $"Applicant Information: <br/> <br/> Name: {_applicant.Name} <br/> Email: {_applicant.Email} <br/> <br/>" +
                               $"<p>Thank you for your valuable assistance in our hiring process.</p>";
                        break;

                    case EmailType.BackgroundCheckEmailOfApproval:
                        subject = "background Check Approval Email";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    //Email for Job Offer
                    case EmailType.JobOfferEmailOfApproval:
                        subject = "Job Offer Approval Email";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.JobOfferEmailNotification:
                        subject = "Job Offer Email Notification";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
                        break;

                    case EmailType.JobOfferConfirmationEmail:
                        subject = "Job Offer Confirmation Email";
                        body = $"Dear {_hrEmployee.Name}, <br/> <br/> <p>Thank you for joining our service.</p>";
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
