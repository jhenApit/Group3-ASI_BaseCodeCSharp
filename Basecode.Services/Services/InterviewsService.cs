using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.EntityFrameworkCore;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class InterviewsService : ErrorHandling, IInterviewsService
    {
        private readonly IInterviewsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public InterviewsService(IInterviewsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<Interviews>> RetrieveAllAsync()
        {
            var interviews = await _repository.RetrieveAllAsync();
            return interviews.ToList();
        }

        public async Task AddAsync(InterviewsCreationDto interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(interviews);
            await _repository.AddAsync(InterviewsModel);
        }

        public async Task<Interviews?> GetByIdAsync(int id)
        {
            return await  _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(InterviewsUpdationDto Interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(Interviews);
            Console.WriteLine("Services" + InterviewsModel.InterviewDate);
            await _repository.UpdateAsync(InterviewsModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Interviews?> GetByApplicantIdAsync(int applicantId)
        {
            return await _repository.GetByApplicantIdAsync(applicantId);
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewerAndDate(int interviewerId, DateTime interviewDate)
        {
            return _repository.GetInterviewsByInterviewerAndDate(interviewerId, interviewDate);
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewer(int interviewerId)
        {
            return _repository.GetInterviewsByInterviewer(interviewerId);
        }

        public IEnumerable<Interviews> GetInterviewsByApplicant(int applicantId)
        {
            return _repository.GetInterviewsByApplicant(applicantId);
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewerAndDate(int interviewerId, DateTime interviewDate)
        {
            return _repository.GetInterviewsByInterviewerAndDate(interviewerId, interviewDate);
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewer(int interviewerId)
        {
            return _repository.GetInterviewsByInterviewer(interviewerId);
        }

        public IEnumerable<Interviews> GetInterviewsByApplicant(int applicantId)
        {
            return _repository.GetInterviewsByApplicant(applicantId);
        }

    }

}
