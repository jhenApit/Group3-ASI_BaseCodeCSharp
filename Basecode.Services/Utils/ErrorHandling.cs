using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Utils
{
    public class ErrorHandling : IErrorHandling
    {
        public string SetLog(LogContent logContent, string location)
        {
            return $"Location: {location} | ErrorCode: {logContent.ErrorCode}. Message: \"{logContent.Message}\"";
        }
    }
}
