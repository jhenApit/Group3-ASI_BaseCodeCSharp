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
        [RegularExpression(@"^[\sa-zA-z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        [RegularExpression(@"^[\sa-zA-z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string LastName { get; set; }
        public string? Name => $"{FirstName} {MiddleName} {LastName}";
        [Required(ErrorMessage = "Please enter your birthday.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public byte[]? Resume { get; set; }
        /// <summary>
        /// this is optional so it's nullable
        /// </summary>
        public byte[]? Photo { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
		[RegularExpression(@"(^(09\d{9}|\+639\d{9})$)", ErrorMessage = "Please enter a valid phone number.")]
		public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
		public string? Email { get; set; }
		/// <summary>
        /// nullable
        /// </summary>
		public AdditionalInfo AdditionalInfo { get; set; }
        /// <summary>
        /// this field is automatically set in service
        /// </summary>

        public ApplicationStatus ApplicationStatus { get; set; }

    }
}
