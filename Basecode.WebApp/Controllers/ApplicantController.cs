using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Mvc;
using static Basecode.Data.Enums.Enums;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _service;
        private readonly IEmailService _emailService;
        private readonly EmailSender _emailSender;

        public ApplicantController(IApplicantService service, IEmailService emailService, EmailSender emailSender)
        {
            _service = service;
            _emailService = emailService;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Retrieves the track status of an applicant based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the applicant.</param>
        /// <returns>The view displaying the track status of the applicant.</returns>
        public async Task<IActionResult> TrackStatus(int id)
        {
            Applicant data = _service.GetById(id);

            await _emailSender.SendEmailAsync(EmailType.ScreeningEmailNotificationApplicant, "jm.senening08@gmail.com");
            await _emailSender.SendEmailAsync(EmailType.ScreeningEmailNotificationHR, "jm.senening08@gmail.com");

            return View(data);
        }

        /// <summary>
        /// Displays the contact us page.
        /// </summary>
        /// <returns>The contact us view.</returns>
        public IActionResult ContactUs()
        {
            return View();
        }

        /// <summary>
        /// Displays the track application page.
        /// </summary>
        /// <returns>The track application view.</returns>
        public IActionResult TrackApplication()
        {
            return View();
        }

        /// <summary>
        /// Displays the application status page.
        /// </summary>
        /// <returns>The application status view.</returns>
        public IActionResult ApplicationStatus()
        {   
            return View();
        }

        /// <summary>
        /// Displays the application form page.
        /// </summary>
        /// <returns>The application form view.</returns>
        public async Task<IActionResult> ApplicationForm()
        {
            /*var recipient = "jm.senening08@gmail.com";
			var subject = "Application Update";
			var body = "Your application ID is APPL-1234";

			_emailService.SendEmail(recipient, subject, body);*/

            await _emailSender.SendEmailAsync(EmailType.ScreeningEmailApplicantID, "jm.senening08@gmail.com");

			return View();
        }

        /// <summary>
        /// Displays the terms and conditions page.
        /// </summary>
        /// <returns>The terms and conditions view.</returns>
        public IActionResult TermsAndConditions()
        {
            return View();
        }

    }
}
