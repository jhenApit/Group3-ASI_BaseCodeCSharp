﻿using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _service;
        private readonly IEmailService _emailService;
        private readonly IApplicantService _applicantService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;

        public ApplicantController(IEmailService emailService, IApplicantService applicantService, IAddressService addressService, ICharacterReferencesService characterService)
        {
            _applicantService = applicantService;
            _addressService = addressService;
            _characterService = characterService;
            _emailService = emailService;
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
        public IActionResult ApplicationFormViewModel()
        {
			var recipient = "jm.senening08@gmail.com";
			var subject = "Application Update";
			var body = "Your application ID is APPL-1234";

			_emailService.SendEmail(recipient, subject, body);

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

        /// <summary>
        /// after clicking the submit button in application form view,
        /// it will lead to this controller. that handles the adding.
        /// it is not tested coz i'm having trouble figuring out how to insert
        /// 2 character ref at one post :)) --Kath
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ApplicationForm(ApplicationFormViewModel model)
        {
            _applicantService.Add(model.Applicant);
            _addressService.Add(model.Address);
            _characterService.Add(model.CharacterReferences);
            
            return View("ApplicationForm");
        }

    }
}
