using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Models.ApplicationTracker;

namespace Basecode.Data.Dtos
{
    public class ApplicationTrackerCreationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
