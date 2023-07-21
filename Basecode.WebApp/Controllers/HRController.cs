using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Dtos.JobPostings;

namespace Basecode.WebApp.Controllers
{
    [Authorize(Roles = "hr,admin")]
    public class HRController : Controller
    {
        
        private readonly IJobPostingsService _jobpostingService;
        private readonly IErrorHandling _errorHandling;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HRController(IJobPostingsService jobposting, IErrorHandling errorHandling)
        {
            _jobpostingService = jobposting;
            _errorHandling = errorHandling;
        }
        public IActionResult JobPostList()
        {
            var data = _jobpostingService.RetrieveAll();
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
        public IActionResult EditJobPost(int id)
        {
            var jobPosting = _jobpostingService.GetById(id);

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
                EmploymentType = jobPosting.EmploymentType
            };
            return View(jobPostingDto);

        }

        /// <summary>
        /// Displays the details of a specific job post.
        /// </summary>
        /// <returns>The view containing the job post details.</returns>
        public IActionResult ViewJobPost(int id)
        {
            var job = _jobpostingService.GetById(id);
            return View(job);
        }
        [HttpPost]
        public IActionResult Add(JobPostingsCreationDto jobPostingsCreationDto)
        {
            if (ModelState.IsValid)
            {
                var data = _jobpostingService.CreateJobPosting(jobPostingsCreationDto);
                if (!data.Result)
                {
                    _logger.Error(_errorHandling.SetLog(data));
                    ViewBag.ErrorMessage = data.Message;
                    return View(jobPostingsCreationDto);
                }
                _jobpostingService.Add(jobPostingsCreationDto);
                return RedirectToAction("JobPostList");
            }
            ModelState.Clear();
            return View(jobPostingsCreationDto);
        }

        [HttpPost]
        public IActionResult Update(JobPostingsUpdationDto jobPostingsUpdationDto)
        {
            if (ModelState.IsValid)
            {
                var data = _jobpostingService.UpdateJobPosting(jobPostingsUpdationDto);
                if (!data.Result)
                {
                    _logger.Error(_errorHandling.SetLog(data));
                    ViewBag.ErrorMessage = data.Message;
                    return View(jobPostingsUpdationDto);
                }
                _jobpostingService.Update(jobPostingsUpdationDto);
                return RedirectToAction("JobPostList");
            }
            return View("EditJobPost", jobPostingsUpdationDto);
        }

        public IActionResult DeleteJob(int id)
        {
            var job = _jobpostingService.GetById(id);
            if (job != null)
            {
                _jobpostingService.PermaDelete(id);
            }
            return RedirectToAction("JobPostList");
        }
        /// <summary>
        /// Displays the details of a applicant's application.
        /// </summary>
        /// <returns>The view containing the application details.</returns>
        public IActionResult ApplicantDetail()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateJobPosting(JobPostingsUpdationDto model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the currently logged-in user
                var loggedInUser = 1;//await _userManager.GetUserAsync(User);

                if (loggedInUser != null)
                {
                    model.UpdatedById = 1;
                    _jobpostingService.Update(model);
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
            return View();
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
    }
}