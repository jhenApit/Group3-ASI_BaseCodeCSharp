using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class CharacterReferencesCreationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string? Name { get; set; }
        public string? Relationship { get; set; }
        public string? Email { get; set; } 
        public string? MobileNumber { get; set; } 
    }
}
