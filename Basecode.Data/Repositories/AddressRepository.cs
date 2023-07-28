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
        public void Add(Addresses address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves an address by the specified city.
        /// </summary>
        /// <param name="city">The city to search for.</param>
        /// <returns>The address matching the city, or null if not found.</returns>
        public Addresses GetByCity(string city)
        {
            return _context.Addresses.FirstOrDefault(e => e.City == city)!;
        }

        /// <summary>
        /// Retrieves an address by the specified ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>The address matching the ID, or null if not found.</returns>
        public Addresses GetById(int id)
        {
            return _context.Addresses.FirstOrDefault(e => e.Id == id)!;
        }

        /// <summary>
        /// Retrieves all addresses.
        /// </summary>
        /// <returns>An IQueryable of all addresses.</returns>
        public IQueryable<Addresses> RetrieveAll()
        {
            return this.GetDbSet<Addresses>();
        }

    }
}
