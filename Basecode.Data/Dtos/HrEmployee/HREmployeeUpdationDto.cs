using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basecode.Data.Dtos.HrEmployee
{
    public class HREmployeeUpdationDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "The 'Email' field is required")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@gmail\.com$", ErrorMessage = "Email is not an Alliance email")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } = string.Empty;
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsAdmin { get; set; }
    }
}
