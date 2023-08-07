using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IAddressService
    {
        Task<List<Addresses>> RetrieveAllAsync();
        Task AddAsync(AddressCreationDto address);
        Task<Addresses?> GetByIdAsync(int id);
        Task<Addresses?> GetByApplicantIdAsync(int applicantId);
    }
}
