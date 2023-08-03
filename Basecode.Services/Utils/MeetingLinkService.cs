using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Utils
{
    public class MeetingLinkService : IMeetingLinkService
    {
        /// <summary>
        /// Generates a Microsoft Teams meeting link with specified details.
        /// </summary>
        /// <param name="meetingSubject">The subject or title of the meeting.</param>
        /// <param name="startDate">The date when the meeting starts (in local time).</param>
        /// <param name="startTime">The time of day when the meeting starts (in local time).</param>
        /// <param name="endTime">The time of day when the meeting ends (in local time).</param>
        /// <param name="attendeeEmails">A collection of email addresses of attendees for the meeting.</param>
        /// <returns>A Microsoft Teams meeting link with the provided details.</returns>
        /// <remarks>
        /// The method constructs a Teams meeting URL with the specified parameters, including the meeting subject,
        /// start and end times, and attendee email addresses. The generated link can be used to create a new
        /// Teams meeting with the provided details.
        /// </remarks>
        /// <exception cref="System.Exception">Thrown when an error occurs during link generation.</exception>
        public string GenerateLink(string meetingSubject, DateTime startDate, DateTime startTime, 
            DateTime endTime, IEnumerable<string> attendeeEmails)
        {
            try
            {
                // Encode the meeting subject to be included in the URL
                string encodedSubject = Uri.EscapeDataString(meetingSubject);

                // Format the start date, start time, and end time in ISO 8601 format (UTC)
                string startDateIso8601 = startDate.ToUniversalTime().ToString("yyyy-MM-dd");
                string startTimeIso8601 = startTime.ToUniversalTime().ToString("HH:mm:ss");
                string endTimeIso8601 = endTime.ToUniversalTime().ToString("HH:mm:ss");

                // Encode and join attendee email addresses
                string encodedEmails = string.Join(";", attendeeEmails.Select(email => Uri.EscapeDataString(email)));

                // Construct the Microsoft Teams meeting URL using StringBuilder
                var stringBuilder = new StringBuilder("https://teams.microsoft.com/l/meeting/new?");
                stringBuilder.Append($"subject={encodedSubject}&");
                stringBuilder.Append($"startDate={startDateIso8601}&");
                stringBuilder.Append($"startTime={startTimeIso8601}&");
                stringBuilder.Append($"endTime={endTimeIso8601}&");
                stringBuilder.Append($"email={encodedEmails}&");
                stringBuilder.Append($"attendees={encodedEmails}");

                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during Teams meeting link generation: {ex}");
                return null!;
            }
        }
    }
}
