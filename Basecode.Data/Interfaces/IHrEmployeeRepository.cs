using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IHrEmployeeRepository
    {
        IQueryable<HrEmployee> RetrieveAll();
        HrEmployee GetByEmail(string email);
        void Add(HrEmployee hrEmployee);
        HrEmployee GetById(int id);
        HrEmployee GetByUserId(string id);
        void Update(HrEmployee hrEmployee);
        void Delete(int id);
    }
}