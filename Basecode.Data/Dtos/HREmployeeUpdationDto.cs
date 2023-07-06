using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Basecode.Data.Dtos
{
    public class HREmployeeUpdationDto
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [JsonProperty(PropertyName = "email")]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        [JsonProperty(PropertyName = "password")]
        public string? Password { get; set; }
    }
}
