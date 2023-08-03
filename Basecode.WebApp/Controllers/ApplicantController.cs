using System.ComponentModel.DataAnnotations;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.Services.Utils;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IApplicantService _applicantService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IJobPostingsService _jobPostingsService;
        private readonly IErrorHandling _errorHandling;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantController(IErrorHandling errorHandling, IJobPostingsService jobPostingsService, IEmailService emailService, IApplicantService applicantService, IAddressService addressService, ICharacterReferencesService characterService)
        {
            _emailService = emailService;
            _applicantService = applicantService;
            _addressService = addressService;
            _characterService = characterService;
            _jobPostingsService = jobPostingsService;
            _errorHandling = errorHandling;
        }

        /// <summary>
        /// Retrieves the track status of an applicant based on the provided applicantID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant.</param>
        /// <returns>The view displaying the status of employment of the applicant.</returns>
        public async Task<IActionResult> ApplicationStatus(string ApplicantId)
        {
            Applicants data = await _applicantService.GetByApplicantIdAsync(ApplicantId);
            Console.WriteLine("Applicant Id+" + ApplicantId);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("TrackApplication");
        }
        /// <summary>
        /// this displays the view for TrackApplication
        /// </summary>
        /// <returns>the view</returns>
        public IActionResult TrackApplication(string from)
        {
            ViewBag.IsFromApplication = (from == "application");
            return View();
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
        /// Displays the application form page.
        /// </summary>
        /// <returns>The application form view.</returns>
        public async Task<IActionResult> ApplicationForm(int id)
        {
            var jobPosting = await _jobPostingsService.GetByIdAsync(id);
            if (jobPosting != null)
            {
                var viewModel = new ApplicationFormViewModel
                {
                    JobPosting = new JobPostings
                    {
                        Name = jobPosting.Name,
                        Id = jobPosting.Id
                    }
                };
                Console.WriteLine("Job exists! " + id);
                return View(viewModel);
            }
            else
            {
                Console.WriteLine("Job doesn't exist! " + id);
                return View();
            }
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
        public async Task<IActionResult> ApplicationFormProcess(ApplicationFormViewModel model, [Required] IFormFile resumeFile, IFormFile? photo)
        {
            if (resumeFile != null && resumeFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await resumeFile.CopyToAsync(memoryStream);

                    // Convert the file content to a byte array and store it in the model
                    model.Applicant.Resume = memoryStream.ToArray();
                }
            }
            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);

                    // Convert the file content to a byte array and store it in the model
                    model.Applicant.Photo = memoryStream.ToArray();
                }
            }
            else
            {
                model.Applicant.Photo = null;
            }
            ModelState.Clear();
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                //logger to be implemented
                //var data = _applicantService.AddApplicantLogContent(model.Applicant);
                //if (!data.Result)
                //{
                //    //_logger.Error(_errorHandling.SetLog(data));
                //    ViewBag.ErrorMessage = data.Message;
                //    //get current job applied
                //    var jobPosting = await _jobPostingsService.GetByIdAsync(model.Applicant.JobId);
                //    //pass job to view model
                //    model.JobPosting = new JobPostings
                //    {
                //        Name = jobPosting.Name,
                //        Id = jobPosting.Id
                //    };
                //    return View("ApplicationForm", model);
                //}

                var applicantIsInserted = await _applicantService.AddAsync(model.Applicant);
                var address = new AddressCreationDto
                {
                    ApplicantId = applicantIsInserted,
                    Street = model.Address.Street,
                    City = model.Address.City,
                    Province = model.Address.Province,
                    ZipCode = model.Address.ZipCode
                };

                var characRef1 = new CharacterReferencesCreationDto
                {
                    ApplicantId = applicantIsInserted,
                    Name = model.CharacterReferences1.Name,
                    Relationship = model.CharacterReferences1.Relationship,
                    Email = model.CharacterReferences1.Email,
                    MobileNumber = model.CharacterReferences1.MobileNumber
                };

                var characRef2 = new CharacterReferencesCreationDto
                {
                    ApplicantId = applicantIsInserted,
                    Name = model.CharacterReferences2.Name,
                    Relationship = model.CharacterReferences2.Relationship,
                    Email = model.CharacterReferences2.Email,
                    MobileNumber = model.CharacterReferences2.MobileNumber
                };
                if (applicantIsInserted != 0)
                {
                    await _addressService.AddAsync(address);
                    await _characterService.AddAsync(characRef1);
                    await _characterService.AddAsync(characRef2);
                    var recipient = model.Applicant.Email;
                    var subject = "Application Update";
                    var body = "Your application ID is " + model.Applicant.ApplicantId;

                    await _emailService.SendEmail(recipient, subject, body);
                }
                else
                {
                    Console.WriteLine("Addition Failed for applicant");
                    return View("ViewJobPost");
                }
                return RedirectToAction("TrackApplication", new { from = "application" });
            }
            ModelState.Clear();
            return View("ApplicationForm");
        }

    }
}