using AutoMapper;
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
            if (hrEmployee.Name.Length > 150)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Length Validation";
                _logContent.Message = "Name cannot be longer than 150 characters";
            }
            else if (hrEmployee.Email.Length > 50)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Length Validation ";
                _logContent.Message = "Email cannot be longer than 50 characters long";
            }
            else if (hrEmployee.Password!.Length > 30)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Length Validation";
                _logContent.Message = "Password cannot be longer than 30 characters";
            }
            else if (IsEmailValid(hrEmployee.Email) == false)
            {
                _logContent.Result = false;
                _logContent.ErrorCode = "ERR! Invalid Email";
                _logContent.Message = "Email is not Alliance Email";
            }


            return _logContent;
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
