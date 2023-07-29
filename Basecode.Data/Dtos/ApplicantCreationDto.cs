using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos
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
        [Required(ErrorMessage = "Please enter your birthday.")]
        public DateTime BirthDate { get; set; }
        public byte[] Resume { get; set; }
        /// <summary>
        /// this is optional so it's nullable
        /// </summary>
        public byte[]? Photo { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
		[RegularExpression(@"((^(\+)(\d){12}$)|(^\d{11}$))", ErrorMessage = "Please enter a phone number email.")]
		public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
		[MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
		[RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Please enter a valid email.")]
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
