using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Interviews
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }
        public enum IType
        {
            //values
        }
        public IType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public bool Results { get; set; }           
    }
}
