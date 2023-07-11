using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos.JobPostings
{
    public class JobPostingsUpdationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Responsibilities { get; set; }
        public string? Qualifications { get; set; }
        public string? WorkSetup { get; set; }
        public int Hours { get; set; }
        public bool IsActive { get; set; }
        public string? Type { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
