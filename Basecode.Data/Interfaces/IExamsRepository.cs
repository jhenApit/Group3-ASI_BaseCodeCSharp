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
        IQueryable<Exams> RetrieveAll();
        Exams? GetByApplicantId(int applicantId);
        void Add(Exams exams);
        Exams? GetById(int id);
        void Update(Exams exams);
        void Delete(int id);
    }
}
