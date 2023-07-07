using Basecode.Data.Dtos;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class EditHrAccountService : ErrorHandling, IEditHrAccountService
    {
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee) 
        { 
            LogContent logContent = new LogContent();

            if (hrEmployee.Name.Length < 2)
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Length Validation";
                logContent.Message = "Name must be at least 2 characters long";
            }
            else if (hrEmployee.Name.Length < 2)
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Length Validation";
                logContent.Message = "Name cannot be longer than 150 characters";
            }
            else if (!hrEmployee.Email.Contains("@asi-dev2.com"))
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Invalid Email";
                logContent.Message = "Email is not Alliance Email";
            }
            else if (hrEmployee.Email.Length > 50)
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Length Validation ";
                logContent.Message = "Email cannot be longer than 50 characters long";
            }
            else if (hrEmployee.Password!.Length < 6)
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Length Validation";
                logContent.Message = "Password must be at least 6 characters long";
            }
            else if (hrEmployee.Password!.Length > 30)
            {
                logContent.Result = false;
                logContent.ErrorCode = "ERR! Length Validation";
                logContent.Message = "Password cannot be longer than 30 characters";
            }
            else
            {
                logContent.Result = true;
            }

            return logContent;
        }
    }
}
