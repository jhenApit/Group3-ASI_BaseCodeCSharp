using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data;
using Basecode.Services.Interfaces;
using Basecode.Data.Dtos.JobPostings;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Basecode.Services.Services
{
    public class JobPostingsService : ErrorHandling, IJobPostingsService
    {
        private readonly IJobPostingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly LogContent _logContent = new();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobPostingsService(IJobPostingsRepository repository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<JobPostings> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(JobPostingsCreationDto jobPostingsDto)
        {
            var JobPostingsModel = _mapper.Map<JobPostings>(jobPostingsDto);
            JobPostingsModel.CreatedTime = DateTime.Now;
            JobPostingsModel.IsDeleted = false;

            _repository.Add(JobPostingsModel);
        }

        public JobPostings? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(JobPostingsUpdationDto JobPostings)
        {
            var JobPostingsModel = _mapper.Map<JobPostings>(JobPostings);
            JobPostingsModel.UpdatedById = GetLoggedInUserId().Result;
            JobPostingsModel.UpdatedTime = DateTime.Now;

            _repository.Update(JobPostingsModel);
        }
        /// <summary>
        /// This is a sample method and is not tested yet
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetLoggedInUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? int.Parse(userId) : 0;
        }

        public void SemiDelete(int id)
        {
            var job = _repository.GetById(id);
            job.IsDeleted = true;
            job.UpdatedById = GetLoggedInUserId().Result;
            job.UpdatedTime = DateTime.Now;
            _repository.SemiDelete(job);
        }

        public void PermaDelete(int id)
        {
            _repository.PermaDelete(id);
        }

        public JobPostings? GetByName(string name)
        {
            return _repository.GetByName(name);
        }

    }
}
