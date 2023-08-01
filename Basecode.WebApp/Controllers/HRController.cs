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
                    //_logger.Error(_errorHandling.SetLog(data));
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
                    //_logger.Error(_errorHandling.SetLog(data));
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
                    _jobPostingsService.Update(model);
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
        public IActionResult Interviews()
        {
            try
            {
                var viewModel = new InterviewsViewModel
                {
                    Interviewers = new Interviewers(),
                    InterviewersList = _interviewersService.RetrieveAll(),
                    InterviewsList = _interviewsService.RetrieveAll().OrderBy(x => x.InterviewDate).ToList()
                };
                return View(viewModel);
            } 
            catch(Exception)
            {
                return BadRequest("An error occurred while retriving Interviews.");
            }
        }

        /// <summary>
        /// Allows HR to create a new interview entry
        /// </summary>
        /// <returns>Redirect to Create Interview Page</returns>
        public IActionResult CreateInterview(int id)
        {
            try
            {
                if(_interviewersService.GetById(id) != null)
                {
                    var viewModel = new InterviewsFormViewModel
                    {
                        Interviewer = _interviewersService.GetById(id),
                        ApplicantsList = _applicantService.RetrieveAll(),
                    };
                    return View(viewModel);
                }
                return RedirectToAction("Interview");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retriving this page.");
            }
        }

        [HttpPost]
        public IActionResult AddInterview(InterviewsFormViewModel interview)
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
                _interviewsService.Add(createInterview);
                return RedirectToAction("Interviews");
            }
            catch(Exception)
            {
                return BadRequest("Error occurred while adding a new interview");
            }
        }

        /// <summary>
        /// Allows HR to edit an interview
        /// </summary>
        /// <returns>Redirect to Edit Interview Page</returns>
        public IActionResult EditInterview(int id)
        {
            Interviews interviews = _interviewsService.GetById(id);
            try
            {
                Console.WriteLine(interviews);
                if (interviews != null)
                {
                    var viewModel = new InterviewsFormViewModel
                    {
                        Interviewer = _interviewersService.GetById(interviews.InterviewerId),
                        ApplicantsList = _applicantService.RetrieveAll(),
                        ApplicantId = interviews.ApplicantId,
                        InterviewerId = interviews.InterviewerId,
                        InterviewType = interviews.InterviewType,
                        InterviewDate = interviews.InterviewDate,
                        TimeStart = interviews.TimeStart,
                        TimeEnd = interviews.TimeEnd
                    };
                    return View(viewModel);
                }
                return RedirectToAction("Interview");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while retriving this page.");
            }
        }

        [HttpPost]
        public IActionResult UpdateInterview(InterviewsFormViewModel interview)
        {
            Console.WriteLine(interview.ApplicantId);
            Console.WriteLine(interview.InterviewerId);
            Console.WriteLine(interview.InterviewType);
            Console.WriteLine(interview.InterviewDate);
            Console.WriteLine(interview.TimeStart);
            Console.WriteLine(interview.TimeEnd);
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
                _interviewsService.Update(updateInterview);
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
        /// <returns>Redirect to interview page</returns>
        public IActionResult DeleteInterview(int id)
        {
            try
            {
                _interviewsService.Delete(id);
                return RedirectToAction("Interview");
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
        public IActionResult AddInterviewer(Interviewers interviewers)
        {
            try
            {
                _interviewersService.Add(interviewers);
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