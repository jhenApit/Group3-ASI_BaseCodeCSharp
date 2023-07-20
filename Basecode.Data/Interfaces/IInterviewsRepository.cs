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
        IQueryable<Interviews> RetrieveAll();
        Interviews? GetByApplicantId(int applicantId);
        void Add(Interviews Interviews);
        Interviews? GetById(int id);
        void Update(Interviews Interviews);
        void Delete(int id);
    }
}
