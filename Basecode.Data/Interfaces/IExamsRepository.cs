using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IExamsRepository
    {
        Task<IQueryable<Exams>> RetrieveAllAsync();
        Task<Exams?> GetByApplicantIdAsync(int applicantId);
        Task AddAsync(Exams exams);
        Task<Exams?> GetByIdAsync(int id);
        Task UpdateAsync(Exams exams);
        Task DeleteAsync(int id);
    }
}
