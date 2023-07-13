using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data;
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
        private readonly LogContent _logContent = new();

        public JobPostingsService(IJobPostingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
