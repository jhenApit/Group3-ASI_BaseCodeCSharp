using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Exams
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicants Applicant { get; set; }
        public int ProctorId { get; set; }
        public string? ExamType { get; set; }
        public DateTime ExamDate { get; set; }
        public bool Results { get; set; }
    }
}
