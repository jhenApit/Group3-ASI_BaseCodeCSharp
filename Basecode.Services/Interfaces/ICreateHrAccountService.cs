using Basecode.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Interfaces
{
    public interface ICreateHrAccountService
    {
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee);
    }
}
