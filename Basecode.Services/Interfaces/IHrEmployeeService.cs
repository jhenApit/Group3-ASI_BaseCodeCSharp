using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using static Basecode.Services.Utils.ErrorHandling;
using Basecode.Services.Utils;
using Basecode.Data.Dtos.HrEmployee;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Services.Interfaces
{
    public interface IHrEmployeeService
    {
        Task<List<HrEmployee>> RetrieveAllAsync();
        Task<HrEmployee?> GetByEmailAsync(string email);
        Task AddAsync(HREmployeeCreationDto hrEmployee);
        Task<HrEmployee?> GetByIdAsync(int id);
        Task<HrEmployee?> GetByUserIdAsync(string id);
        Task UpdateAsync(HREmployeeUpdationDto hrEmployee);
        Task DeleteAsync(int id);
    }
}
