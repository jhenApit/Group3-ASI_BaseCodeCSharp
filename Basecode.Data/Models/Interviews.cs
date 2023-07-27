using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Models
{
    public class Interviews
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public bool Results { get; set; }           
    }
}
