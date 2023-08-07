using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Exams;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IExamsService
    {
        Task<List<Exams>> RetrieveAllAsync();
        Task<Exams?> GetByApplicantIdAsync(int applicantId);
        Task AddAsync(ExamCreationDto Exams);
        Task<Exams?> GetByIdAsync(int id);
        Task UpdateAsync(ExamUpdationDto Exams);
        Task DeleteAsync(int id);
    }
}
