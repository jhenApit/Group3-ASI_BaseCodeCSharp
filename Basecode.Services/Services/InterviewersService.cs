using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;

namespace Basecode.Services.Services
{
    public class InterviewersService : IInterviewersService
    {
        private readonly IInterviewersRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public InterviewersService(IInterviewersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all Interviewers from the repository.
        /// </summary>
        /// <returns>A list of Interviewers.</returns>
        public List<Interviewers> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new Interviewer to the repository.
        /// </summary>
        /// <param name="Interviewers">The Interviewer to add.</param>
        public void Add(Interviewers Interviewers)
        {
            var interviewersModel = new Interviewers();
            interviewersModel.Name = Interviewers.Name;
            interviewersModel.Email = Interviewers.Email;

            _repository.Add(interviewersModel);
        }

        /// <summary>
        /// Retrieves an Interviewer by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the Interviewer.</param>
        /// <returns>The Interviewer if found; otherwise, null.</returns>
        public Interviewers? GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Updates an Interviewer in the repository.
        /// </summary>
        /// <param name="Interviewers">The Interviewer to update.</param>
        public void Update(InterviewersUpdationDto Interviewers)
        {
            var InterviewersModel = _mapper.Map<Interviewers>(Interviewers);
            InterviewersModel.Name = Interviewers.Name;

            _repository.Update(InterviewersModel);
        }

        /// <summary>
        /// Deletes an Interviewer from the repository.
        /// </summary>
        /// <param name="id">The ID of the Interviewer to delete.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// Retrieves an Interviewer by its name from the repository.
        /// </summary>
        /// <param name="name">The name of the Interviewer.</param>
        /// <returns>The Interviewer if found; otherwise, null.</returns>
        public Interviewers? GetByName(string name)
        {
            return _repository.GetByName(name);
        }

    }

}
