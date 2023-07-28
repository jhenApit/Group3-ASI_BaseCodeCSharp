using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class CharacterReferencesCreationDto
    {
        public int ApplicantId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? Relationship { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Please enter a valid email.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"((^(\+)(\d){12}$)|(^\d{11}$))", ErrorMessage = "Please enter a phone number email.")]
        public string? MobileNumber { get; set; } 
    }
}
