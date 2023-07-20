using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _service;

        public ApplicantController(IApplicantService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the track status of an applicant based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the applicant.</param>
        /// <returns>The view displaying the track status of the applicant.</returns>
        public IActionResult TrackStatus(string applicantId)
        {
            Applicants data = _service.GetByApplicantId(applicantId);
            return View("ApplicationStatus",data);
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
        /// Displays the track application page 
        /// when the user clicks on it on the header of the landing page
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
        public IActionResult ApplicationForm()
        {
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
