using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IAddressService
    {
        List<Address> RetrieveAll();
        Address GetByCity(string city);
        void Add(AddressCreationDto address);
        Address GetById(int id);
    }
}
