using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;

namespace Basecode.Data.Models
{
    public class ApplicationFormViewModel
    {
        public ApplicantCreationDto? Applicant { get; set; }
        public AddressCreationDto? Address { get; set; }
        public CharacterReferencesCreationDto? CharacterReferences1 { get; set; }
        public CharacterReferencesCreationDto? CharacterReferences2 { get; set; }
    }
}
