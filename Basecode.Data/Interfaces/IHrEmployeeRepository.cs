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
        Task<IQueryable<HrEmployee>> RetrieveAllAsync();
        Task<HrEmployee?> GetByEmailAsync(string email);
        Task AddAsync(HrEmployee hrEmployee);
        Task<HrEmployee?> GetByIdAsync(int id);
        Task<HrEmployee?> GetByUserIdAsync(string id);
        Task UpdateAsync(HrEmployee hrEmployee);
        Task DeleteAsync(int id);
    }
}