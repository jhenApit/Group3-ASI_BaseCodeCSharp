using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IInterviewersRepository
    {
        Task<IQueryable<Interviewers>> RetrieveAllAsync();
        Task<Interviewers?> GetByNameAsync(string name);
        Task AddAsync(Interviewers Interviewers);
        Task<Interviewers?> GetByIdAsync(int id);
        Task UpdateAsync(Interviewers Interviewers);
        Task DeleteAsync(int id);
    }
}
