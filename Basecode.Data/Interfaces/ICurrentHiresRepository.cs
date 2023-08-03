using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface ICurrentHiresRepository
    {
        Task<IQueryable<CurrentHires>> RetrieveAllAsync();
        Task<CurrentHires?> GetByPositionIdAsync(int positionId);
        Task AddAsync(CurrentHires currentHires);
        Task<CurrentHires?> GetByIdAsync(int id);
        Task UpdateAsync(CurrentHires currentHires);
        Task DeleteAsync(int id);
        Task<CurrentHires?> GetByHireStatusAsync(string status);
    }
}
