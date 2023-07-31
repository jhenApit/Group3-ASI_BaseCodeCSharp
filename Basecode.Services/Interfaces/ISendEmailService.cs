﻿using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface ISendEmailService
    {
        void SendHrDetailsEmail(HrEmployee hrEmployee, string password);
        void SendNewApplicantEmail(Applicants applicant, string position);
        void SendRegretEmail(string recipientEmail, string subject, string body);
        void SendApprovalEmail(string recipientEmail, string subject, string body);
    }
}
