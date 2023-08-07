using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Dtos.Interviewers
{
    public class InterviewersCreationDto
    {
        public int Id { get; set; }

        [RegularExpression(@"^[\sa-zA-z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
