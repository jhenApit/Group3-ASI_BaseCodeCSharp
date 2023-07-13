using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos.Exams
{
    public class ExamUpdationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int ProctorId { get; set; }
        public string? ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public bool Results { get; set; }
    }
}
