using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.ViewModels;
using Basecode.Data.Models;
using Basecode.Data.Dtos.Interviews;
using static Basecode.Data.Enums.Enums;

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
        private readonly IInterviewersService _interviewersService;
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
            IInterviewsService interviewsService,
            IInterviewersService interviewersService
            )
        {
            _service = service;
            _jobPostingsService = jobPostingsService;
            _errorHandling = errorHandling;
            _userManager = userManager;
            _applicantService = applicantService;
            _currentHiresService = currentHiresService;
            _interviewsService = interviewsService;
            _interviewersService = interviewersService;
        }



        public async Task<IActionResult> AdminDashboard()
        {
            var user = _userManager.GetUserId(User);
            var jobs = await _jobPostingsService.RetrieveAllAsync();
            var employees = await _currentHiresService.RetrieveAllAsync();
            var interviews = await _interviewsService.RetrieveAllAsync();

			DashboardView model = new DashboardView
            {
                User = await _service.GetByUserIdAsync(user),
                JobCount = jobs.Count(),
                Candidates = await _applicantService.RetrieveAllAsync(),
                EmployeeCount = employees.Count(),
                Interviews = interviews.OrderBy(x => x.InterviewDate).Take(6).ToList()
            };
            return View(model);
        }


        /// <summary>
        /// Displays the list of job posts.
        /// </summary>
        /// <returns>The view containing the job post list.</returns>
        public async Task<IActionResult> JobPostList()
        {
            var data = await _jobPostingsService.RetrieveAllAsync();
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
            var jobPosting = await _jobPostingsService.GetByIdAsync(id);
            var loggedUser = await _userManager.GetUserAsync(User);

            if (jobPosting == null)
            {
                return RedirectToAction("JobPostList");
            }
			JobPostingsUpdationDto jobPostingDto = new JobPostingsUpdationDto
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
        public async Task<IActionResult> ViewJobPost(int id)
        {
            var job = await _jobPostingsService.GetByIdAsync(id);
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
				
                //logger to be implemented
                //var data = _jobPostingsService.CreateJobPosting(jobPostingsCreationDto);
                
                //if (!data.Result)
                //{
                //    //_logger.Error(_errorHandling.SetLog(data));
                //    ViewBag.ErrorMessage = data.Message;
                //    return View(jobPostingsCreationDto);
                //}
                await _jobPostingsService.AddAsync(jobPostingsCreationDto);
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
                
                //logger to be implemented
                //var data = _jobPostingsService.UpdateJobPosting(jobPostingsUpdationDto);
                
                //if (!data.Result)
                //{
                //    //_logger.Error(_errorHandling.SetLog(data));
                //    ViewBag.ErrorMessage = data.Message;
                //    return View(jobPostingsUpdationDto);
                //}
                await _jobPostingsService.UpdateAsync(jobPostingsUpdationDto);
                return RedirectToAction("JobPostList");
            }
            return View("EditJobPost", jobPostingsUpdationDto);
        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobPostingsService.GetByIdAsync(id);
            if (job != null)
            {
                await _jobPostingsService.PermaDeleteAsync(id);
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
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (loggedInUser != null)
                {
					await _jobPostingsService.UpdateAsync(model);
                    return RedirectToAction("JobPostList");
                }
            }

            // If the model is not valid or the user is not logged in, return the EditJobPosting view with the appropriate error
            return View("EditJobPosting", model);
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

        #region Interviews

        /// <summary>
        /// View List of Upcoming Interviews
        /// </summary>
        /// <returns>Redirect to Interview Page</returns>
        public async Task<IActionResult> Interviews()
        {
            try
            {
                var interviews = await _interviewsService.RetrieveAllAsync();

				var viewModel = new InterviewsViewModel
                {
                    Interviewers = new Interviewers(),
                    InterviewersList = await _interviewersService.RetrieveAllAsync(),
                    InterviewsList = interviews.OrderBy(x => x.InterviewDate).ToList()
                };
                return View(viewModel);
            } 
            catch(Exception)
            {
                return BadRequest("An error occurred while retriving Interviews.");
            }
        }

        /// <summary>
        /// Create a new interview
        /// </summary>
        /// <returns>Redirect to Create Interview Page</returns>
        public async Task<IActionResult> CreateInterview(int id)
        {
            try
            {
                var existingIntervewer = await _interviewersService.GetByIdAsync(id);

				if (existingIntervewer != null)
                {
                    var viewModel = new InterviewsFormViewModel
                    {
                        Interviewer = await _interviewersService.GetByIdAsync(id),
                        ApplicantsList = await _applicantService.RetrieveAllAsync(),
                    };
                    return View(viewModel);
                }
                return RedirectToAction("Interviews");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retriving this page.");
            }
        }

        /// <summary>
        /// Add new interview to the database
        /// </summary>
        /// <param name="interview">Data</param>
        /// <returns>Redirect to the interviews page</returns>
        [HttpPost]
        public async Task<IActionResult> AddInterview(InterviewsFormViewModel interview)
        {
            try
            {
                var createInterview = new InterviewsCreationDto
                {
                    ApplicantId = interview.ApplicantId,
                    InterviewerId = interview.InterviewerId,
                    InterviewType = interview.InterviewType,
                    InterviewDate = interview.InterviewDate,
                    TimeStart = interview.TimeStart,
                    TimeEnd = interview.TimeEnd,
                };
                await _interviewsService.AddAsync(createInterview);
                return RedirectToAction("Interviews");
            }
            catch(Exception)
            {
                return BadRequest("Error occurred while adding a new interview");
            }
        }

        /// <summary>
        /// Edit an interview
        /// </summary>
        /// <returns>Redirect to Edit Interview Page</returns>
        public async Task<IActionResult> EditInterview(int id)
        {
            try
            {
                var interviews = await _interviewsService.GetByIdAsync(id);
                Console.WriteLine(interviews);
                if (interviews != null)
                {
                    var viewModel = new InterviewsFormViewModel
                    {
                        Interviewer = await _interviewersService.GetByIdAsync(interviews.InterviewerId),
                        ApplicantsList = await _applicantService.RetrieveAllAsync(),
                        ApplicantId = interviews.ApplicantId,
                        InterviewerId = interviews.InterviewerId,
                        InterviewType = interviews.InterviewType,
                        InterviewDate = interviews.InterviewDate,
                        TimeStart = interviews.TimeStart,
                        TimeEnd = interviews.TimeEnd
                    };
                    return View(viewModel);
                }
                return RedirectToAction("Interviews");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retriving this page.");
            }
        }

        /// <summary>
        /// Update an interview in the database
        /// </summary>
        /// <param name="interview">Updated Data</param>
        /// <returns>Redirect to interviews page</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateInterview(InterviewsFormViewModel interview)
        {
            try
            {
                var updateInterview = new InterviewsUpdationDto
                {
                    Id = interview.Id,
                    ApplicantId = interview.ApplicantId,
                    InterviewerId = interview.InterviewerId,
                    InterviewType = interview.InterviewType,
                    InterviewDate = interview.InterviewDate,
                    TimeStart = interview.TimeStart,
                    TimeEnd = interview.TimeEnd,
                };
                await _interviewsService.UpdateAsync(updateInterview);
                return RedirectToAction("Interviews");
            }
            catch (Exception)
            {
                return BadRequest("Error occurred while adding a new interview");
            }
        }

        /// <summary>
        /// Delete an interview
        /// </summary>
        /// <param name="id">Interview Id</param>
        /// <returns>Redirect to interviews page</returns>
        public async Task<IActionResult> DeleteInterview(int id)
        {
            try
            {
                await _interviewsService.DeleteAsync(id);
                return RedirectToAction("Interviews");
            }
            catch
            {
                return BadRequest("Delete Failed");
            }
        }

        #endregion

        #region Interviewers

        /// <summary>
        /// Adds interviewers to the database
        /// </summary>
        /// <param name="interviewers">Data</param>
        /// <returns>Redirects to the Interviews Page</returns>
        [HttpPost]
        public async Task<IActionResult> AddInterviewer(Interviewers interviewers)
        {
            try
            {
                await _interviewersService.AddAsync(interviewers);
                return RedirectToAction("Interviews");
            }
            catch (Exception)
            {
                return BadRequest("An error happend while adding an interviewer.");
            }
        }

        #endregion
    }
}