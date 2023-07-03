using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IHrEmployeeRepository
    {
        IQueryable<HrEmployee> RetrieveAll();
        HrEmployee GetById(int id);
        void Update(HrEmployee hrEmployee);
    }
}