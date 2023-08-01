using Basecode.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Services.Interfaces
{
    public interface IErrorHandling
    {
        string SetLog(LogContent logContent, string location);
    }
}
