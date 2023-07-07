using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class ErrorHandling
    {
        public class LogContent
        {
            public string ErrorCode { get; set; } = string.Empty;
            public DateTime Time { get; set; }
            public string Message { get; set; } = string.Empty;
            public bool Result { get; set; } = false;
        }

        public static string SetLog(LogContent logContent)
        {
            return "ErrorCode: " + logContent.ErrorCode + ". Message: " + "\"" + logContent.Message + "\"";
        }
    }
}
