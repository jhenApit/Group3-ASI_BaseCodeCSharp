using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;
using static Basecode.Data.Enums.Enums;
using static Basecode.Data.Models.CurrentHires;

namespace Basecode.Data.Repositories
{
    public class CurrentHiresRepository : BaseRepository, ICurrentHiresRepository
    {
        private readonly BasecodeContext _context;
        public CurrentHiresRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public async Task<IQueryable<CurrentHires>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<CurrentHires>().Include(e => e.Applicant));
        }

        public async Task AddAsync(CurrentHires CurrentHires)
        {
            await _context.CurrentHires.AddAsync(CurrentHires);
            await _context.SaveChangesAsync();
        }

        public async Task<CurrentHires?> GetByIdAsync(int id)
        {
            return await _context.CurrentHires.FindAsync(id);
        }

        public async Task UpdateAsync(CurrentHires CurrentHires)
        {
            var existingCurrentHires = await _context.CurrentHires.FindAsync(CurrentHires.Id);
            
            if (existingCurrentHires != null)
            {
                // Update the properties of the existing entity
                existingCurrentHires.HireStatus = CurrentHires.HireStatus;
                existingCurrentHires.HireDate = CurrentHires.HireDate;

                // Save the changes
                await _context.SaveChangesAsync();
            }

        }
        public async Task DeleteAsync(int id)
        {
            var data = await _context.CurrentHires.FindAsync(id);
            if (data != null)
            {
                _context.CurrentHires.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CurrentHires?> GetByPositionIdAsync(int positionId)
        {
            return await _context.CurrentHires.FirstOrDefaultAsync(e => e.PositionId == positionId);
        }

        public async Task<CurrentHires?> GetByHireStatusAsync(string status)
        {
            if (Enum.TryParse(status, out HireStatus hireStatus))
            {
                return await _context.CurrentHires.FirstOrDefaultAsync(e => e.HireStatus == hireStatus);
            }
            return null;
        }
    }
}
