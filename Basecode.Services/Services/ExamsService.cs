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
