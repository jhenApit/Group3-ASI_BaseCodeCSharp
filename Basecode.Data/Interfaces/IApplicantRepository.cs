using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IApplicantRepository
    {
        Task<Applicants?> GetByIdAsync(int id);
        Task<IQueryable<Applicants>> RetrieveAllAsync();
        Task<Applicants?> GetByNameAsync(string fname, string mname, string lname);
        Task AddAsync(Applicants applicant);
        Task<Applicants?> GetByApplicantIdAsync(string trackerId);
        Task<IQueryable<Applicants>> GetByEmailAsync(string email);
    }
}
