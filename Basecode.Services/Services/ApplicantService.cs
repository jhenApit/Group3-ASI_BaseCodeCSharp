using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;

namespace Basecode.Services.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _repository;
        private readonly IMapper _mapper;
        public ApplicantService(IApplicantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves an applicant by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant.</param>
        /// <returns>The applicant with the specified ID.</returns>
        public int Add(ApplicantCreationDto applicant)
        {
            var applicantModel = _mapper.Map<Applicants>(applicant);
            applicantModel.ApplicantId = GenerateRandomApplicantId();
            applicantModel.ApplicationDate = DateTime.Now;
            applicantModel.ApplicationStatus = Data.Enums.Enums.ApplicationStatus.UndergoingBackgroundCheck;
            _repository.Add(applicantModel);
            return applicantModel.Id;
        }
        private static string? GenerateRandomApplicantId()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(allowedChars, 15)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        public Applicants GetByApplicantId(string applicantId)
        {
            return _repository.GetByApplicantId(applicantId);
        }

        public Applicants GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Applicants GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public List<Applicants> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }
    }
}
