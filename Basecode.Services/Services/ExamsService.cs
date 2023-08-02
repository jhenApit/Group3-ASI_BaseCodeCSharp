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
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class ExamsService : ErrorHandling, IExamsService
    {
        private readonly IExamsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public ExamsService(IExamsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<Exams>> RetrieveAllAsync()
        {
            var exams = await _repository.RetrieveAllAsync();
            return exams.ToList();
        }

        public async Task AddAsync(ExamCreationDto Exams)
        {
            var ExamsModel = _mapper.Map<Exams>(Exams);
            ExamsModel.Results = false;
            await _repository.AddAsync(ExamsModel);
        }

        public async Task<Exams?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ExamUpdationDto Exams)
        {
            var ExamsModel = _mapper.Map<Exams>(Exams);
            await _repository.UpdateAsync(ExamsModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Exams?> GetByApplicantIdAsync(int applicantId)
        {
            return await _repository.GetByApplicantIdAsync(applicantId);
        }

    }
}
