using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.Applicants
{
    public class ApplicantCreationDto
    {
        public string? ApplicantId { get; set; }
        public int JobId { get; set; } // Foreign Key
        [Required(ErrorMessage = "Please enter your first name.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your middle name.")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        public string? LastName { get; set; }
        public string? Name => $"{FirstName} {MiddleName} {LastName}";
        [Required(ErrorMessage = "Please enter your birsthday.")]
        public DateTime BirthDate { get; set; }
        public byte[]? Resume { get; set; }
        public byte[]? Photo { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]

        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        public string? Email { get; set; }
        public AdditionalInfo AdditionalInfo { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

    }
}
