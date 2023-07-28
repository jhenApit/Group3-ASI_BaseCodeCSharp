using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class HrAccountViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "The 'Email' field is required")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@gmail\.com$", ErrorMessage = "Email is not a valid email")] //Email is not an Alliance email
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } = string.Empty;

    }
}
