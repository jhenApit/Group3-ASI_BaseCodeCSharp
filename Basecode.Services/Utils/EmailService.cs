using Basecode.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Basecode.Services.Utils
{
    public class EmailService : IEmailService
    {
        public void SendEmail(MimeMessage email)
        {
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("alliance.jobhiring@gmail.com", "zepzkrqsybiahgmf");
            client.Send(email);
            client.Disconnect(true);
        }
    }
}
