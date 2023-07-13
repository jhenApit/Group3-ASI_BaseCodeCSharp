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
        List<Exams> RetrieveAll();
        Exams? GetByApplicantId(int applicantId);
        void Add(ExamCreationDto Exams);
        Exams? GetById(int id);
        void Update(ExamUpdationDto Exams);
        void Delete(int id);
    }
}
