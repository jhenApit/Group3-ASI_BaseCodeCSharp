//using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Basecode.Services.Utils;

namespace Basecode.Services.Services
{
    public class JobPostingsService : ErrorHandling, IJobPostingsService
    {
        private readonly IJobPostingsRepository _repository;
        private readonly IMapper _mapper;
        //private readonly UserManager<HrEmployee> _userManager
        private readonly LogContent _logContent = new();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobPostingsService(IJobPostingsRepository repository, /*UserManager<HrEmployee> userManager,*/ IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //_userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Retrieves all job postings.
        /// </summary>
        /// <returns>A list of job postings.</returns>
        public List<JobPostings> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new job posting.
        /// </summary>
        /// <param name="jobPostingsDto">The DTO object containing job posting details.</param>
        public void Add(JobPostingsCreationDto jobPostingsDto)
        {
            var JobPostingsModel = _mapper.Map<JobPostings>(jobPostingsDto);
            JobPostingsModel.CreatedTime = DateTime.Now;
            JobPostingsModel.IsDeleted = false;

            _repository.Add(JobPostingsModel);
        }

        /// <summary>
        /// Retrieves a job posting by its ID.
        /// </summary>
        /// <param name="id">The ID of the job posting.</param>
        /// <returns>The job posting with the specified ID, or null if not found.</returns>
        public JobPostings? GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Updates a job posting.
        /// </summary>
        /// <param name="JobPostings">The DTO object containing updated job posting details.</param>
        public void Update(JobPostingsUpdationDto JobPostings)
        {
            var JobPostingsModel = _mapper.Map<JobPostings>(JobPostings);
            JobPostingsModel.UpdatedById = GetLoggedInUserId().Result;
            JobPostingsModel.UpdatedTime = DateTime.Now;

            _repository.Update(JobPostingsModel);
        }

        /// <summary>
        /// Gets the ID of the currently logged-in user.
        /// </summary>
        /// <returns>The ID of the currently logged-in user.</returns>
        /// <remarks>
        /// This is a sample method and is not tested yet.
        /// </remarks>
        public async Task<int> GetLoggedInUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }

        /// <summary>
        /// Sets a job posting as deleted but retains it in the database.
        /// </summary>
        /// <param name="id">The ID of the job posting to semi-delete.</param>
        public void SemiDelete(int id)
        {
            var job = _repository.GetById(id);
            job.IsDeleted = true;
            job.UpdatedById = GetLoggedInUserId().Result;
            job.UpdatedTime = DateTime.Now;
            _repository.SemiDelete(job);
        }

        /// <summary>
        /// Permanently deletes a job posting from the database.
        /// </summary>
        /// <param name="id">The ID of the job posting to permanently delete.</param>
        public void PermaDelete(int id)
        {
            _repository.PermaDelete(id);
        }

        /// <summary>
        /// Retrieves a job posting by its name.
        /// </summary>
        /// <param name="name">The name of the job posting.</param>
        /// <returns>The job posting with the specified name, or null if not found.</returns>
        public JobPostings? GetByName(string name)
        {
            return _repository.GetByName(name);
        }

    }
}
