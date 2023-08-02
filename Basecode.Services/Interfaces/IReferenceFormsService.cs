using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IReferenceFormsService
    {
        Task<List<ReferenceForms>> RetrieveAllAsync();
        Task<ReferenceForms?> GetByCharacterReferenceIdAsync(int characterReferenceId);
        Task AddAsync(ReferenceFormsCreationDto ReferenceForms);
        Task<ReferenceForms?> GetByIdAsync(int id);
    }
}
