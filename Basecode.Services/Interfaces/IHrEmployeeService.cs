using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using Basecode.Data.Dtos;
using static Basecode.Services.Services.ErrorHandling;

namespace Basecode.Services.Interfaces
{
    public interface IHrEmployeeService
    {
        List<HrEmployee> RetrieveAll();
        HrEmployee GetByEmail(string email);
        void Add(HREmployeeCreationDto hrEmployee);
        HrEmployee GetById(int id);
        void Update(HREmployeeUpdationDto hrEmployee);
        void SemiDelete(int id);
        void PermaDelete(int id);
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee);
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee);
        public bool IsEmailValid(string email);
        public bool IsEmailValid(string email);
        public bool IsEmailValid(string email);
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee);
    }
}
