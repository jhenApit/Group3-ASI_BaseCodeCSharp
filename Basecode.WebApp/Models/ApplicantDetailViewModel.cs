using Basecode.Data.Models;

namespace Basecode.WebApp.Models
{
    public class ApplicantDetailViewModel
    {
        public Applicants Applicant { get; set; }
        public Address Address { get; set; }
        public List<CharacterReferences> CharacterReferences { get; set; }

    }
}
