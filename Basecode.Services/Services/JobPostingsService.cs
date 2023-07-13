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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LogContent _logContent = new();
        public JobPostingsService(IJobPostingsRepository repository, /*UserManager<HrEmployee> userManager,*/ IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //_userManager = userManager;
            _contextAccessor = httpContextAccessor;
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
            JobPostingsModel.UpdatedById = 1; // i"ll change this later when the log in part is implemented already
            JobPostingsModel.UpdatedTime = DateTime.Now;

            _repository.Update(JobPostingsModel);
        }
        /// <summary>
        /// This is suppose to get the id of the current logged in.
        /// This is a sample method and is not tested yet
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetLoggedInUserId()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _userManager.FindByIdAsync(userId);
            //return user != null ? int.Parse(userId) : 0;
            return int.Parse(userId);
        }

        public void SemiDelete(int id)
        {
            var job = _repository.GetById(id);
            job.IsDeleted = true;
            job.UpdatedById = 1; // i"ll change this later when the log in part is implemented already
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
