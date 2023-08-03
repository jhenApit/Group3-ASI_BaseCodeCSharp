using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IInterviewsRepository
    {
        IQueryable<Interviews> GetByApplicantId(int applicantId);
        Task<IQueryable<Interviews>> RetrieveAllAsync();
        Task<Interviews?> GetByApplicantIdAsync(int applicantId);
        Task AddAsync(Interviews Interviews);
        Task<Interviews?> GetByIdAsync(int id);
        Task UpdateAsync(Interviews Interviews);
        Task DeleteAsync(int id);
    }
}
