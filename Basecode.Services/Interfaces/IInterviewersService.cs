using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IInterviewersService
    {
        List<Interviewers> RetrieveAll();
        Interviewers? GetByName(string name);
        void Add(Interviewers Interviewers);
        Interviewers? GetById(int id);
        void Update(InterviewersUpdationDto Interviewers);
        void Delete(int id);
    }
}
