using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos
{
    public class AddressCreationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Province { get; set; } 
        public string? ZipCode { get; set; } 
    }
}
