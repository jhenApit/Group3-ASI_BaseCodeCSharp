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
        HrEmployee GetByEmail(string email);
        void Add(HREmployeeCreationDto hrEmployee);
        HrEmployee GetById(int id);
        void Update(HREmployeeUpdationDto hrEmployee);
        void SemiDelete(int id);
        void PermaDelete(int id);
    }
}
