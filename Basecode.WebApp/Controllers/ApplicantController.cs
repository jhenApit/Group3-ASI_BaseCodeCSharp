using System.ComponentModel.DataAnnotations;
using Basecode.Data.Dtos.CurrentHires;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;
using static Basecode.Data.Enums.Enums;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IEmailService _emailService;
		private readonly ICurrentHiresService _currentHiresService;
		private readonly IApplicantService _applicantService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IJobPostingsService _jobPostingsService;
        private readonly IErrorHandling _errorHandling;
        private readonly ISendEmailService _sendEmailService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ApplicantController(IErrorHandling errorHandling, ICurrentHiresService currentHiresService, IJobPostingsService jobPostingsService, IEmailService emailService, IApplicantService applicantService, IAddressService addressService, ICharacterReferencesService characterService)
        {
            _applicantService = applicantService;
            _addressService = addressService;
			_currentHiresService = currentHiresService;
			_characterService = characterService;
            _jobPostingsService = jobPostingsService;
            _errorHandling = errorHandling;
            _sendEmailService = sendEmailService;
        }

        /// <summary>
        /// Retrieves the track status of an applicant based on the provided applicantID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant.</param>
        /// <returns>The view displaying the status of employment of the applicant.</returns>
        public async Task<IActionResult> ApplicationStatus(string ApplicantId)
        {
            try
            {
                Applicants data = await _applicantService.GetByApplicantIdAsync(ApplicantId);
                Console.WriteLine("Applicant Id: " + ApplicantId);

                if (data != null)
                {
                    return View(data);
                }

                return RedirectToAction("TrackApplication");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return BadRequest("An error occured when retrieving application status.");
            }
        }
        /// <summary>
        /// this displays the view for TrackApplication
        /// </summary>
        /// <returns>the view</returns>
        public IActionResult TrackApplication(string from)
        {
            try
            {
                ViewBag.IsFromApplication = (from == "application");
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return BadRequest("An error occured when trying to access track application");
            }
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
            try
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
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return BadRequest("An error occured when trying to access application form");
            }
        }

		/// <summary>
		/// this will update the applicants status
		/// <param name="id">the id of the applicant to be updated</param>
		/// <param name="status">and the status it wants to uupdate to</param>
		/// <returns>returns the jobapplicant overview view if succesful</returns>
		[HttpPost]
		public async Task<IActionResult> UpdateApplicantStatus(int id, string status)
		{
			try
			{
				var applicant = await _applicantService.GetByIdAsync(id);
				if (applicant != null)
				{
					if (status == "Confirmed")
					{
						var hired = new CurrentHiresCreationDto
						{
							ApplicantId = applicant.Id,
							PositionId = applicant.JobId,
							HireDate = DateTime.Now
						};
						if (Enum.TryParse(status, out HireStatus parsedStatus))
						{
							hired.HireStatus = parsedStatus;
						}
						await _currentHiresService.AddAsync(hired);
					}
					await _applicantService.UpdateAsync(id, status);
					return RedirectToAction("JobApplicantsOverview");
				}
				else
				{
					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return BadRequest("An error occured when updating the status of applicant.");
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
        /// this will handle the application form process
        /// </summary>
        /// <param name="model">the viewmodel which containes the 
        /// required models for creation and iform file for file reading</param>
        /// <returns>the view corresponding to succes or failure of applicant creation</returns>
        [HttpPost]
        public async Task<IActionResult> ApplicationFormProcess(ApplicationFormViewModel model, [Required] IFormFile resumeFile, IFormFile? photo)
        {
            try
            {
                ModelState.Clear();
                TryValidateModel(model);

                if (ModelState.IsValid)
                {
                    // Use the service method to handle the logic
                    var isApplicantAdded = await _applicantService.AddApplicantAsync(model, resumeFile, photo);

                    if (isApplicantAdded)
                    {
                        // Applicant was successfully added
                        return RedirectToAction("TrackApplication", new { from = "application" });
                    }
                    else
                    {
                        // Handle the error case here if needed
                        ViewBag.ErrorMessage = "Failed to add the applicant.";
                        return View("ApplicationForm", model);
                    }
                }

                ModelState.Clear();
                return View("ApplicationForm");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return BadRequest("An error occured when trying to process application form.");
            }
        }

    }
}