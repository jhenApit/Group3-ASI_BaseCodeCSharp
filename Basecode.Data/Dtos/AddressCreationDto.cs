using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class AddressCreationDto
    {
        public int ApplicantId { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? Province { get; set; }
        [Required(ErrorMessage = "This is a required field.")]
        public string? ZipCode { get; set; } 
    }
}
