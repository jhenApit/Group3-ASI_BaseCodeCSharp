using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class ReferenceForms
    {
        public int Id { get; set; }
        public int CharacterReferenceId { get; set; }
        [ForeignKey("CharacterReferenceId")]
        public CharacterReferences CharacterReference { get; set; }
        public DateTime AnsweredDate { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? Answer5 { get; set; }
        public string? Answer6 { get; set; }
        public string? Answer7 { get; set; }
        public string? Answer8 { get; set; }
        public string? Answer9 { get; set; }
        public string? Answer10 { get; set; }
    }
}
