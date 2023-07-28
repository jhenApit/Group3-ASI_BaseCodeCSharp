using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace Basecode.Services.Services
{
    public class SendEmailService : ISendEmailService
    {
        public void SendHrDetailsEmail(HrEmployee hrEmployee, string password)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Alliance Job Hiring", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("", hrEmployee.Email));
            email.Subject = "HR Account Details";

            var emailBody = new TextPart("plain")
            {
                Text = @"Good Day {Name},

                   This is your Alliance HR Account Details

                    Username : {Username}
                    Password : {Password}

                    You may edit you details once you have logged in. Welcome to the team.
                   
                    -- Alliance Software Inc."
            };

            emailBody.Text = emailBody.Text.Replace("{Name}", hrEmployee.Name);
            emailBody.Text = emailBody.Text.Replace("{Username}", hrEmployee.User.UserName);
            emailBody.Text = emailBody.Text.Replace("{Password}", password);

            email.Body = emailBody;

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                client.Authenticate("alliance.jobhiring@gmail.com", "zepzkrqsybiahgmf");
                client.Send(email);
                client.Disconnect(true);
            }
        }

        public void SendApprovalEmail(string recipientEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public void SendRegretEmail(string recipientEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
