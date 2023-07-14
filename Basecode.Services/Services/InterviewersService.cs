using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;

namespace Basecode.Services.Services
{
    public class InterviewersService : ErrorHandling, IInterviewersService
    {
        private readonly IInterviewersRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public InterviewersService(IInterviewersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<Interviewers> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(Interviewers Interviewers)
        {
            var interviewersModel = new Interviewers();
            interviewersModel.Name = Interviewers.Name;
            interviewersModel.Email = Interviewers.Email;

            _repository.Add(interviewersModel);
        }

        public Interviewers? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(InterviewersUpdationDto Interviewers)
        {
            var InterviewersModel = _mapper.Map<Interviewers>(Interviewers);
            InterviewersModel.Name = Interviewers.Name;

            _repository.Update(InterviewersModel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Interviewers? GetByName(string name)
        {
            return _repository.GetByName(name);
        }

    }

}
