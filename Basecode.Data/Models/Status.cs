using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.Models
{
    public class Status
    {
        public int Id { get; set; }
        public bool IsRejected { get; set; }
        public bool IsShortlisted { get; set; }
        public bool IsForScreening { get; set; }
        public bool IsForHRInterview { get; set; }
        public bool IsForTechnicalInterview { get; set; }
        public bool IsForTechnicalExam { get; set; }
        public bool IsUndergoingBackgroundCheck { get; set; }
        public bool IsForFinalInterview { get; set; }
        public bool IsUndergoingJobOffer { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsNotConfirmed { get; set; }
        public bool IsOnboarding { get; set; }
        public bool IsDeployed { get; set; }
    }
}
