﻿using AutoMapper;
using Basecode.Data;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;


namespace Basecode.Services.Services
{
    public class HrEmployeeService : IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
        private readonly SignInManager<IdentityUser> _signInManager;

        public HrEmployeeService(IHrEmployeeRepository repository, IMapper mapper, SignInManager<IdentityUser> signInManager) 
        {
            _repository = repository;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all HR employees from the repository.
        /// </summary>
        /// <returns>List of HR employees</returns>
        public List<HrEmployee> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new HR employee to the repository.
        /// </summary>
        /// <param name="hrEmployeeDto">The DTO object containing the information of the HR employee to be added</param>
        public void Add(HREmployeeCreationDto hrEmployeeDto)
        {
            var hrEmployeeModel = _mapper.Map<HrEmployee>(hrEmployeeDto);
            hrEmployeeModel.CreatedBy = System.Environment.UserName;
            hrEmployeeModel.CreatedDate = DateTime.Now;
            hrEmployeeModel.ModifiedBy = System.Environment.UserName;
            hrEmployeeModel.ModifiedDate = DateTime.Now;
            hrEmployeeModel.IsDeleted = false;

            _repository.Add(hrEmployeeModel);
        }

        /// <summary>
        /// Retrieves an HR employee by their ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public HrEmployee GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Updates an existing HR employee in the repository.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        public void Update(HREmployeeUpdationDto hrEmployee)
        {
            var hrEmployeeModel = _mapper.Map<HrEmployee>(hrEmployee);

            // Update only the properties that should be modified
            hrEmployeeModel.Name = hrEmployee.Name;
            hrEmployeeModel.Email = hrEmployee.Email;
            hrEmployeeModel.Password = hrEmployee.Password;
            hrEmployeeModel.ModifiedBy = System.Environment.UserName;
            hrEmployeeModel.ModifiedDate = DateTime.Now;

            _repository.Update(hrEmployeeModel);
        }

        /// <summary>
        /// Marks an HR employee as deleted in the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to mark as deleted</param>
        public void SemiDelete(int id)
        {
            var hr = _repository.GetById(id);
            hr.IsDeleted = true;
            hr.ModifiedBy = System.Environment.UserName;
            hr.ModifiedDate = DateTime.Now;
            _repository.SemiDelete(hr);
        }

        /// <summary>
        /// Permanently deletes an HR employee from the repository.
        /// </summary>
        /// <param name="id">The ID of the HR employee to permanently delete</param>
        public void PermaDelete(int id)
        {
            _repository.PermaDelete(id);
        }

        /// <summary>
        /// Retrieves an HR employee by their email from the repository.
        /// </summary>
        /// <param name="email">The email of the HR employee to retrieve</param>
        /// <returns>The HR employee object</returns>
        public HrEmployee GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
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

        /// <summary>
        /// Edits an HR account and logs the error content if the edit fails.
        /// </summary>
        /// <param name="hrEmployee">The DTO object containing the updated information of the HR employee</param>
        /// <returns>The log content upon editing a HR account</returns>
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            var hr = GetByEmail(hrEmployee.Email);
            if (hr != null)
            {
                if (hr.Id != hrEmployee.Id)
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

            return _logContent;
        }

    }
}
