using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //values
        }
        public enum Requirements
        {
            //values
        }
        public enum AdditionalInfo
        {
            //values
        }
        public enum InterviewType
        {
            //values
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
