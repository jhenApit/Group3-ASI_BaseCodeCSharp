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
            JobPostingsModel.UpdatedById = 1; // i"ll change this later when the log in part is implemented already
            JobPostingsModel.UpdatedTime = DateTime.Now;

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
            job.UpdatedById = 1; // i"ll change this later when the log in part is implemented already
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
