using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;

namespace Basecode.Data.Models
{
    public class CurrentHires
    {
        public int Id { get; set; } 
        public int ApplicantId { get; set; }
        public int PositionId { get; set; }
        public HireStatus HireStatus { get; set; }
        public DateTime HireDate { get; set; } 
    }
}
