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
        [RegularExpression(@"^[\sa-zA-z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        [RegularExpression(@"^[\sa-zA-z\s]+$", ErrorMessage = "This field must contain only letters")]
        public string Relationship { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"(^(09\d{9}|\+639\d{9})$)", ErrorMessage = "Please enter a valid phone number.")]
        public string MobileNumber { get; set; } 
    }
}
