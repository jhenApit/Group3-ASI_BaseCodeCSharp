using System;
using System.ComponentModel.DataAnnotations;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Models
{
    public class Applicants
    {
        public int Id { get; set; } // Primary Key
        public string? ApplicantId { get; set; }
        public int? JobId { get; set; } // Foreign Key

        [Required(ErrorMessage = "ApplicationDate is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDate { get; set; }

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Name => $"{FirstName} {MiddleName} {LastName}";

        [Required(ErrorMessage = "BirthDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public byte[]? Photo { get; set; } 

        [Phone(ErrorMessage = "MobileNo must be a valid phone number.")]
        public int PhoneNumber { get; set; } 

        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string? Email { get; set; }

        public AdditionalInfo AdditionalInfo { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public Requirements Requirements { get; set; }
    }
}
