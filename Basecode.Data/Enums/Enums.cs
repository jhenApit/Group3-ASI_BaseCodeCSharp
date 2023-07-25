using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            [Description("Confirmed")]
            Confirmed,
            [Description("Not Confirmed")]
            NotConfirmed,
            [Description("Rejected")]
            Rejected
        }
        public enum Requirements
        {
            CONFIRMED,
            TBC
        }
        public enum AdditionalInfo
        {
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
            Failed
        }
        public enum WorkSetup
        {
            [Description("Online")]
            Online,
            [Description("On-site")]
            Onsite,
            [Description("Hybrid")]
            Hybrid
        }
        public enum WorkingHours
        {
            [Description("8-Hour shift")]
            EightHourShift,
            [Description("4-Hour shift")]
            FourHourShift
        }
        public enum JobStatus
        {
            [Description("Open")]
            Open,
            [Description("Urgent")]
            Urgent,
            [Description("Closed")]
            Closed,
            [Description("On hold")]
            OnHold
        }
        public enum EmploymentType
        {
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
            Internship
        }

    }
}
