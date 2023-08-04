using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.JobPostings
{
    public class JobPostingsCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<string> ResponsibilityList { get; set; }
        public string? Responsibilities { get; set; }
        public List<string> QualificationList { get; set; }
        public string? Qualifications { get; set; }
        [Required]
        public WorkSetup WorkSetup { get; set; }
        [Required]
        public WorkingHours Hours { get; set; }
        [Required]
        public JobStatus JobStatus { get; set; }
        [Required]
        public EmploymentType EmploymentType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
