using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IApplicantService
    {
        Applicant GetById(int id);
    }
}
