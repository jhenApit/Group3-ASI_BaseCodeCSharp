using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Interfaces
{
    public interface IInterviewsRepository
    {
        Task<IQueryable<Interviews>> GetByApplicantIdAsync(int applicantId);
        Task<IQueryable<Interviews>> RetrieveAllAsync();
        Task AddAsync(Interviews Interviews);
        Task<Interviews?> GetByIdAsync(int id);
        Task UpdateAsync(Interviews Interviews);
        Task DeleteAsync(int id);
        Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAndDateAsync(int interviewerId, DateTime interviewDate);
        Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAsync(int interviewerId);
        Task<IEnumerable<Interviews>> GetInterviewsByApplicantAsync(int applicantId);
        Task<bool> GetByApplicantIdAndInterviewTypeAsync(int applicantId, InterviewType interviewType);
    }
}
