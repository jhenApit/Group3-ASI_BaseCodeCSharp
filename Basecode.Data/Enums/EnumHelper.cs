using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Enums
{
    //class helper for enums
    public class EnumHelper
    {
        /// <summary>
        /// this method is a process to get the enumdescription
        /// </summary>
        /// <param name="value"> the enum value</param>
        /// <returns> it returns the enum description </returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// this method is a process to set a color for a specific status
        /// </summary>
        /// <param name="status">the application status enum</param>
        /// <returns>returns the color set for that passed enum</returns>
        public static Color GetColorForApplicationStatus(Enums.ApplicationStatus status)
        {
            switch (status)
            {
                case Enums.ApplicationStatus.Rejected:
                    return Color.Red;
                case Enums.ApplicationStatus.Shortlisted:
                    return Color.Blue;
                case Enums.ApplicationStatus.ForScreening:
                case Enums.ApplicationStatus.ForHRInterview:
                case Enums.ApplicationStatus.ForTechnicalExam:
                case Enums.ApplicationStatus.ForTechnicalInterview:
                case Enums.ApplicationStatus.UndergoingBackgroundCheck:
                case Enums.ApplicationStatus.ForFinalInterview:
                    return Color.Orange;
                case Enums.ApplicationStatus.UndergoingJobOffer:
                case Enums.ApplicationStatus.Confirmed:
                case Enums.ApplicationStatus.NotConfirmed:
                    return Color.LightGreen;
                case Enums.ApplicationStatus.Onboarding:
                case Enums.ApplicationStatus.Deployed:
                    return Color.DarkGreen;
                case Enums.ApplicationStatus.Received:
                    return Color.SkyBlue;
                default:
                    return Color.Black;
            }
        }

        public static Color GetColorForHireStatus(Enums.HireStatus status)
        {
            switch (status)
            {
                case Enums.HireStatus.Rejected:
                    return Color.Red;
                case Enums.HireStatus.Confirmed:
                    return Color.DarkGreen;
                case Enums.HireStatus.NotConfirmed:
                    return Color.OrangeRed;

                default:
                    return Color.Black;
            }
        }
    }
}
