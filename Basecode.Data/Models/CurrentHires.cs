using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class CurrentHires
    {
        public int Id { get; set; } 
        public int ApplicantId { get; set; }
        public int PositionId { get; set; }
        public enum HStatus
        {
            //values
        }
        public HStatus HireStatus { get; set; }
        public DateTime HireDate { get; set; }
    }
}
