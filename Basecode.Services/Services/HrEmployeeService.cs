using AutoMapper;
using Basecode.Data;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using static Basecode.Data.Constants;


namespace Basecode.Services.Services
{
    public class HrEmployeeService : IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        public HrEmployeeService(IHrEmployeeRepository repository, IMapper mapper, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        /// <summary>
        /// Retrieves all HR employees from the repository.
        /// </summary>
        /// <returns>List of HR employees</returns>
        public async Task<List<HrEmployee>> RetrieveAllAsync()
        {
            var hr = await _repository.RetrieveAllAsync();
            return hr.ToList();
        }

        /// <summary>
        /// Adds a new HR employee to the repository.
        /// </summary>
        /// <param name="hrEmployeeDto">The DTO object containing the information of the HR employee to be added</param>
        public async Task AddAsync(HREmployeeCreationDto hrEmployeeDto)
        {
            var hrEmployeeModel = _mapper.Map<HrEmployee>(hrEmployeeDto);

            await _repository.AddAsync(hrEmployeeModel);
        }

        /// <summary>
        /// Retrieves an HR employee by their ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public async Task<HrEmployee?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<HrEmployee> GetByUserIdAsync(string id)
        {
            return await _repository.GetByUserIdAsync(id);
        }
        /// <summary>
        /// Updates an existing HR employee in the repository.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        public async Task UpdateAsync(HREmployeeUpdationDto hrEmployee)
        {
            var hrEmployeeModel = _mapper.Map<HrEmployee>(hrEmployee);

            // Update only the properties that should be modified
            hrEmployeeModel.Name = hrEmployee.Name;
            hrEmployeeModel.Email = hrEmployee.Email;
            hrEmployeeModel.Password = hrEmployee.Password;
            hrEmployeeModel.ModifiedBy = hrEmployee.ModifiedBy;
            hrEmployeeModel.ModifiedDate = DateTime.Now;

            await _repository.UpdateAsync(hrEmployeeModel);
        }

        /// <summary>
        /// Permanently deletes an HR employee from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to permanently delete</param>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves an HR employee by their email from the repository.
        /// </summary>
        /// <param name="email">The email of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public async Task<HrEmployee> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        /// <summary>
        /// Creates an HR account and logs the error content if the account creation fails.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the information of the HR employee to be created</param>
        /// <returns>The log content upon creating a HR account</returns>
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            HrEmployee hr = GetByEmail(hrEmployee.Email);
            if (hr != null)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "400";
                _logContent.Message = "Email already registered.";
            }
            else
            {
                _logContent.Result = true;
            }

            return _logContent;
        }

        /*/// <summary>
        /// Edits an HR account and logs the error content if the edit fails.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        /// <returns>The log content upon editing a HR account</returns>
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            var hrEmail = GetByEmail(hrEmployee.Email);
            if (hrEmail != null)
            {
                if (hrEmail.Id != hrEmployee.Id)
                {
                    _logContent.Result = false;
                    _logContent.ErrorCode = "400. Edit Failed!";
                    _logContent.Message = "Email already exists";
                }
                else
                {
                    _logContent.Result = true;
                }
            }
            else
            {
                _logContent.Result = true;
            }

            var hrUsername = GetById(hrEmployee.Id);
            if (hrUsername != null)
            {
                if (hrUsername.User.Id != hrEmployee.UserId)
                {
                    _logContent.Result = false;
                    _logContent.ErrorCode = "400. Edit Failed!";
                    _logContent.Message = "Username is not available";
                }
                else
                {
                    _logContent.Result = true;
                }
            }
            else
            {
                _logContent.Result = true;
            }

            return _logContent;
        }
        /// <summary>
        /// Handles the login and logging for the errors of the input
        /// </summary>
        /// <param name="email">the email input for log in</param>
        /// <param name="password">the password input for log in</param>
        /// <returns> the log content for the log in</returns>
        public LogContent Login(string email, string password)
        {
            var hr = GetByEmail(email);

            if (hr != null)
            {
                if (hr.Password != password)
                {
                    _logContent.Result = false;
                    _logContent.ErrorCode = "401. Incorrect Password!";
                    _logContent.Message = "Incorrect Password";
                }
                else
                {
                    _logContent.Result = true;
                    _logContent.Message = "Login succesful for user " + email;
                }
            }
            else
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "401. Incorrect Email!";
                _logContent.Message = "Email doesn't exist";
            }
            return _logContent;
        }*/
    }
}
