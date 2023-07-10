using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Address;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IAddressService
    {
        List<Address> RetrieveAll();
        Address GetByEmail(string email);
        void Add(AddressCreationDto address);
        Address GetById(int id);
        void Update(AddressUpdationDto address);
        void Delete(int id);
    }
}
