using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
	public class EmailService : IEmailService
	{
		public async Task SendEmail(string recipient, string subject, string body)
		{
			try
			{
				using (var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587))
				{
					smtpClient.EnableSsl = true;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new NetworkCredential("alliance.hrautomationsystem@outlook.com", "hrautomationsystem3");

					using (var mailMessage = new MailMessage("alliance.hrautomationsystem@outlook.com", recipient))
					{
						mailMessage.Subject = subject;
						mailMessage.Body = body;
						mailMessage.IsBodyHtml = true;

						await smtpClient.SendMailAsync(mailMessage);
					}
				}

				Console.WriteLine("Email sent successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERR! Failed to send email: " + ex.Message);
			}
		}

	}
}
