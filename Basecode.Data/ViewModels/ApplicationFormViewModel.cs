using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Models;

namespace Basecode.Data.ViewModels
{
    public class ApplicationFormViewModel
    {
        public ApplicantCreationDto? Applicant { get; set; }
        public AddressCreationDto? Address { get; set; }
        public CharacterReferencesCreationDto? CharacterReferences1 { get; set; }
        public CharacterReferencesCreationDto? CharacterReferences2 { get; set; }
        public JobPostings? JobPosting { get; set; }
    }
}
