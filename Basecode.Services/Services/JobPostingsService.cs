using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Basecode.Services.Services
{
    public class JobPostingsService : IJobPostingsService
    {
        private readonly IJobPostingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public JobPostingsService(IJobPostingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all job postings.
        /// </summary>
        /// <returns>A list of job postings.</returns>
        public async Task<List<JobPostings>> RetrieveAllAsync()
        {
            try
            {
                var jobPost = await _repository.RetrieveAllAsync();
                return jobPost.ToList();
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new job posting.
        /// </summary>
        /// <param name="jobPostingsDto">The DTO object containing job posting details.</param>
        public async Task AddAsync(JobPostingsCreationDto jobPostingsDto, IdentityUser loggedUser)
        {
            try
            {
                jobPostingsDto.CreatedBy = loggedUser.UserName;
                jobPostingsDto.Qualifications = string.Join(", ", jobPostingsDto.QualificationList);
                jobPostingsDto.Responsibilities = string.Join(", ", jobPostingsDto.ResponsibilityList);
                var JobPostingsModel = _mapper.Map<JobPostings>(jobPostingsDto);
                JobPostingsModel.CreatedTime = DateTime.Now;
                JobPostingsModel.IsDeleted = false;
                JobPostingsModel.CreatedBy = jobPostingsDto.CreatedBy;
                JobPostingsModel.UpdatedTime = null;
                JobPostingsModel.UpdatedBy = null;

                await _repository.AddAsync(JobPostingsModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a job posting by its ID.
        /// </summary>
        /// <param name="id">The ID of the job posting.</param>
        /// <returns>The job posting with the specified ID, or null if not found.</returns>
        public async Task<JobPostings?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates a job posting.
        /// </summary>
        /// <param name="JobPostings">The DTO object containing updated job posting details.</param>
        public async Task UpdateAsync(JobPostingsUpdationDto jobPostings, IdentityUser loggedUser)
        {
            try
            {
                jobPostings.UpdatedBy = loggedUser.UserName;
                jobPostings.Qualifications = string.Join(", ", jobPostings.QualificationList);
                jobPostings.Responsibilities = string.Join(", ", jobPostings.ResponsibilityList);
                var JobPostingsModel = _mapper.Map<JobPostings>(jobPostings);
                JobPostingsModel.UpdatedTime = DateTime.Now;
                JobPostingsModel.UpdatedBy = jobPostings.UpdatedBy;
                await _repository.UpdateAsync(JobPostingsModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Sets a job posting as deleted but retains it in the database.
        /// </summary>
        /// <param name="id">The ID of the job posting to semi-delete.</param>
        public async Task SemiDeleteAsync(int id)
        {
            try
            {
                var job = await _repository.GetByIdAsync(id);
                job.IsDeleted = true;
                job.UpdatedTime = DateTime.Now;
                await _repository.SemiDeleteAsync(job);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > SemiDeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Permanently deletes a job posting from the database.
        /// </summary>
        /// <param name="id">The ID of the job posting to permanently delete.</param>
        public async Task PermaDeleteAsync(int id)
        {
            try
            {
                await _repository.PermaDeleteAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > PermaDeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a job posting by its name.
        /// </summary>
        /// <param name="name">The name of the job posting.</param>
        /// <returns>The job posting with the specified name, or null if not found.</returns>
        public async Task<JobPostings?> GetByNameAsync(string name)
        {
            try
            {
                return await _repository.GetByNameAsync(name);
            }
            catch (System.Exception ex)
            {
                _logger.Error("JobPostingsService > GetByNameAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This logs the errors the user encounters when creating a job post
        /// </summary>
        /// <param name="jobPostingCreationDto"> the dto passed to create a job post</param>
        /// <returns>the log content containing the result, errorcode, message </returns>

        public async Task<LogContent> CreateJobPosting(JobPostingsCreationDto jobPostingCreationDto)
        {
            var job = await GetByNameAsync(jobPostingCreationDto.Name);
            if (job != null)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "400";
                _logContent.Message = "Job already posted!";
            }
            else
            {
                _logContent.Result = true;
            }

            return _logContent;
        }

        public async Task<LogContent> UpdateJobPosting(JobPostingsUpdationDto jobPostings)
        {
            var job = await GetByIdAsync(jobPostings.Id);
            if (job != null)
            {
                // Check if the new name is different from the current name
                if (job.Name != jobPostings.Name)
                {
                    // Check if the new name already exists in the table
                    var existingJob = await GetByNameAsync(jobPostings.Name);
                    if (existingJob != null)
                    {
                        // Another job with the same name already exists
                        _logContent.Result = false;
                        _logContent.ErrorCode = "400. Edit Failed!";
                        _logContent.Message = "Position Name already exists.";
                        return _logContent;
                    }
                    _logContent.Result = true;
                    return _logContent;
                }
                //if job exists and job name entered is the same as the current name
                _logContent.Result = true;
                return _logContent;
            }
            else
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "400. Edit Failed!";
                _logContent.Message = "Job with ID doesn't exist.";
                //return _logContent;
            }
            return _logContent;
        }
    }
}
