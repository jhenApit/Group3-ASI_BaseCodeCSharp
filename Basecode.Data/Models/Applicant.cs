using System;
using System.ComponentModel.DataAnnotations;

namespace Basecode.Data.Models
{
    public class Applicant
    {
        [Required]
        public int Id { get; set; } // Primary Key

        public int? JobId { get; set; } // Foreign Key

        [Range(1, 255, ErrorMessage = "ResumeCV must be between 1 and 255.")]
        public byte ResumeCV { get; set; }

        [Required(ErrorMessage = "BirthDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "Nationality cannot exceed 100 characters.")]
        public string? Nationality { get; set; }

        [Phone(ErrorMessage = "MobileNo must be a valid phone number.")]
        public string? MobileNo { get; set; }

        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "ApplicationDate is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ApplicationDate { get; set; }

        public int? Reference1Id { get; set; } // Foreign Key

        public int? Reference2Id { get; set; } // Foreign Key

        public int? AddressId { get; set; } // Foreign Key

        [StringLength(50, ErrorMessage = "StatusId cannot exceed 50 characters.")]
        public string? StatusId { get; set; } // Foreign Key

        [StringLength(100, ErrorMessage = "UpdatedBy cannot exceed 100 characters.")]
        public string? UpdatedBy { get; set; }

        [Required(ErrorMessage = "UpdatedTime is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedTime { get; set; }
    }
}
