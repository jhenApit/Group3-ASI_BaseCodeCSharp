﻿using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Basecode.Services.Services
{
    public class HrEmployeeService : ErrorHandling, IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();
    
        public HrEmployeeService(IHrEmployeeRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<HrEmployee> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

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

        public HrEmployee GetById(int id)
        {
            return _repository.GetById(id);
        }

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

        public void SemiDelete(int id)
        {
            var hr = _repository.GetById(id);
            hr.IsDeleted = true;
            hr.ModifiedBy = System.Environment.UserName;
            hr.ModifiedDate = DateTime.Now;
            _repository.SemiDelete(hr);
        }

        public void PermaDelete(int id)
        {
            _repository.PermaDelete(id);
        }

        public HrEmployee GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        /// <summary>
        /// Log the error content upon creating the HR account
        /// </summary>
        /// <param name="hrEmployee">HREmployeeCreationDto</param>
        /// <returns>LogContent upon creating a HR Account</returns>
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            HrEmployee hr = GetByEmail(hrEmployee.Email);

            if(hr != null)
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

            /*string? email = GetByEmail(hrEmployee.Email).Email;
            if (IsNameValid(hrEmployee.Name) == false)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Name Validation";
                _logContent.Message = email;
            }
            else if (IsEmailValid(hrEmployee.Email) == false)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Invalid Email";
                _logContent.Message = "Email is not Alliance Email";
            }
            else
            {
                _logContent.Result = true;
            }


            return _logContent; */
        }

        public bool IsNameValid(string name)
        {
            string pattern = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(name, pattern);
        }

        public bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@asi-dev2\.com$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
