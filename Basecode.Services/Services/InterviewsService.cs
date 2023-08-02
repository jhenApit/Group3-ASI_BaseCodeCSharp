using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAndDateAsync(int interviewerId, DateTime interviewDate)
        {
            return await _repository.GetInterviewsByInterviewerAndDateAsync(interviewerId, interviewDate);
        }

        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAsync(int interviewerId)
        {
            return await _repository.GetInterviewsByInterviewerAsync(interviewerId);
        }

        public async Task<IEnumerable<Interviews>> GetInterviewsByApplicantAsync(int applicantId)
        {
            return await _repository.GetInterviewsByApplicantAsync(applicantId);
        }

        public async Task<bool> IsTimeRangeOverlappingAsync(InterviewsCreationDto newInterview)
        {
            var existingInterviews = await GetInterviewsByInterviewerAndDateAsync(newInterview.InterviewerId, newInterview.InterviewDate);

            var newStart = DateTime.ParseExact(newInterview.TimeStart, "h:mm tt", CultureInfo.InvariantCulture);
            var newEnd = DateTime.ParseExact(newInterview.TimeEnd, "h:mm tt", CultureInfo.InvariantCulture);

            foreach (var existingInterview in existingInterviews)
            {
                var existingStart = DateTime.ParseExact(existingInterview.TimeStart, "h:mm tt", CultureInfo.InvariantCulture);
                var existingEnd = DateTime.ParseExact(existingInterview.TimeEnd, "h:mm tt", CultureInfo.InvariantCulture);

                if (newInterview.InterviewDate == existingInterview.InterviewDate)
                {
                    if ((newStart >= existingStart && newStart < existingEnd) ||
                        (newEnd > existingStart && newEnd <= existingEnd) ||
                        (newStart <= existingStart && newEnd >= existingEnd))
                    {
                        return true;
                    }
                }
            }

            var otherInterviewsByInterviewer = await GetInterviewsByInterviewerAsync(newInterview.InterviewerId);
            foreach (var otherInterview in otherInterviewsByInterviewer)
            {
                if (otherInterview.Id != newInterview.Id)
                {
                    var otherStart = DateTime.ParseExact(otherInterview.TimeStart, "h:mm tt", CultureInfo.InvariantCulture);
                    var otherEnd = DateTime.ParseExact(otherInterview.TimeEnd, "h:mm tt", CultureInfo.InvariantCulture);

                    if (newInterview.InterviewDate == otherInterview.InterviewDate)
                    {
                        if ((newStart >= otherStart && newStart < otherEnd) ||
                            (newEnd > otherStart && newEnd <= otherEnd) ||
                            (newStart <= otherStart && newEnd >= otherEnd))
                        {
                            Console.WriteLine(2);
                            return true;
                        }
                    }
                }
            }

            var otherInterviewsByApplicant = await GetInterviewsByApplicantAsync(newInterview.ApplicantId);
            foreach (var otherInterview in otherInterviewsByApplicant)
            {
                if (otherInterview.Id != newInterview.Id)
                {
                    var otherStart = DateTime.ParseExact(otherInterview.TimeStart, "h:mm tt", CultureInfo.InvariantCulture);
                    var otherEnd = DateTime.ParseExact(otherInterview.TimeEnd, "h:mm tt", CultureInfo.InvariantCulture);

                    if (newInterview.InterviewDate == otherInterview.InterviewDate)
                    {
                        if ((newStart >= otherStart && newStart < otherEnd) ||
                            (newEnd > otherStart && newEnd <= otherEnd) ||
                            (newStart <= otherStart && newEnd >= otherEnd))
                        {
                            Console.WriteLine(3);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }

}
