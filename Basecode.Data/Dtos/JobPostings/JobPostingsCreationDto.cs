using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class JobPostingsCreationDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Responsibilities { get; set; }
        public string? Qualifications { get; set; }
        public string? WorkSetup { get; set; }
        public int Hours { get; set; }
        public bool IsActive { get; set; }
        public string? Type { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedById { get; set; }
    }
}
