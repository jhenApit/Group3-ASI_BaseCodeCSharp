using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class ApplicationTracker
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string? TrackerId { get; set; }
        public enum ApplicationStatus
        {
            //values
        }
        public ApplicationStatus Status { get; set; }
    }
}
