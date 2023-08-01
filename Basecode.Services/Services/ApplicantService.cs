using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Basecode.Data.RandomIDGenerator;
using Basecode.Data.Dtos.Applicants;
using static Basecode.Data.Enums.Enums;

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

        public List<Applicants> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// display the status being passed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public bool Update(int id, string status)
		{
            var applicantModel = _repository.GetById(id);
            if(applicantModel != null)
            {
                Console.WriteLine("applicant is in table.");
				if (Enum.TryParse(status, out ApplicationStatus parsedStatus))
				{
					var applicant = new ApplicantsUpdationDto
					{
                        Id = applicantModel.Id,
						ApplicationStatus = parsedStatus
					};
					Console.WriteLine("applicant is updated." +parsedStatus);
					var applicantMapper = _mapper.Map<Applicants>(applicant);
					return _repository.Update(applicantMapper);
				}
                return false;
			}
			
			else
			{
				// Handle the case where the provided status is not a valid ApplicationStatus enum value
				// You may throw an exception, log an error, or take any other appropriate action.
				return false;
			}
		}

		public LogContent AddApplicantLogContent(ApplicantCreationDto applicantCreationDto)
        {
            Applicants applicant = GetByName(applicantCreationDto.FirstName, applicantCreationDto.MiddleName, applicantCreationDto.LastName);
            if (applicant != null && applicant.JobId == applicantCreationDto.JobId)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "400";
                _logContent.Message = "Applicant already applied for this job!";
            }
            else
            {
                _logContent.Result = true;
            }

            return _logContent;
        }
    }
}
