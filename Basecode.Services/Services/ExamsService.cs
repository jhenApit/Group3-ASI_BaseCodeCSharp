using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.Exams;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using NLog;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class ExamsService : ErrorHandling, IExamsService
    {
        private readonly IExamsRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ExamsService(IExamsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all exams from the database asynchronously.
        /// </summary>
        /// <returns>A list of exams.</returns>
        public async Task<List<Exams>> RetrieveAllAsync()
        {
            try
            {
                var exams = await _repository.RetrieveAllAsync();
                return exams.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new exam to the database asynchronously.
        /// </summary>
        /// <param name="Exams">The exam data to be added.</param>
        public async Task AddAsync(ExamCreationDto Exams)
        {
            try
            {
                var ExamsModel = _mapper.Map<Exams>(Exams);
                ExamsModel.Results = false;
                await _repository.AddAsync(ExamsModel);
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an exam from the database by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the exam to retrieve.</param>
        /// <returns>The exam with the specified ID, or null if not found.</returns>
        public async Task<Exams?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing exam in the database asynchronously.
        /// </summary>
        /// <param name="Exams">The exam data to be updated.</param>
        public async Task UpdateAsync(ExamUpdationDto Exams)
        {
            try
            {
                var ExamsModel = _mapper.Map<Exams>(Exams);
                await _repository.UpdateAsync(ExamsModel);
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes an exam from the database by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the exam to delete.</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > DeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an exam from the database by the applicant's ID asynchronously.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant associated with the exam.</param>
        /// <returns>The exam associated with the specified applicant ID, or null if not found.</returns>
        public async Task<Exams?> GetByApplicantIdAsync(int applicantId)
        {
            try
            {
                return await _repository.GetByApplicantIdAsync(applicantId);
            }
            catch (Exception ex)
            {
                _logger.Error("ExamsService > GetByApplicantIdAsync > Failed:" + ex.Message);
                throw;
            }
        }
    }
}
