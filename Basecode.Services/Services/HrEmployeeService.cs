using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;

namespace Basecode.Services.Services
{
    public class HrEmployeeService : IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _repository;
        public HrEmployeeService(IHrEmployeeRepository repository) 
        {
            _repository = repository;
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

        public HrEmployee GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public void Update(HrEmployee hrEmployee)
        {
            hrEmployee.Name = hrEmployee.Name;
            hrEmployee.Email = hrEmployee.Email;
            hrEmployee.Password = hrEmployee.Password;
            hrEmployee.ModifiedBy = System.Environment.UserName;
            hrEmployee.ModifiedDate = DateTime.Now;
            _repository.Update(hrEmployee);
        }
    }
}
