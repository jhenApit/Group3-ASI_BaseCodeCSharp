using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Dtos.CurrentHires
{
    public class CurrentHiresUpdationDto
    {
        public int Id { get; set; }
        public HireStatus HireStatus { get; set; }
        public DateTime HireDate { get; set; }
    }
}
