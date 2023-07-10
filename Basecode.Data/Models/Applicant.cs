using System;
using System.ComponentModel.DataAnnotations;

namespace Basecode.Data.Models
{
    public class Applicant
    {
        [Required]
        public int Id { get; set; } // Primary Key

        public int? JobId { get; set; } // Foreign Key

        [Required(ErrorMessage = "ApplicationDate is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDate { get; set; }

        public string? Name { get; set; } 

        [Required(ErrorMessage = "BirthDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public byte[]? Photo { get; set; } 

        [Phone(ErrorMessage = "MobileNo must be a valid phone number.")]
        public int PhoneNumber { get; set; } 

        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string? Email { get; set; } 

        public enum AdditionalInfo 
        {
            // Define enum values here
        }

        public enum ApplicationStatus 
        {
            // Define enum values here
        }

        public enum Requirements 
        {
            // Define enum values here
        }
    }
}
