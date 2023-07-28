using Basecode.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class SendEmailService : ISendEmailService
    {
        private async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Alliance Job Hiring", "alliance.jobhiring@gmail.com"));
            email.To.Add(new MailboxAddress("", recipientEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            email.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 578 , true);
                await client.AuthenticateAsync("alliance.jobhiring@gmail.com", "alliance.jobhiring2023");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
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
