using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Addresses
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicants Applicant { get; set; }
        public string? Street { get; set; }
        public string? City { get; set;}
        public string? Province { get; set; }
        public string? ZipCode { get; set; }

    }
}
