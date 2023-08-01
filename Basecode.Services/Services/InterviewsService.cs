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
        public List<Interviews> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(InterviewsCreationDto interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(interviews);
            _repository.Add(InterviewsModel);
        }

        public Interviews? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(InterviewsUpdationDto Interviews)
        {
            var InterviewsModel = _mapper.Map<Interviews>(Interviews);
            Console.WriteLine("Services" + InterviewsModel.InterviewDate);
            _repository.Update(InterviewsModel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Interviews? GetByApplicantId(int applicantId)
        {
            return _repository.GetByApplicantId(applicantId);
        }

    }

}
