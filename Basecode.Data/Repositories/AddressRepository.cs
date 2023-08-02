using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        private readonly BasecodeContext _context;
        public AddressRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        /// <summary>
        /// Adds an address to the context and saves changes.
        /// </summary>
        /// <param name="address">The address to add.</param>
        public async Task AddAsync(Addresses address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an address by the specified ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>The address matching the ID, or null if not found.</returns>
        public async Task<Addresses?> GetByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Retrieves all addresses.
        /// </summary>
        /// <returns>An IQueryable of all addresses.</returns>
        public async Task<IQueryable<Addresses>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Addresses>());
        }

    }
}
