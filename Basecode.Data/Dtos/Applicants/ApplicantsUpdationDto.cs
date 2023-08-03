using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.Applicants
{
    public class ApplicantsUpdationDto
    {
        public int Id { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
