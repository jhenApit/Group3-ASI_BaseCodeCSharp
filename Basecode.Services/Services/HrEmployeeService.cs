using AutoMapper;
using Basecode.Data;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


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
            try
            {
                var hr = await _repository.RetrieveAllAsync();
                return hr.ToList();
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Adds a new HR employee to the repository.
        /// </summary>
        /// <param name="hrEmployeeDto">The DTO object containing the information of the HR employee to be added</param>
        public async Task AddAsync(HREmployeeCreationDto hrEmployeeDto)
        {
            try
            {
                var hrEmployeeModel = _mapper.Map<HrEmployee>(hrEmployeeDto);
                await _repository.AddAsync(hrEmployeeModel);
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an HR employee by their ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public async Task<HrEmployee?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }
        public async Task<HrEmployee> GetByUserIdAsync(string id)
        {
            try
            {
                return await _repository.GetByUserIdAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > GetByUserIdAsync > Failed:" + ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Updates an existing HR employee in the repository.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        public async Task UpdateAsync(HREmployeeUpdationDto hrEmployee)
        {
            try
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
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Permanently deletes an HR employee from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to permanently delete</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > DeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an HR employee by their email from the repository.
        /// </summary>
        /// <param name="email">The email of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public async Task<HrEmployee?> GetByEmailAsync(string email)
        {
            try
            {
                return await _repository.GetByEmailAsync(email);
            }
            catch (System.Exception ex)
            {
                _logger.Error("HrEmployeeService > GetByEmailAsync > Failed:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Edits an HR account and logs the error content if the edit fails.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        /// <returns>The log content upon editing a HR account</returns>
        public async Task<LogContent> EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            List<string> errors = new List<string>();
            var hrEmail = await GetByEmailAsync(hrEmployee.Email);
            if (hrEmail != null)
            {
                if (hrEmail.Id != hrEmployee.Id)
                {
                    errors.Add($"{hrEmployee.Email} is already registered to another account\n");
                }
            }

            var existingUsername = await _userManager.FindByNameAsync(hrEmployee.UserName);
            if (existingUsername != null)
            {
                if (existingUsername.Id != hrEmployee.UserId)
                {
                    errors.Add("Username is not available");
                }
            }
            if (errors.Count > 0)
            {
                // Combine the error messages into a single string with line breaks
                string errorMessage = string.Join(Environment.NewLine, errors);

                // Set the log content properties
                _logContent.Result = false;
                _logContent.ErrorCode = "400. Edit Failed!";
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
