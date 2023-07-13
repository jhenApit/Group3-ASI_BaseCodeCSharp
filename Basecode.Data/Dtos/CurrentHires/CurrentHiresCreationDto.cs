using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Models.CurrentHires;

namespace Basecode.Data.Dtos.CurrentHires
{
    public class CurrentHiresCreationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int PositionId { get; set; }
        public HStatus HireStatus { get; set; }
        public DateTime HireDate { get; set; }
    }
}
