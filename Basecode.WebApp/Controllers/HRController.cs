using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.ViewModels;
using Basecode.Data.Models;
using static Basecode.Data.Enums.Enums;
using Basecode.Services.Services;
using Basecode.WebApp.Models;
using Basecode.Data.Dtos.Applicants;

namespace Basecode.WebApp.Controllers
{
    [Authorize(Roles = "hr,admin")]
    public class HRController : Controller
    {
        private readonly IHrEmployeeService _service;
        private readonly IJobPostingsService _jobPostingsService;
        private readonly IApplicantService _applicantService;
        private readonly ICurrentHiresService _currentHiresService;
        private readonly IInterviewsService _interviewsService;
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterReferencesService;
        private readonly IErrorHandling _errorHandling; 
        private readonly UserManager<IdentityUser> _userManager;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HRController(
            IHrEmployeeService service, 
            IJobPostingsService jobPostingsService, 
            IErrorHandling errorHandling, 
            UserManager<IdentityUser> userManager, 
            IApplicantService applicantService,
            ICurrentHiresService currentHiresService,
            IInterviewsService interviewersService,
            IAddressService addressService,
            ICharacterReferencesService characterReferencesService
            )
        {
            _addressService = addressService;
            _service = service;
            _jobPostingsService = jobPostingsService;
            _applicantService = applicantService;
            _errorHandling = errorHandling;
            _userManager = userManager;
            _applicantService = applicantService;
            _currentHiresService = currentHiresService;
            _interviewsService = interviewersService;
            _characterReferencesService = characterReferencesService;
        }



        public IActionResult AdminDashboard()
        {
            var user = _userManager.GetUserId(User);
            var model = new DashboardView
            {
                User = _service.GetByUserId(user),
                JobCount = _jobPostingsService.RetrieveAll().Count(),
                Candidates = _applicantService.RetrieveAll(),
                EmployeeCount = _currentHiresService.RetrieveAll().Count(),
                Schedules = _interviewsService.RetrieveAll()
            };
            return View(model);
        }


        /// <summary>
        /// Displays the list of job posts.
        /// </summary>
        /// <returns>The view containing the job post list.</returns>
        public IActionResult JobPostList()
        {
            var data = _jobPostingsService.RetrieveAll();
            return View(data);
        }

        /// <summary>
        /// Displays the form to create a new job post.
        /// </summary>
        /// <returns>The view containing the job post creation form.</returns>
        public IActionResult CreateJobPost()
        {
            return View();
        }

        /// <summary>
        /// Displays the form to edit an existing job post.
        /// </summary>
        /// <returns>The view containing the job post edit form.</returns>
        public async Task<IActionResult> EditJobPost(int id)
        {
            var jobPosting = _jobPostingsService.GetById(id);
            var loggedUser = await _userManager.GetUserAsync(User);

            if (jobPosting == null)
            {
                // Handle the case where the job posting is not found, for example, redirect to an error page or show an error message
                return RedirectToAction("JobPostList");
            }
            var jobPostingDto = new JobPostingsUpdationDto
            {
                Name = jobPosting.Name,
                Description = jobPosting.Description,
                Qualifications = jobPosting.Qualifications,
                Responsibilities = jobPosting.Responsibilities,
                WorkSetup = jobPosting.WorkSetup,
                JobStatus = jobPosting.JobStatus,
                Hours = jobPosting.Hours,
                EmploymentType = jobPosting.EmploymentType,
                UpdatedBy = loggedUser.UserName
            };
            return View(jobPostingDto);

        }

        /// <summary>
        /// Displays the details of a specific job post.
        /// </summary>
        /// <returns>The view containing the job post details.</returns>
        public IActionResult ViewJobPost(int id)
        {
            var job = _jobPostingsService.GetById(id);
            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobPostingsCreationDto jobPostingsCreationDto)
        {
            if (ModelState.IsValid)
            {
                //Get AspNetUser Data
                var loggedUser = await _userManager.GetUserAsync(User);

                jobPostingsCreationDto.CreatedBy = loggedUser.UserName;
                jobPostingsCreationDto.Qualifications = string.Join(", ", jobPostingsCreationDto.QualificationList);
				jobPostingsCreationDto.Responsibilities = string.Join(", ", jobPostingsCreationDto.ResponsibilityList);
				
                var data = _jobPostingsService.CreateJobPosting(jobPostingsCreationDto);
                
                if (!data.Result)
                {
                    _logger.Error(_errorHandling.SetLog(data));
                    ViewBag.ErrorMessage = data.Message;
                    return View(jobPostingsCreationDto);
                }
                _jobPostingsService.Add(jobPostingsCreationDto);
                return RedirectToAction("JobPostList");
            }
            ModelState.Clear();
			return View("JobPostList", jobPostingsCreationDto);
		}

        [HttpPost]
        public async Task<IActionResult> Update(JobPostingsUpdationDto jobPostingsUpdationDto)
        {
            if (ModelState.IsValid)
            {
                //Get AspNetUser Data
                var loggedUser = await _userManager.GetUserAsync(User);
                
                jobPostingsUpdationDto.UpdatedBy = loggedUser.UserName;
                jobPostingsUpdationDto.Qualifications = string.Join(", ", jobPostingsUpdationDto.QualificationList);
                jobPostingsUpdationDto.Responsibilities = string.Join(", ", jobPostingsUpdationDto.ResponsibilityList);
                
                var data = _jobPostingsService.UpdateJobPosting(jobPostingsUpdationDto);
                
                if (!data.Result)
                {
                    _logger.Error(_errorHandling.SetLog(data));
                    ViewBag.ErrorMessage = data.Message;
                    return View(jobPostingsUpdationDto);
                }
                _jobPostingsService.Update(jobPostingsUpdationDto);
                return RedirectToAction("JobPostList");
            }
            return View("EditJobPost", jobPostingsUpdationDto);
        }

        public IActionResult DeleteJob(int id)
        {
            var job = _jobPostingsService.GetById(id);
            if (job != null)
            {
                _jobPostingsService.PermaDelete(id);
            }
            return RedirectToAction("JobPostList");
        }
        /// <summary>
        /// Displays the details of a applicant's application.
        /// </summary>
        /// <returns>The view containing the application details.</returns>
        public IActionResult ApplicantDetail(int id)
        {
            var applicant = _applicantService.GetById(id);
            var address = _addressService.GetByApplicantId(applicant.Id);
            var characterReferences = _characterReferencesService.GetByApplicantId(applicant.Id);
            var interviews = _interviewsService.GetByApplicantId(applicant.Id);
            var applicantDetailViewModel = new ApplicantDetailViewModel
            {
                Applicant = applicant,
                Address = address,
                CharacterReferences = characterReferences,
                Interviews = interviews
            };
            string imreBase64Data = Convert.ToBase64String(applicant.Photo);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            string resumeBase64Data = Convert.ToBase64String(applicant.Resume);
            string resumeDataURL = string.Format("data:application/pdf;base64,{0}", resumeBase64Data);
            //Passing image data in viewbag to view
            ViewBag.ImageData = imgDataURL;
            //not working
            ViewBag.ResumeData = resumeDataURL;
            return View(applicantDetailViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateJobPosting(JobPostingsUpdationDto model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the currently logged-in user
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (loggedInUser != null)
                {
                    _jobPostingsService.Update(model);
                    return RedirectToAction("JobPostList");
                }
            }

            // If the model is not valid or the user is not logged in, return the EditJobPosting view with the appropriate error
            return View("EditJobPosting", model);
        }

        /// <summary>
        /// View List of Upcoming Interviews
        /// </summary>
        /// <returns>Redirect to Interview Page</returns>
        public IActionResult Interview()
        {
            return View();
        }

        /// <summary>
        /// Allows HR to create a new interview entry
        /// </summary>
        /// <returns>Redirect to Create Interview Page</returns>
        public IActionResult CreateInterview()
        {
            return View();
        }

        /// <summary>
        /// Allows HR to edit an interview
        /// </summary>
        /// <returns>Redirect to Edit Interview Page</returns>
        public IActionResult EditInterview()
        {
            return View();
        }

        /// <summary>
        /// Allows HR to view job applicants
        /// </summary>
        /// <returns>Redirect to Job Applicant Overview Page</returns>
        public IActionResult JobApplicantsOverview()
        {
			var applicants = _applicantService.RetrieveAll();
            var jobPostings = _jobPostingsService.RetrieveAll();

			var jobApplicantsOverviewModel = new JobApplicantOverviewModel
			{
				applicants = applicants,
				jobPostings = jobPostings
			};

			return View(jobApplicantsOverviewModel);
		}

        /// <summary>
        /// Allows HR to view job applicants in a specific job post with different status
        /// </summary>
        /// <returns>Redirect to View Applicants Page</returns>
        public IActionResult ViewApplicants()
        {
            return View();
        }

        /// <summary>
        /// Allows HR to view disqualified job applicants in a specific job post
        /// </summary>
        /// <returns>Redirect to View Disqualified Applicants Page</returns>
        public IActionResult DisqualifiedApplicants()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateApplicantStatus(int id, string status)
        {
			var applicant = _applicantService.GetById(id);
			if (applicant != null)
			{
				_applicantService.Update(id, status);
				var applicants = _applicantService.RetrieveAll();
				var jobPostings = _jobPostingsService.RetrieveAll();

				var jobApplicantsOverviewModel = new JobApplicantOverviewModel
				{
					applicants = applicants,
					jobPostings = jobPostings
				};
				//return View("JobApplicantsOverview",jobApplicantsOverviewModel);
				return RedirectToAction("JobApplicantsOverview");
			}
			else
			{
                return RedirectToAction("Index");
			}

		}
	}
}