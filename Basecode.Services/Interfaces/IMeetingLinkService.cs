using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IMeetingLinkService
    {
        public string GenerateLink(string meetingSubject, DateTime startDate, string startTime,
             string endTime, IEnumerable<string> attendeeEmails);
    }
}
