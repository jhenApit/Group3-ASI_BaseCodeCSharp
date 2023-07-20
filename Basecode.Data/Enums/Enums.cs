using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Enums
{
    /// <summary>
    /// Class for enumerated values
    /// </summary>
    public class Enums
    {
        public enum ApplicationStatus
        {
            Shortlisted,
            ForScreening,
            ForHRInterview,
            ForTechnicalInterview,
            ForTechnicalExam,
            UndergoingBackgroundCheck,
            ForFinalInterview,
            UndergoingJobOffer,
            Confirmed,
            NotConfirmed,
            Onboarding,
            Deployed
        }
        public enum HireStatus
        {
            Confirmed,
            NotConfirmed,
            Rejected
        }
        public enum Requirements
        {
            CONFIRMED,
            TBC
        }
        public enum AdditionalInfo
        {
            LinkedInJobPost,
            AllianceRecreuitmentTeam,
            FriendsAndPeers,
            Facebook,
            Twitter,
            Instagram,
            AllianceCareersPage
        }
        public enum InterviewType
        {
            HRInterview,
            TechnicalExam,
            TechnicalInterview,
            UndergoingBackgroundCheck,
            FinalInterview,
            Failed
        }
        public enum WorkSetup
        {
            Online,
            Onsite,
            Hybrid
        }
        public enum WorkingHours
        {
            EightHourShift,
            FourHourShift
        }
        public enum JobStatus
        {
            Open,
            Urgent,
            Closed,
            OnHold
        }
        public enum EmploymentType
        {
            FullTime,
            PartTime,
            Temporary,
            Contract,
            Freelance,
            Internship
        }

    }
}
