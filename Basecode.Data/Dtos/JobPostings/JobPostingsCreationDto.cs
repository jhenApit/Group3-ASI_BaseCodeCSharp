using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.JobPostings
{
    public class JobPostingsCreationDto
    {
        public string? Name { get; set; }
        public List<string> Descriptions { get; set; }
        public string? Description { get; set; }
        public List<string> ResponsibilityList { get; set; }
        public string? Responsibilities { get; set; }
        public List<string> QualificationList { get; set; }
        public string? Qualifications { get; set; }
        public WorkSetup WorkSetup { get; set; }
        public WorkingHours Hours { get; set; }
        public JobStatus JobStatus { get; set; }
        public EmploymentType EmploymentType { get; set; }
    }
}
