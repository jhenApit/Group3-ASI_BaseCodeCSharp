using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Applicant
    {
        public int Id { get; set; } // Primary Key
        //public int? JobId { get; set; } // Foreign Key

        //public string TrackerId { get; set; }
        //public byte ResumeCV { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Nationality { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public DateTime ApplicationDate { get; set; }
        //public int? Reference1Id { get; set; } // Foreign Key
        //public int? Reference2Id { get; set; } // Foreign Key
        //public int? AddressId { get; set; } // Foreign Key
        public string? StatusId { get; set; } // Foreign Key
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

    }
}
