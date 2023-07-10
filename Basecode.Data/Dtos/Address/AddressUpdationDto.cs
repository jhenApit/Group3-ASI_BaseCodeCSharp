using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos.Address
{
    public class AddressUpdationDto
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public Type? Province { get; set; }
        public Type? ZipCode { get; set; }
    }
}
