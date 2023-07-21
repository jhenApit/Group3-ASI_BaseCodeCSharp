using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.JobPostings
{
    public class JobPostingsUpdationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Responsibilities { get; set; }
        public string? Qualifications { get; set; }
        public WorkSetup WorkSetup { get; set; }
        public WorkingHours Hours { get; set; }
        public JobStatus JobStatus { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
