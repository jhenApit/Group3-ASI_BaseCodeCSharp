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

        /// <summary>
        /// Retrieves all current hires from the database.
        /// </summary>
        /// <returns>A queryable collection of current hires.</returns>
        public async Task<IQueryable<CurrentHires>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<CurrentHires>().Include(e => e.Applicant));
        }

        /// <summary>
        /// Adds a new current hire to the database.
        /// </summary>
        /// <param name="CurrentHires">The current hire object to add.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddAsync(CurrentHires CurrentHires)
        {
            await _context.CurrentHires.AddAsync(CurrentHires);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a current hire from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the current hire to retrieve.</param>
        /// <returns>The current hire with the specified ID, or null if not found.</returns>
        public async Task<CurrentHires?> GetByIdAsync(int id)
        {
            return await _context.CurrentHires.FindAsync(id);
        }

        /// <summary>
        /// Updates a current hire in the database.
        /// </summary>
        /// <param name="CurrentHires">The updated current hire object.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Deletes a current hire from the database.
        /// </summary>
        /// <param name="id">The ID of the current hire to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var data = await _context.CurrentHires.FindAsync(id);
            if (data != null)
            {
                _context.CurrentHires.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a current hire from the database by their position ID.
        /// </summary>
        /// <param name="positionId">The position ID of the current hire to retrieve.</param>
        /// <returns>The current hire with the specified position ID, or null if not found.</returns>
        public async Task<CurrentHires?> GetByPositionIdAsync(int positionId)
        {
            return await _context.CurrentHires.FirstOrDefaultAsync(e => e.PositionId == positionId);
        }

        /// <summary>
        /// Retrieves a current hire from the database by their hire status.
        /// </summary>
        /// <param name="status">The hire status of the current hire to retrieve.</param>
        /// <returns>The current hire with the specified hire status, or null if not found.</returns>
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
