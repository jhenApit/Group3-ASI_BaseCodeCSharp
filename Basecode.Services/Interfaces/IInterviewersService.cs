using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Interviewers;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IInterviewersService
    {
        Task<List<Interviewers>> RetrieveAllAsync();
        Task<Interviewers?> GetByNameAsync(string name);
        Task AddAsync(InterviewersCreationDto Interviewers);
        Task<Interviewers?> GetByIdAsync(int id);
        Task UpdateAsync(InterviewersUpdationDto Interviewers);
        Task DeleteAsync(int id);
    }
}
