using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using Basecode.Data.Dtos;

namespace Basecode.Services.Interfaces
{
    public interface IHrEmployeeService
    {
        List<HrEmployee> RetrieveAll();
        void Add(HREmployeeCreationDto hrEmployee);
        HrEmployee GetById(int id);
        void Update(HREmployeeUpdationDto hrEmployee);
    }
}
