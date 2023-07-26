using System;
using System.Collections.Generic;
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
            //values
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

        public enum EmailType
        {
            HRCreationAccountEmail,

            //Application and Screening
            ScreeningEmailNotificationHR,
            ScreeningEmailNotificationApplicant,
            ScreeningEmailApplicantID,
            ScreeningEmailofApproval,
            ScreeningEmailofRegrets,

            //Interviews
            InterviewEmailNotificationInterviewer, 
            InterviewEmailNotificationApplicant,
            InterviewReminderEmailInterviewer,
            InterviewReminderEmailHR,
            InterviewEmailofApprovalApplicantSchedule,
            InterviewEmailNotificationAcceptedSchedule,
            InterviewEmailOfApproval,
            InterviewEmailOfRegrets,

            //Background Check
            BackgroundCheckEmailofGratitude,
            BackgroundCheckReminderEmail,
            BackgroundCheckEmailNotification,
            BackgroundCheckEmailOfApproval,

            //Job Offer
            JobOfferEmailOfApproval,
            JobOfferEmailNotification,
            JobOfferConfirmationEmail
        }
    }
}
