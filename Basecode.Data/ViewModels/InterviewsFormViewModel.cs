using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Models;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.ViewModels
{
    public class InterviewsFormViewModel
    {
        public Interviewers? Interviewer { get; set; }
        public List<Applicants>? ApplicantsList { get; set; }
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
    }
}
