using System.Globalization;
using AutoMapper;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.EntityFrameworkCore;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    /// <summary>
    /// Service class for managing interviews.
    /// </summary>
    public class InterviewsService : ErrorHandling, IInterviewsService
    {
        private readonly IInterviewsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        /// <summary>
        /// Constructor for InterviewsService.
        /// </summary>
        /// <param name="repository">The repository for interviews data.</param>
        /// <param name="mapper">The mapper for DTO and entity conversion.</param>
        public InterviewsService(IInterviewsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // <summary>
        /// Retrieves all interviews asynchronously.
        /// </summary>
        /// <returns>A list of all interviews.</returns>
        public async Task<List<Interviews>> RetrieveAllAsync()
        {
            var interviews = await _repository.RetrieveAllAsync();
            return interviews.ToList();
        }

        /// <summary>
        /// Adds a new interview asynchronously.
        /// </summary>
        /// <param name="interviews">The DTO containing the details of the new interview.</param>
        public async Task AddAsync(InterviewsCreationDto interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(interviews);
            await _repository.AddAsync(InterviewsModel);
        }

        /// <summary>
        /// Retrieves an interview by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the interview to retrieve.</param>
        /// <returns>The interview with the specified ID, or null if not found.</returns>
        public async Task<Interviews?> GetByIdAsync(int id)
        {
            return await  _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an interview asynchronously.
        /// </summary>
        /// <param name="Interviews">The DTO containing the details of the updated interview.</param>
        /// <returns>Updated interview</returns>
        public async Task UpdateAsync(InterviewsUpdationDto Interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(Interviews);
            Console.WriteLine("Services" + InterviewsModel.InterviewDate);
            await _repository.UpdateAsync(InterviewsModel);
        }

        /// <summary>
        /// Deletes an interview asynchronously.
        /// </summary>
        /// <param name="id">The ID of the interview to delete</param>
        /// <returns>The interview with the specified ID, or null if not found</returns>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Interviews>> GetByApplicantIdAsync(int applicantId)
        {
            var interviews = await _repository.GetByApplicantIdAsync(applicantId);
            return interviews.ToList();
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific interviewer and date asynchronously.
        /// </summary>
        /// <param name="interviewerId">The ID of the interviewer.</param>
        /// <param name="interviewDate">The date of the interviews to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAndDateAsync(int interviewerId, DateTime interviewDate)
        {
            return await _repository.GetInterviewsByInterviewerAndDateAsync(interviewerId, interviewDate);
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific interviewer asynchronously.
        /// </summary>
        /// <param name="interviewerId">The ID of the interviewer.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAsync(int interviewerId)
        {
            return await _repository.GetInterviewsByInterviewerAsync(interviewerId);
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific applicant asynchronously.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByApplicantAsync(int applicantId)
        {
            return await _repository.GetInterviewsByApplicantAsync(applicantId);
        }

        /// <summary>
        /// Checks if a new interview's time range overlaps with any existing interviews for the same interviewer or applicant.
        /// </summary>
        /// <param name="newInterview">The DTO containing the details of the new interview.</param>
        /// <returns>True if the time range overlaps with existing interviews, otherwise false.</returns>
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

        /// <summary>
        /// Checks if an updated interview's time range overlaps with any existing interviews for the same interviewer or applicant.
        /// </summary>
        /// <param name="newInterview">The DTO containing the updated details of the interview.</param>
        /// <returns>True if the time range overlaps with existing interviews, otherwise false.</returns>
        public async Task<bool> IsTimeRangeOverlappingAsync(InterviewsUpdationDto newInterview)
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
