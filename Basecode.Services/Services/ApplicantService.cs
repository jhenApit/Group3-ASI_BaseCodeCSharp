using AutoMapper;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Basecode.Data.RandomIDGenerator;
using Basecode.Data.Dtos.Applicants;
using static Basecode.Data.Enums.Enums;
using Microsoft.AspNetCore.Http;
using Basecode.Data.Dtos;
using Basecode.Data.ViewModels;
using NLog;

namespace Basecode.Services.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IAddressService _addressService;
        private readonly ICharacterReferencesService _characterService;
        private readonly IApplicantRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDGenerator _idGenerator = new();
        private readonly LogContent _logContent = new();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISendEmailService _sendEmailService;
        public ApplicantService (
            IAddressService addressService,
            ICharacterReferencesService characterService,
            IApplicantRepository repository, 
            IMapper mapper,
            IErrorHandling errorHandling,
            ISendEmailService sendEmailService
        )
        {
            _addressService = addressService;
            _characterService = characterService;
            _repository = repository;
            _mapper = mapper;
            _errorHandling = errorHandling;
            _sendEmailService = sendEmailService;
        }

        /// <summary>
        /// Adds a new applicant asynchronously to the database.
        /// </summary>
        /// <param name="applicant">The <see cref="ApplicantCreationDto"/> containing applicant details.</param>
        /// <returns>The ID of the newly added applicant.</returns>
        public async Task<int> AddAsync(ApplicantCreationDto applicant)
        {
            try
            {
                var applicantModel = _mapper.Map<Applicants>(applicant);

                applicantModel.ApplicantId = _idGenerator.GenerateRandomApplicantId();
                applicantModel.ApplicationDate = DateTime.Now;
                applicantModel.ApplicationStatus = ApplicationStatus.Received;
                
                //Row 41 on the function List. Set requirements to TBC upon applying
                applicantModel.Requirements = Data.Enums.Enums.Requirements.TBC;

                await _repository.AddAsync(applicantModel);
                return applicantModel.Id;
            }
            catch ( Exception ex ) 
            {
                _logger.Error("ApplicantService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates the status of an applicant asynchronously in the database.
        /// </summary>
        /// <param name="id">The ID of the applicant to update.</param>
        /// <param name="status">The new status of the applicant.</param>
        /// <returns>True if the update is successful, False otherwise.</returns>
        public async Task<bool> UpdateAsync(int id, string status)
        {
            var applicantModel = await _repository.GetByIdAsync(id);
            try
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
                    return await _repository.UpdateAsync(applicantMapper);
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error("ApplicantService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ApplicantID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>
        public async Task<Applicants?> GetByApplicantIdAsync(string applicantId)
        {
            try
            {
                return await _repository.GetByApplicantIdAsync(applicantId);
            }
            catch (Exception ex) 
            {
                _logger.Error("ApplicantService > GetByApplicantIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of applicants from the database by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>A list of applicants with the specified email address.</returns>
        public async Task<List<Applicants>> GetByEmailAsync(string email)
        {
            try
            {
                var applicantEmails = await _repository.GetByEmailAsync(email);
                return applicantEmails.ToList();
            }
            catch (Exception ex) 
            {
                _logger.Error("ApplicantService > GetByEmailAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an applicant from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the applicant to retrieve.</param>
        /// <returns>The applicant with the specified ID, or null if not found.</returns>
        public async Task<Applicants?> GetByIdAsync(int id)
        {
            try 
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex) 
            {
                _logger.Error("ApplicantService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an applicant from the database by their first name, middle name, and last name.
        /// </summary>
        /// <param name="fname">The first name of the applicant.</param>
        /// <param name="mname">The middle name of the applicant.</param>
        /// <param name="lname">The last name of the applicant.</param>
        /// <returns>The applicant with the specified name, or null if not found.</returns>
        public async Task<Applicants?> GetByNameAsync(string fname, string mname, string lname)
        {
            try
            {
                return await _repository.GetByNameAsync(fname, mname, lname);
            }
            catch (Exception ex) 
            {
                _logger.Error("ApplicantService > GetByNameAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of all applicants from the database.
        /// </summary>
        /// <returns>A list of all applicants.</returns>
        public async Task<List<Applicants>> RetrieveAllAsync()
        {
            try
            {
                var applicants = await _repository.RetrieveAllAsync();
                return applicants.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("ApplicantService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds log content for an applicant and validates the applicant data before adding to the database.
        /// </summary>
        /// <param name="applicantCreationDto">The <see cref="ApplicantCreationDto"/> containing applicant details.</param>
        /// <returns>The log content indicating the result of the validation.</returns>
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

        /// <summary>
        /// Adds a new applicant asynchronously to the database with additional information.
        /// </summary>
        /// <param name="model">The <see cref="ApplicationFormViewModel"/> containing applicant and related information.</param>
        /// <param name="resumeFile">The resume file of the applicant.</param>
        /// <param name="photo">The photo file of the applicant.</param>
        /// <returns>True if the applicant is added successfully, False otherwise.</returns>
        public async Task<bool> AddApplicantAsync(ApplicationFormViewModel model, IFormFile resumeFile, IFormFile photo)
        {
            try 
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
                await _addressService.AddAsync(address);

                var characRef1 = new CharacterReferencesCreationDto
                {
                    ApplicantId = applicantIsInserted,
                    Name = model.CharacterReferences1.Name,
                    Relationship = model.CharacterReferences1.Relationship,
                    Email = model.CharacterReferences1.Email,
                    MobileNumber = model.CharacterReferences1.MobileNumber
                };
                await _characterService.AddAsync(characRef1);

                var characRef2 = new CharacterReferencesCreationDto
                {
                    ApplicantId = applicantIsInserted,
                    Name = model.CharacterReferences2.Name,
                    Relationship = model.CharacterReferences2.Relationship,
                    Email = model.CharacterReferences2.Email,
                    MobileNumber = model.CharacterReferences2.MobileNumber
                };
                await _characterService.AddAsync(characRef2);

                

                // Return true if the applicant was successfully added
                _logger.Info("ApplicantService > AddApplicantAsync > Successfully added applicant:");
                return true;
            }
            catch (Exception ex) 
            {
                _logger.Error("ApplicantService > AddApplicantAsync > Failed:" + ex.Message);
                throw;
            }
        }
    }
}
