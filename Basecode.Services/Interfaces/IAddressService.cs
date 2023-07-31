using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IAddressService
    {
        List<Addresses> RetrieveAll();
        Addresses GetByCity(string city);
        void Add(AddressCreationDto address);
        Addresses GetById(int id);
    }
}
