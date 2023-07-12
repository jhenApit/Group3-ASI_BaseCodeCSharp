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
        IQueryable<Interviewers> RetrieveAll();
        Interviewers? GetByName(string name);
        void Add(Interviewers Interviewers);
        Interviewers? GetById(int id);
        void Update(Interviewers Interviewers);
        void Delete(int id);
    }
}
