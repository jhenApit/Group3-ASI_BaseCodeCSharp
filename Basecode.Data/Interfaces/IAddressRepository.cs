using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IAddressRepository
    {
        Task<IQueryable<Addresses>> RetrieveAllAsync();
        Task AddAsync(Addresses address);
        Task<Addresses> GetByIdAsync(int id);
    }
}
