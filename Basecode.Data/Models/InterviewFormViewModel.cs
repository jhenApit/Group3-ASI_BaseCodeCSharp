using Basecode.Data.Dtos.Interviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class InterviewFormViewModel
    {
        public InterviewsCreationDto? InterviewsCreationDto { get; set; }
        public List<Applicants>? ApplicantsList { get; set; }
    }
}
