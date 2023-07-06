using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class HREmployeeCreationDto
    {
        [Required(ErrorMessage = "The 'Name' field is required")]
        [DataType(DataType.Text)]
        [StringLength(150, ErrorMessage = "Name length can't be more than 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The 'Email' field is required")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@asi-dev2\.com$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The 'Password' field is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(30, ErrorMessage = "Password cannot be longer than 30 characters")]
        public string Password { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
