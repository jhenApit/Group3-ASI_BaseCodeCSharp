using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IApplicantService _applicantService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IJobPostingsService _jobPostingsService;

        public ApplicantController(IJobPostingsService jobPostingsService, IEmailService emailService, IApplicantService applicantService, IAddressService addressService, ICharacterReferencesService characterService)
        {
            _emailService = emailService;
            _applicantService = applicantService;
            _addressService = addressService;
            _characterService = characterService;
            _jobPostingsService = jobPostingsService;
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
        public IActionResult ApplicationForm(int id)
        {
            var jobPosting = _jobPostingsService.GetById(id);
            if (jobPosting != null)
            {
                var viewModel = new ApplicationFormViewModel();
                viewModel.Applicant = new ApplicantCreationDto(); // Initialize the Applicant property
                viewModel.Applicant.JobId = id;
                Console.WriteLine("Job exists! " + id);
                return View(viewModel);
            }
            else
            {
                Console.WriteLine("Job doesn't exist! " +id);
                return View();
            }
        }

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
        public IActionResult ApplicationFormProcess(ApplicationFormViewModel model, IFormFile resumeFile)
        {
            if (resumeFile != null && resumeFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    resumeFile.CopyTo(memoryStream);

                    // Convert the file content to a byte array and store it in the model
                    model.Applicant.Resume = memoryStream.ToArray();
                }
            }
            bool applicant = _applicantService.Add(model.Applicant);
            var address = new AddressCreationDto
            {
                ApplicantId = model.Applicant.Id,
                Street = model.Address.Street,
                City = model.Address.City,
                Province = model.Address.Province,
                ZipCode = model.Address.ZipCode
            };

            var characRef1 = new CharacterReferencesCreationDto
            {
                ApplicantId = model.Applicant.Id,
                Name = model.CharacterReferences1.Name,
                Relationship = model.CharacterReferences1.Relationship,
                Email = model.CharacterReferences1.Email,
                MobileNumber = model.CharacterReferences1.MobileNumber
            };

            var characRef2 = new CharacterReferencesCreationDto
            {
                ApplicantId = model.Applicant.Id,
                Name = model.CharacterReferences2.Name,
                Relationship = model.CharacterReferences2.Relationship,
                Email = model.CharacterReferences2.Email,
                MobileNumber = model.CharacterReferences2.MobileNumber
            };

            if (applicant == true)
            {
                _addressService.Add(address);
                _characterService.Add(characRef1);
                _characterService.Add(characRef2);
            }
            else
            {
                Console.WriteLine("Addition Failed for applicant");
                return View("JobPostList");
            }

            var recipient = model.Applicant.Email;
            var subject = "Application Update";
            var body = "Your application ID is " + model.Applicant.ApplicantId;

            _emailService.SendEmail(recipient, subject, body);
            return View("ApplicationForm");
        }

    }
}
