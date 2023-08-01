using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IInterviewsService
    {
        List<Interviews> RetrieveAll();
        List<Interviews> GetByApplicantId(int applicantId);
        void Add(InterviewsCreationDto Interviews);
        Interviews? GetById(int id);
        void Update(InterviewsUpdationDto Interviews);
        void Delete(int id);
    }
}
