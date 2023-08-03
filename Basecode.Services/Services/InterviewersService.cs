using AutoMapper;
using Basecode.Data.Dtos.Interviewers;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using NLog;

namespace Basecode.Services.Services
{
    /// <summary>
    /// Service class for managing interviewers.
    /// </summary>
    public class InterviewersService : IInterviewersService
    {
        private readonly IInterviewersRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor for InterviewersService.
        /// </summary>
        /// <param name="repository">The repository for interviewers data.</param>
        /// <param name="mapper">The mapper for DTO and entity conversion.</param>
        public InterviewersService(IInterviewersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all Interviewers asynchronously.
        /// </summary>
        /// <returns>A list of all interviewers.</returns>
        public async Task<List<Interviewers>> RetrieveAllAsync()
        {
            try
            {
                var interviews = await _repository.RetrieveAllAsync();
                return interviews.ToList();
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new Interviewer asynchronously.
        /// </summary>
        /// <param name="Interviewers">TThe DTO containing the details of the new interviewer.</param>
        public async Task AddAsync(InterviewersCreationDto Interviewers)
        {
            try
            {
                var InterviewsModel = _mapper.Map<Interviewers>(Interviewers);
                await _repository.AddAsync(InterviewsModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an Interviewer by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the Interviewer.</param>
        /// <returns>The interview with the specified ID, or null if not found.</returns>
        public async Task<Interviewers?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an Interviewer asynchronously.
        /// </summary>
        /// <param name="Interviewers">The DTO containing the details of the updated interviewer.</param>
        public async Task UpdateAsync(InterviewersUpdationDto Interviewers)
        {
            try
            {
                var InterviewersModel = _mapper.Map<Interviewers>(Interviewers);
                InterviewersModel.Name = Interviewers.Name;

                await _repository.UpdateAsync(InterviewersModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes an Interviewer asynchronously.
        /// </summary>
        /// <param name="id">The interview with the specified ID, or null if not found.</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > DeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an Interviewer by its name from the repository.
        /// </summary>
        /// <param name="name">The name of the Interviewer.</param>
        /// <returns>The Interviewer if found; otherwise, null.</returns>
        public async Task<Interviewers?> GetByNameAsync(string name)
        {
            try
            {
                return await _repository.GetByNameAsync(name);
            }
            catch (System.Exception ex)
            {
                _logger.Error("InterviewersService > GetByNameAsync > Failed:" + ex.Message);
                throw;
            }
        }

    }

}
