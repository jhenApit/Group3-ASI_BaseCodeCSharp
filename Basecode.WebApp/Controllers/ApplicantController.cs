using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _applicantService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;

        public ApplicantController(IApplicantService applicantService, IAddressService addressService, ICharacterReferencesService characterService)
        {
            _applicantService = applicantService;
            _addressService = addressService;
            _characterService = characterService;
        }

        /// <summary>
        /// Retrieves the track status of an applicant based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the applicant.</param>
        /// <returns>The view displaying the track status of the applicant.</returns>
        public IActionResult TrackStatus(string applicantId)
        {
            Applicants data = _applicantService.GetByApplicantId(applicantId);
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
            // Assuming you have your models populated
            var applicantModel = new ApplicantCreationDto(); // ApplicantCreationDto
            var addressModel = new AddressCreationDto(); // AddressCreationDto
            var characterReferencesModel = new CharacterReferencesCreationDto(); // CharacterReferencesCreationDto

            var viewModel = new ApplicationFormViewModel
            {
                Applicant = applicantModel,
                Address = addressModel,
                CharacterReferences = characterReferencesModel
            };

            return View(viewModel);
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
