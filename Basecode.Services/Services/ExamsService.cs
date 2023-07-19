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
        public List<Exams> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(ExamCreationDto Exams)
        {
            var ExamsModel = _mapper.Map<Exams>(Exams);
            ExamsModel.Results = false;
            _repository.Add(ExamsModel);
        }

        public Exams? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(ExamUpdationDto Exams)
        {
            var ExamsModel = _mapper.Map<Exams>(Exams);
            _repository.Update(ExamsModel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Exams? GetByApplicantId(int applicantId)
        {
            return _repository.GetByApplicantId(applicantId);
        }

    }
}
