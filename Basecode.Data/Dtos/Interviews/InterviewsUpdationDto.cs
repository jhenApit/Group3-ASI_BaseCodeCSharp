using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Models.Interviews;

namespace Basecode.Data.Dtos.Interviews
{
    public class InterviewsUpdationDto
    {
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }
        public IType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public bool Results { get; set; }
    }
}
