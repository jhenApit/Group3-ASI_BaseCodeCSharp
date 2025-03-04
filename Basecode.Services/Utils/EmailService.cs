﻿using Basecode.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Basecode.Services.Utils
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Sends an email using the provided MimeMessage object through the configured SMTP client.
        /// </summary>
        /// <param name="email">The MimeMessage object representing the email to be sent.</param>
        /// <returns>A Task representing the asynchronous email sending process.</returns>
        public async Task SendEmail(MimeMessage email)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("alliance.jobhiring@gmail.com", "zepzkrqsybiahgmf");
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
    }
}
