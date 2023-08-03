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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Basecode.Data.Dtos;
using Basecode.WebApp.Models;

namespace Basecode.Services.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IEmailService _emailService;
        private readonly IApplicantRepository _repository;
        private readonly ICharacterReferencesService _characterReferencesService;
        private readonly IMapper _mapper;
        private readonly IDGenerator _idGenerator = new();
        private readonly LogContent _logContent = new();
        private readonly IErrorHandling _errorHandling;
        public ApplicantService (
            IAddressService addressService,
            ICharacterReferencesService characterService,
            IEmailService emailService,
            ICharacterReferencesService characterReferencesService,
            IApplicantRepository repository, 
            IMapper mapper
        )
        {
            _addressService = addressService;
            _characterService = characterService;
            _emailService = emailService;
            _characterReferencesService = characterReferencesService;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves an applicant by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant.</param>
        /// <returns>The applicant with the specified ID.</returns>
        public async Task<int> AddAsync(ApplicantCreationDto applicant)
        {
            var applicantModel = _mapper.Map<Applicants>(applicant);

            applicantModel.ApplicantId = _idGenerator.GenerateRandomApplicantId();
            applicantModel.ApplicationDate = DateTime.Now;
            applicantModel.ApplicationStatus = Data.Enums.Enums.ApplicationStatus.Received;
            //Row 41 on the function List. Set requirements to TBC upon applying
            applicantModel.Requirements = Data.Enums.Enums.Requirements.TBC;
            
            await _repository.AddAsync(applicantModel);
            return applicantModel.Id;
        }

        /// <summary>
        /// display the status being passed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(int id, string status)
        {
            var applicantModel = await _repository.GetByIdAsync(id);
            if (applicantModel != null)
            {
                Console.WriteLine("applicant is in table.");
                if (Enum.TryParse(status, out ApplicationStatus parsedStatus))
                {
                    var applicant = new ApplicantsUpdationDto
                    {
                        Id = applicantModel.Id,
                        ApplicationStatus = parsedStatus
                    };
                    Console.WriteLine("applicant is updated." + parsedStatus);
                    var applicantMapper = _mapper.Map<Applicants>(applicant);
                    return  _repository.Update(applicantMapper);
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

        public async Task<Applicants?> GetByApplicantIdAsync(string applicantId)
        {
            return await _repository.GetByApplicantIdAsync(applicantId);
        }

        public async Task<List<Applicants>> GetByEmailAsync(string email)
        {
            var applicantEmails = await _repository.GetByEmailAsync(email);
            return applicantEmails.ToList();
        }

        public async Task<Applicants?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Applicants?> GetByNameAsync(string fname, string mname, string lname)
        {
            return await _repository.GetByNameAsync(fname, mname, lname);
        }

        public async Task<List<Applicants>> RetrieveAllAsync()
        {
            var applicants = await _repository.RetrieveAllAsync();
            return applicants.ToList();
        }

        public async Task<LogContent> AddApplicantLogContent(ApplicantCreationDto applicantCreationDto)
        {
            
            List<string> errors = new List<string>();
            if (applicantCreationDto.Resume ==  null) 
            {
                errors.Add("Resume is missing\n");
            }
            var applications = await GetByEmailAsync(applicantCreationDto.Email);
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

        public async Task<bool> AddApplicantAsync(ApplicationFormViewModel model, IFormFile resumeFile, IFormFile photo)
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

            var data = await AddApplicantLogContent(model.Applicant);
            if (!data.Result)
            {
                return false;
            }

            var applicantIsInserted = await AddAsync(model.Applicant);
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
            _addressService.AddAsync(address);

            var characRef1 = new CharacterReferencesCreationDto
            {
                ApplicantId = applicantIsInserted,
                Name = model.CharacterReferences1.Name,
                Relationship = model.CharacterReferences1.Relationship,
                Email = model.CharacterReferences1.Email,
                MobileNumber = model.CharacterReferences1.MobileNumber
            };
            _characterService.AddAsync(characRef1);

            var characRef2 = new CharacterReferencesCreationDto
            {
                ApplicantId = applicantIsInserted,
                Name = model.CharacterReferences2.Name,
                Relationship = model.CharacterReferences2.Relationship,
                Email = model.CharacterReferences2.Email,
                MobileNumber = model.CharacterReferences2.MobileNumber
            };
            _characterService.AddAsync(characRef2);

            var recipient = model.Applicant.Email;
            var subject = "Application Update";
            var body = "Your application ID is " + model.Applicant.ApplicantId;
            await _emailService.SendEmail(recipient, subject, body);

            // Return true if the applicant was successfully added
            return true;
        }
    }
}
