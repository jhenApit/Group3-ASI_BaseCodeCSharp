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
        [Required(ErrorMessage = "This is a required field.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? MobileNumber { get; set; } 
    }
}
