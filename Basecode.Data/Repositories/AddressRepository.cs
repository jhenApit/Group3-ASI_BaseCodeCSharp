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
        public void Add(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public Address GetByCity(string city)
        {
            return _context.Addresses.FirstOrDefault(e => e.City == city);
        }

        public Address GetById(int id)
        {
            return _context.Addresses.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<Address> RetrieveAll()
        {
            return this.GetDbSet<Address>();
        }
    }
}
