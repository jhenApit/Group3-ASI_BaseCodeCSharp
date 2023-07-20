using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos
{
    public class ApplicantCreationDto
    {
        public string? ApplicantId { get; set; }
        public int? JobId { get; set; } // Foreign Key
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }

        public byte[]? Photo { get; set; }

        public int PhoneNumber { get; set; }
        public string? Email { get; set; }

        public AdditionalInfo AdditionalInfo { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public Requirements Requirements { get; set; }

    }
}
