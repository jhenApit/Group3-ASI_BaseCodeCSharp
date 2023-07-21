using System;
using System.Collections.Generic;
using System.ComponentModel;
<<<<<<< HEAD
=======
using System.Drawing;
>>>>>>> 2f3734acf27a794f17e56026a9115d7ca915ea97
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
            [Description("Rejected")]
            Rejected,

            [Description("Shortlisted")]
            Shortlisted,

            [Description("For Screening")]
            ForScreening,

            [Description("For HR Interview")]
            ForHRInterview,

            [Description("For Technical Interview")]
            ForTechnicalInterview,

            [Description("For Technical Exam")]
            ForTechnicalExam,

            [Description("Undergoing Background Check")]
            UndergoingBackgroundCheck,

            [Description("For Final Interview")]
            ForFinalInterview,

            [Description("Undergoing Job Offer")]
            UndergoingJobOffer,

            [Description("Confirmed")]
            Confirmed,

            [Description("Not Confirmed")]
            NotConfirmed,

            [Description("Onboarding")]
            Onboarding,

            [Description("Deployed")]
            Deployed
        }
        public enum HireStatus
        {
<<<<<<< HEAD
            Confirmed,
            NotConfirmed,
=======
            [Description("Confirmed")]
            Confirmed,
            [Description("Not Confirmed")]
            NotConfirmed,
            [Description("Rejected")]
>>>>>>> 2f3734acf27a794f17e56026a9115d7ca915ea97
            Rejected
        }
        public enum Requirements
        {
            CONFIRMED,
            TBC
        }
        public enum AdditionalInfo
        {
<<<<<<< HEAD
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
=======
            [Description("LinkedIn Job Post")]
            LinkedInJobPost,
            [Description("Recruitment Team")]
            RecruitmentTeam,
            [Description("Peers & Friends")]
            PeersFriends,
            [Description("Social Media Platforms")]
            SocialMediaPlatforms,
            [Description("Career Page")]
            CareerPage,
            [Description("Others")]
            Others
        }
        public enum InterviewType
        {
            [Description("HR Interview")]
            HRInterview,
            [Description("Technical Exam")]
            TechnicalExam,
            [Description("Technical Interview")]
            TechnicalInterview,
            [Description("Undergoing Background Check")]
            UndergoingBackgroundCheck,
            [Description("Final Interview")]
            FinalInterview,
            [Description("Failed")]
>>>>>>> 2f3734acf27a794f17e56026a9115d7ca915ea97
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
<<<<<<< HEAD
            EightHourShift,
=======
            [Description("8")]
            EightHourShift,
            [Description("4")]
>>>>>>> 2f3734acf27a794f17e56026a9115d7ca915ea97
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
<<<<<<< HEAD
            FullTime,
            PartTime,
            Temporary,
            Contract,
            Freelance,
=======
            [Description("Full-Time")]
            FullTime,
            [Description("Part-Time")]
            PartTime,
            [Description("Temporary")]
            Temporary,
            [Description("Contract")]
            Contract,
            [Description("Freelance")]
            Freelance,
            [Description("Internship")]
>>>>>>> 2f3734acf27a794f17e56026a9115d7ca915ea97
            Internship
        }

    }
}
