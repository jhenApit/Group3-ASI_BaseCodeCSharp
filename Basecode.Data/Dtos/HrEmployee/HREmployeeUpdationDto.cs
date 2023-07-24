using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basecode.Data.Dtos.HrEmployee
{
    public class HREmployeeUpdationDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "The 'Name' field is required")]
        [JsonProperty(PropertyName = "name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters.")]
        [RegularExpression(@"^[\sa-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "The 'Email' field is required")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@asi-dev2\.com$", ErrorMessage = "Email is not an Alliance email")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter password to save changes")]
        [Display(Name = "Password")]
        [JsonProperty(PropertyName = "password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(30, ErrorMessage = "Password cannot be longer than 30 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
