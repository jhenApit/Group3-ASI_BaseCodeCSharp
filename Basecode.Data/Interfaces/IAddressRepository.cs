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
        IQueryable<Address> RetrieveAll();
        Address GetByCity(string city);
        void Add(Address address);
        Address GetById(int id);
        void Update(Address address);
        void Delete(int id);
    }
}
