using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Models
{
    public class Applicants
    {
        public int Id { get; set; } // Primary Key this should be the identity
        public string? ApplicantId { get; set; }
        public int JobId { get; set; } // Foreign Key to the JobPostings table
        [ForeignKey("JobId")]
        public JobPostings? Job { get; set; }

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
        public byte[]? Resume { get; set; }
        public byte[]? Photo { get; set; } 

        [Phone(ErrorMessage = "MobileNo must be a valid phone number.")]
        public string? PhoneNumber { get; set; } 

        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string? Email { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public AdditionalInfo AdditionalInfo { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public Requirements Requirements { get; set; }
    }
}
