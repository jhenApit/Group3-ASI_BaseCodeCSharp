using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Basecode.Data.RandomIDGenerator;

namespace Basecode.Services.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDGenerator _idGenerator = new();
        private readonly LogContent _logContent = new();
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
            applicantModel.ApplicantId = _idGenerator.GenerateRandomApplicantId();
            applicantModel.ApplicationDate = DateTime.Now;
            applicantModel.ApplicationStatus = Data.Enums.Enums.ApplicationStatus.Received;
            //Row 41 on the function List. Set requirements to TBC upon applying
            applicantModel.Requirements = Data.Enums.Enums.Requirements.TBC;
            _repository.Add(applicantModel);
            return applicantModel.Id;
        }
        

        public Applicants GetByApplicantId(string applicantId)
        {
            return _repository.GetByApplicantId(applicantId);
        }

        public Applicants GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Applicants GetByName(string fname, string mname, string lname)
        {
            return _repository.GetByName(fname, mname, lname);
        }

        public List<Applicants> GetByEmail(string email)
        {
            return _repository.GetByEmail(email).ToList();
        }

        public List<Applicants> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public LogContent AddApplicantLogContent(ApplicantCreationDto applicantCreationDto)
        {
            
            List<string> errors = new List<string>();
            if (applicantCreationDto.Resume ==  null) 
            {
                errors.Add("Resume is missing\n");
            }
            var applications = GetByEmail(applicantCreationDto.Email);
            foreach (var applicant in applications)
            {
                if (applicant.JobId == applicantCreationDto.JobId)
                {
                    errors.Add($"{applicantCreationDto.Email} already applied for this job!");
                    break;
                }
            }
            if (errors.Count > 0)
            {
                // Combine the error messages into a single string with line breaks
                string errorMessage = string.Join(Environment.NewLine, errors);

                // Set the log content properties
                _logContent.Result = false;
                _logContent.ErrorCode = "400";
                _logContent.Message = errorMessage;
            }
            else
            {
                _logContent.Result = true;
            }

            return _logContent;
        }
    }
}
