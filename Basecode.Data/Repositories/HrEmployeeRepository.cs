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
        public IQueryable<HrEmployee> RetrieveAll()
        {
            return this.GetDbSet<HrEmployee>().Where(e => !e.IsDeleted);
        }

        /// <summary>
        /// Adds a new HrEmployee record to the database.
        /// </summary>
        /// <param name="hrEmployee">The HrEmployee object to add.</param>
        public void Add(HrEmployee hrEmployee)
        {
            _context.HrEmployees.Add(hrEmployee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves a specific HrEmployee record from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the HrEmployee to retrieve.</param>
        /// <returns>The HrEmployee object with the specified ID.</returns>
        public HrEmployee GetById(int id)
        {
            return _context.HrEmployees.Include(e => e.User).FirstOrDefault(e => e.Id == id)!;
        }
        public HrEmployee GetByUserId(string id)
        {
            return _context.HrEmployees.FirstOrDefault(e => e.UserId == id)!;
        }

        /// <summary>
        /// Updates an existing HrEmployee record in the database.
        /// </summary>
        /// <param name="hrEmployee">The updated HrEmployee object.</param>
        public void Update(HrEmployee hrEmployee)
        {
            var existingHrEmployee = _context.HrEmployees.Find(hrEmployee.Id);
            if (existingHrEmployee != null)
            {
                // Update the properties of the existing entity
                existingHrEmployee.Name = hrEmployee.Name;
                existingHrEmployee.Email = hrEmployee.Email;
                existingHrEmployee.Password = hrEmployee.Password;

                // Exclude certain properties from being modified
                _context.Entry(existingHrEmployee).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(existingHrEmployee).Property(x => x.CreatedDate).IsModified = false;

                // Save the changes
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Soft deletes an HrEmployee record in the database.
        /// </summary>
        /// <param name="hrEmployee">The HrEmployee object to soft delete.</param>
        public void SemiDelete(HrEmployee hrEmployee)
        {
            _context.HrEmployees.Update(hrEmployee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Permanently deletes an HrEmployee record from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the HrEmployee to permanently delete.</param>
        public void PermaDelete(int id)
        {
            var data = _context.HrEmployees.Find(id);
            if (data != null)
            {
                _context.HrEmployees.Remove(data);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves a specific HrEmployee record from the database by its email.
        /// </summary>
        /// <param name="email">The email address of the HrEmployee to retrieve.</param>
        /// <returns>The HrEmployee object with the specified email address.</returns>
        public HrEmployee GetByEmail(string email)
        {
            return _context.HrEmployees.FirstOrDefault(e => e.Email == email)!;
        }

    }
}
