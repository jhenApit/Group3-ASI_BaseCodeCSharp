using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.CurrentHires;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface ICurrentHiresService
    {
        Task<List<CurrentHires>> RetrieveAllAsync();
        Task<CurrentHires?> GetByPositionIdAsync(int positionId);
        Task<CurrentHires?> GetByHireStatusAsync(string status);
        Task AddAsync(CurrentHiresCreationDto CurrentHires);
        Task<CurrentHires?> GetByIdAsync(int id);
        Task UpdateAsync(CurrentHiresUpdationDto CurrentHires);
        Task DeleteAsync(int id);
    }
}
