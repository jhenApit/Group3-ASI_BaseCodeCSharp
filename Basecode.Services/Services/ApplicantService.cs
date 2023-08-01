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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Basecode.Services.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IEmailService _emailService;
        private readonly IApplicantRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDGenerator _idGenerator = new();
        private readonly LogContent _logContent = new();
        private readonly IErrorHandling _errorHandling;
        public ApplicantService (
            IAddressService addressService,
            ICharacterReferencesService characterService,
            IEmailService emailService,
            IApplicantRepository repository, 
            IMapper mapper
        )
        {
            _addressService = addressService;
            _characterService = characterService;
            _emailService = emailService;
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

        public async Task<bool> AddApplicant(ApplicationFormViewModel model, IFormFile resumeFile, IFormFile photo)
        {
            if (resumeFile != null && resumeFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    resumeFile.CopyTo(memoryStream);

                    // Convert the file content to a byte array and store it in the model
                    model.Applicant.Resume = memoryStream.ToArray();
                }
            }

            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);

                    // Convert the file content to a byte array and store it in the model
                    model.Applicant.Photo = memoryStream.ToArray();
                }
            }
            else
            {
                model.Applicant.Photo = null;
            }

            var data = AddApplicantLogContent(model.Applicant);
            if (!data.Result)
            {
                return false;
            }

            var applicantIsInserted = Add(model.Applicant);
            if (applicantIsInserted == 0)
            {
                Console.WriteLine("Addition Failed for applicant");
                return false;
            }

            var address = new AddressCreationDto
            {
                ApplicantId = applicantIsInserted,
                Street = model.Address.Street,
                City = model.Address.City,
                Province = model.Address.Province,
                ZipCode = model.Address.ZipCode
            };
            _addressService.Add(address);

            var characRef1 = new CharacterReferencesCreationDto
            {
                ApplicantId = applicantIsInserted,
                Name = model.CharacterReferences1.Name,
                Relationship = model.CharacterReferences1.Relationship,
                Email = model.CharacterReferences1.Email,
                MobileNumber = model.CharacterReferences1.MobileNumber
            };
            _characterService.Add(characRef1);

            var characRef2 = new CharacterReferencesCreationDto
            {
                ApplicantId = applicantIsInserted,
                Name = model.CharacterReferences2.Name,
                Relationship = model.CharacterReferences2.Relationship,
                Email = model.CharacterReferences2.Email,
                MobileNumber = model.CharacterReferences2.MobileNumber
            };
            _characterService.Add(characRef2);

            var recipient = model.Applicant.Email;
            var subject = "Application Update";
            var body = "Your application ID is " + model.Applicant.ApplicantId;
            _emailService.SendEmail(recipient, subject, body);

            // Return true if the applicant was successfully added
            return true;
        }
    }
}
