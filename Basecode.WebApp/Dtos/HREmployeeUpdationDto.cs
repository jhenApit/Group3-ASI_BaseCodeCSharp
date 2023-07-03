using Basecode.WebApp.Models;
using Humanizer;
using System.Collections.Specialized;

namespace Basecode.WebApp.Dtos
{
    public class HREmployeeUpdationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
