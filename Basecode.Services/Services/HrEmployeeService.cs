using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;

namespace Basecode.Services.Services
{
    public class HrEmployeeService : IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _repository;
        private readonly IMapper _mapper;
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
    }
}
