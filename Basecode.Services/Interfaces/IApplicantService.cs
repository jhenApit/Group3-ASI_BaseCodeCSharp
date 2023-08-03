using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Models;
using Basecode.Services.Utils;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantService
    {
        Task <Applicants?> GetByIdAsync(int id);
        Task<List<Applicants>> RetrieveAllAsync();
        Task<Applicants?> GetByNameAsync(string fname, string mname, string lname);
        Task<int> AddAsync(ApplicantCreationDto applicant);
        Task<Applicants> GetByApplicantIdAsync(string trackerId);
        Task<List<Applicants>> GetByEmailAsync(string email);
    }
}
