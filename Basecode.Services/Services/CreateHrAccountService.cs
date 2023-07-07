using Basecode.Data.Dtos;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class CreateHrAccountService: ErrorHandling, ICreateHrAccountService
    {
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            LogContent logContent = new LogContent();

            if(!hrEmployee.Email.Contains("@asi-dev2.com"))
            {
                logContent.Result = false;
                logContent.ErrorCode = "INVALID";
                logContent.Message = "Email is not Alliance Email";
            }

            return logContent;
        }
    }
}
