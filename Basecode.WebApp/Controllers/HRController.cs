using Basecode.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Basecode.WebApp.Controllers
{
    [Authorize(Roles = "hr")]
    public class HRController : Controller
    {
        /// <summary>
        /// Displays the list of job posts.
        /// </summary>
        /// <returns>The view containing the job post list.</returns>
        private readonly UserManager<HrEmployee> _userManager;
        private readonly IJobPostingsService _jobpostingService;

        public HRController() { }
        /*public HRController(UserManager<HrEmployee> userManager, IJobPostingsService jobposting)
        {
            _userManager = userManager;
            _jobpostingService = jobposting;
        }*/
        public IActionResult JobPostList()
        {
            return View();
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
        public IActionResult EditJobPost()
        {
            return View();
        }

        /// <summary>
        /// Displays the details of a specific job post.
        /// </summary>
        /// <returns>The view containing the job post details.</returns>
        public IActionResult ViewJobPost()
        {
            return View();
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