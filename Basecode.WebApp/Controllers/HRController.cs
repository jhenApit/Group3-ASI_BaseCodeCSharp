using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Identity;
using Basecode.Data.ViewModels;
using Basecode.Data.Models;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Dtos.Interviewers;

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
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterReferencesService;
        private readonly IFileService _fileService;
        private readonly IErrorHandling _errorHandling; 
        private readonly UserManager<IdentityUser> _userManager;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISendEmailService _sendEmailService;
        private readonly IMapper _mapper;
        private readonly IMeetingLinkService _meetingLinkService;

        public HRController(
            IHrEmployeeService service,
            IFileService fileService,
            IJobPostingsService jobPostingsService, 
            IErrorHandling errorHandling, 
            UserManager<IdentityUser> userManager, 
            IApplicantService applicantService,
            ICurrentHiresService currentHiresService,
            IInterviewsService interviewsService,
            IAddressService addressService,
            ICharacterReferencesService characterReferencesService,
            IInterviewersService interviewersService,
            ISendEmailService sendEmailService,
            IMapper mapper, IMeetingLinkService meetingLinkService
            )
        {
            _addressService = addressService;
            _service = service;
            _fileService = fileService;
            _jobPostingsService = jobPostingsService;
            _errorHandling = errorHandling;
            _userManager = userManager;
            _applicantService = applicantService;
            _currentHiresService = currentHiresService;
            _interviewsService = interviewsService;
            _interviewersService = interviewersService;
            _characterReferencesService = characterReferencesService;
        }



        public async Task<IActionResult> AdminDashboard()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var jobs = await _jobPostingsService.RetrieveAllAsync();
                var employees = await _currentHiresService.RetrieveAllAsync();
                var interviews = await _interviewsService.RetrieveAllAsync();

                DashboardView model = new ()
                {
                    User = await _service.GetByUserIdAsync(user),
                    JobCount = jobs.Count(),
                    Candidates = await _applicantService.RetrieveAllAsync(),
                    EmployeeCount = employees.Count(),
                    Interviews = interviews.OrderBy(x => x.InterviewDate).Take(6).ToList()
                };
                _logger.Info("Dashboard data retrieved");
                return View(model);
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Failed to load data");
                return BadRequest("An error occured when loading dashboard");
            }
        }


        /// <summary>
        /// Displays the list of job posts.
        /// </summary>
        /// <returns>The view containing the job post list.</returns>
        public async Task<IActionResult> JobPostList()
        {
            try
            {
                var data = await _jobPostingsService.RetrieveAllAsync();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve jobpost list");
                return BadRequest("An error occured when retrieving job post list.");
            }
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
            try
            {
                var jobPosting = await _jobPostingsService.GetByIdAsync(id);
                var loggedUser = await _userManager.GetUserAsync(User);

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
            catch (Exception e)
            {
                _logger.Error(e, "Failed to update jobpost");
                return BadRequest("An error occured when trying to update job");
            }


        }

        /// <summary>
        /// Displays the details of a specific job post.
        /// </summary>
        /// <returns>The view containing the job post details.</returns>
        public async Task<IActionResult> ViewJobPost(int id)
        {
            try
            {
                var job = await _jobPostingsService.GetByIdAsync(id);
                return View(job);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve jobpost");
                return BadRequest("An error occured when retrieving the job post to view.");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobPostingsCreationDto jobPostingsCreationDto)
        {
            try
            {
                var loggedUser = await _userManager.GetUserAsync(User);
                await _jobPostingsService.AddAsync(jobPostingsCreationDto, loggedUser);
                return RedirectToAction("JobPostList");
            }
            catch (Exception e)
            {
                ModelState.Clear();
                _logger.Error(e, "Failed to add jobpost");
                return BadRequest("An error occured when trying to add a job.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(JobPostingsUpdationDto jobPostingsUpdationDto)
        {
            try
            {
                // Get AspNetUser Data
                var loggedUser = await _userManager.GetUserAsync(User);
                await _jobPostingsService.UpdateAsync(jobPostingsUpdationDto, loggedUser);
                return RedirectToAction("JobPostList");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to update jobpost");
                return BadRequest("An error occured when trying to update a job.");
            }

        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                var job = await _jobPostingsService.GetByIdAsync(id);
                await _jobPostingsService.PermaDeleteAsync(id);
                return RedirectToAction("JobPostList");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to delete jobpost");
                return BadRequest("An error occured when trying to delete a job.");
            }

        }
        /// <summary>
        /// Displays the details of a applicant's application.
        /// </summary>
        /// <returns>The view containing the application details.</returns>
        public async Task<IActionResult> ApplicantDetail(int id)
        {
            try
            {
                var applicant = await _applicantService.GetByIdAsync(id);
                var address = await _addressService.GetByApplicantIdAsync(applicant.Id);
                var characterReferences = await _characterReferencesService.GetByApplicantIdAsync(applicant.Id);
                var interviews = await _interviewsService.GetByApplicantIdAsync(applicant.Id);
                var job = await _jobPostingsService.GetByIdAsync(applicant.JobId);
                var applicantDetailViewModel = new ApplicantDetailViewModel
                {
                    Applicant = applicant,
                    Address = address,
                    JobPosting = job,
                    CharacterReferences = characterReferences,
                    Interviews = interviews,
                };

                string imreBase64Data = Convert.ToBase64String(applicantDetailViewModel.Applicant.Photo);
                string imgDataURL = $"data:image/png;base64,{imreBase64Data}";

                ViewBag.ImageData = imgDataURL;

                _fileService.DeleteFile("wwwroot/applicants/resume/resume.pdf");
                _fileService.SaveFile("wwwroot/applicants/resume/resume.pdf", applicantDetailViewModel.Applicant.Resume);

                ViewBag.ResumeData = File("wwwroot/applicants/resume/resume.pdf", "application/pdf");

                return View(applicantDetailViewModel);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve applicant details");
                return BadRequest("An error occured when retrieving applicant detail");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateJobPosting(JobPostingsUpdationDto model)
        {
            try
            {
                // Retrieve the currently logged-in user
                var loggedInUser = await _userManager.GetUserAsync(User);
                await _jobPostingsService.UpdateAsync(model, loggedInUser);
                return RedirectToAction("JobPostList");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to retrieve update job post");
                return BadRequest("An error occured when updating a job post list.");
            }

        }

        /// <summary>
        /// Allows HR to view job applicants
        /// </summary>
        /// <returns>Redirect to Job Applicant Overview Page</returns>
        public async Task<IActionResult> JobApplicantsOverview()
        {
            try
            {
                var applicants = await _applicantService.RetrieveAllAsync();
                var jobs = await _jobPostingsService.RetrieveAllAsync();

                var jobApplicantsOverviewModel = new JobApplicantOverviewModel
                {
                    applicants = applicants,
                    jobPostings = jobs
                };

                return View(jobApplicantsOverviewModel);
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to retrieve update overview data");
                return BadRequest("An error occurred while retrieving the Job Applicant Overview.");
            }
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

        /// <summary>
        /// Allows HR to view new hires
        /// </summary>
        /// <returns>Redirect to View Applicants Page</returns>
        public async Task<IActionResult> NewHires()
        {
            try
            {
                var currentHiresList = await _currentHiresService.RetrieveAllAsync();
                var jobPostingsList = await _jobPostingsService.RetrieveAllAsync();

                var newHiresModel = new NewHiresViewModel
                {
                    CurrentHires = currentHiresList,
                    jobPostings = jobPostingsList
                };

                return View(newHiresModel);
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to retrieve retrieve current hires data");
                return BadRequest("An error occurred while retriving New Hires.");
            }
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
                    Interviewers = new InterviewersCreationDto(),
                    InterviewersList = await _interviewersService.RetrieveAllAsync(),
                    InterviewsList = (await _interviewsService.RetrieveAllAsync())
                    .OrderBy(x => x.InterviewDate)
                    .ToList()
                };
                return View(viewModel);
            } 
            catch(Exception e)
            {
                _logger.Error(e, "Failed to retrieve interviews data");
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
                var viewModel = new InterviewsFormViewModel
                {
                    Interviewer = await _interviewersService.GetByIdAsync(id),
                    ApplicantsList = await _applicantService.RetrieveAllAsync(),
                };
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to load view");
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

                if (await _interviewsService.IsTimeRangeOverlappingAsync(createInterview))
                {
                    TempData["IsOverlap"] = true;
                    return RedirectToAction("CreateInterview", new { id = interview.InterviewerId });
                }
                await _interviewsService.AddAsync(createInterview);

                var interviewSched = _mapper.Map<Interviews>(createInterview);
                var applicant = await _applicantService.GetByIdAsync(interviewSched.ApplicantId);
                
                await _sendEmailService.SendSetInterviewScheduleEmail(interviewSched, applicant!);
                
                return RedirectToAction("Interviews");
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to add new interview");
                return BadRequest(e.Message + " Error occurred while adding a new interview");
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
                Interviews interviews = await _interviewsService.GetByIdAsync(id);
                Console.WriteLine(interviews);
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
            catch (Exception e)
            {
                _logger.Error(e, "Failed to load view");
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

                if (await _interviewsService.IsTimeRangeOverlappingAsync(updateInterview))
                {
                    TempData["IsOverlapUpdate"] = true;
                    return RedirectToAction("EditInterview", new { id = interview.Id });
                }

                await _interviewsService.UpdateAsync(updateInterview);
                return RedirectToAction("Interviews");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to update interview");
                return BadRequest("Error occurred while updating interview");
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
                _logger.Error("Failed to delete interview");
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
        public async Task<IActionResult> AddInterviewer(InterviewsViewModel interviewsViewModel)
        {
            try
            {
                await _interviewersService.AddAsync(interviewsViewModel.Interviewers);
                return RedirectToAction("Interviews");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to add interviewer");
                return BadRequest(ex.Message + " An error happend while adding an interviewer.");
            }
        }

        /// <summary>
        /// Deletes interviewer from the database
        /// </summary>
        /// <param name="id">Interviewer Id</param>
        /// <returns>Redirects to the Interviews Page</returns>
        public async Task<IActionResult> DeleteInterviewer(int id)
        {
            try
            {
                Console.WriteLine("Heere" + id);
                await _interviewersService.DeleteAsync(id);
                return RedirectToAction("Interviews");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to remove interviewer");
                return BadRequest("An error happend while removing an interviewer.");
            }
        }
        #endregion
    }
}