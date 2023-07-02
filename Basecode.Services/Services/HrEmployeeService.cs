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
    }
}
