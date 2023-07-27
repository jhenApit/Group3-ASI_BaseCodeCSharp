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
    public class JobPostingsService : IJobPostingsService
    {
        private readonly IJobPostingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        public JobPostingsService(IJobPostingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            JobPostingsModel.CreatedBy = jobPostingsDto.CreatedBy;
            JobPostingsModel.UpdatedTime = null;
            JobPostingsModel.UpdatedBy = null;

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
            JobPostingsModel.UpdatedTime = DateTime.Now;
            JobPostingsModel.UpdatedBy = JobPostings.UpdatedBy;

            _repository.Update(JobPostingsModel);
        }

        /// <summary>
        /// Sets a job posting as deleted but retains it in the database.
        /// </summary>
        /// <param name="id">The ID of the job posting to semi-delete.</param>
        public void SemiDelete(int id)
        {
            var job = _repository.GetById(id);
            job.IsDeleted = true;
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

        /// <summary>
        /// This logs the errors the user encounters when creating a job post
        /// </summary>
        /// <param name="jobPostingCreationDto"> the dto passed to create a job post</param>
        /// <returns>the log content containing the result, errorcode, message </returns>

        public LogContent CreateJobPosting(JobPostingsCreationDto jobPostingCreationDto)
        {
            JobPostings job = GetByName(jobPostingCreationDto.Name);
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

        public LogContent UpdateJobPosting(JobPostingsUpdationDto jobPostings)
        {
            var job = GetById(jobPostings.Id);
            if (job != null)
            {
                // Check if the new name is different from the current name
                if (job.Name != jobPostings.Name)
                {
                    // Check if the new name already exists in the table
                    var existingJob = GetByName(jobPostings.Name);
                    if (existingJob != null)
                    {
                        // Another job with the same name already exists
                        _logContent.Result = false;
                        _logContent.ErrorCode = "400. Edit Failed!";
                        _logContent.Message = "Position Name already exists.";
                        return _logContent;
                    }
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
                return _logContent;
            }
        }

    }
}
