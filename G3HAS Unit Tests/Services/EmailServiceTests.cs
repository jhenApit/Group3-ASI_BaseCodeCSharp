using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Basecode.Services.Services;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class EmailServiceTests
	{
		[Fact]
		public async Task SendEmail_ShouldSendEmailSuccessfully()
		{
			// Arrange
			var recipient = "test@example.com";
			var subject = "Test Subject";
			var body = "<p>This is a test email.</p>";

			var smtpClientMock = new Mock<SmtpClient>("smtp-mail.outlook.com", 587);
			smtpClientMock.SetupAllProperties();
			smtpClientMock.SetupSet(smtp => smtp.EnableSsl = true);
			smtpClientMock.SetupSet(smtp => smtp.UseDefaultCredentials = false);
			smtpClientMock.SetupSet(smtp => smtp.Credentials = It.IsAny<NetworkCredential>());

			var mailMessageMock = new Mock<MailMessage>("alliance.hrautomationsystem@outlook.com", recipient);
			mailMessageMock.SetupAllProperties();
			mailMessageMock.Object.Subject = subject;
			mailMessageMock.Object.Body = body;
			mailMessageMock.Object.IsBodyHtml = true;

			smtpClientMock.Setup(smtp => smtp.SendMailAsync(mailMessageMock.Object))
						  .Returns(Task.CompletedTask);

			var emailService = new EmailService();

			// Act
			await emailService.SendEmail(recipient, subject, body);

			// Assert
			smtpClientMock.Verify(smtp => smtp.SendMailAsync(mailMessageMock.Object), Times.Once);
		}
	}
}
