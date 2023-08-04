using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class HrEmployeeRepository : BaseRepository , IHrEmployeeRepository
    { 
        private readonly BasecodeContext _context;

        public HrEmployeeRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base (unitOfWork) 
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all non-deleted HrEmployee records from the database.
        /// </summary>
        /// <returns>An IQueryable of HrEmployee.</returns>
        public async Task<IQueryable<HrEmployee>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<HrEmployee>().Include(e => e.User));
        }

        /// <summary>
        /// Adds a new HrEmployee record to the database.
        /// </summary>
        /// <param name="hrEmployee">The HrEmployee object to add.</param>
        public async Task AddAsync(HrEmployee hrEmployee)
        {
            await _context.HrEmployees.AddAsync(hrEmployee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a specific HrEmployee record from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the HrEmployee to retrieve.</param>
        /// <returns>The HrEmployee object with the specified ID.</returns>
        public async Task<HrEmployee?> GetByIdAsync(int id)
        {
            return await _context.HrEmployees.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Retrieves an HR employee from the database by their user ID.
        /// </summary>
        /// <param name="id">The user ID of the HR employee to retrieve.</param>
        /// <returns>The HR employee with the specified user ID, or null if not found.</returns>

        public async Task<HrEmployee?> GetByUserIdAsync(string id)
        {
            return await _context.HrEmployees.FirstOrDefaultAsync(e => e.UserId == id)!;
        }

        /// <summary>
        /// Updates an existing HrEmployee record in the database.
        /// </summary>
        /// <param name="hrEmployee">The updated HrEmployee object.</param>
        public async Task UpdateAsync(HrEmployee hrEmployee)
        {
            var existingHrEmployee = await _context.HrEmployees.FindAsync(hrEmployee.Id);
            if (existingHrEmployee != null)
            {
                // Update the properties of the existing entity
                existingHrEmployee.Name = hrEmployee.Name;
                existingHrEmployee.Email = hrEmployee.Email;
                existingHrEmployee.Password = hrEmployee.Password;
                existingHrEmployee.ModifiedBy = hrEmployee.ModifiedBy;
                existingHrEmployee.ModifiedDate = hrEmployee.ModifiedDate;

                // Exclude certain properties from being modified
                _context.Entry(existingHrEmployee).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(existingHrEmployee).Property(x => x.CreatedDate).IsModified = false;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Permanently deletes an HrEmployee record from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the HrEmployee to permanently delete.</param>
        public async Task DeleteAsync(int id)
        {
            var data = await _context.HrEmployees.FindAsync(id);
            if (data != null)
            { 
                _context.HrEmployees.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a specific HrEmployee record from the database by its email.
        /// </summary>
        /// <param name="email">The email address of the HrEmployee to retrieve.</param>
        /// <returns>The HrEmployee object with the specified email address.</returns>
        public async Task<HrEmployee?> GetByEmailAsync(string email)
        {
            return await _context.HrEmployees.FirstOrDefaultAsync(e => e.Email == email)!;
        }

    }
}
