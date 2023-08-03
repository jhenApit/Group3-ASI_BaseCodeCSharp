using Basecode.Data.Models;

namespace Basecode.WebApp.Models
{
    public class ApplicantDetailViewModel
    {
        public Applicants Applicant { get; set; }
        public Addresses Address { get; set; }
        public List<CharacterReferences> CharacterReferences { get; set; }
        public List<Interviews> Interviews { get; set; }

    }
}
