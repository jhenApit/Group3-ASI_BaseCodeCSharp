﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;
using static Basecode.Services.Utils.ErrorHandling;
using Basecode.Services.Utils;
using Basecode.Data.Dtos.HrEmployee;

namespace Basecode.Services.Interfaces
{
    public interface IHrEmployeeService
    {
        List<HrEmployee> RetrieveAll();
        HrEmployee GetByEmail(string email);
        void Add(HREmployeeCreationDto hrEmployee);
        HrEmployee GetById(int id);
        HrEmployee GetByUserId(string id);
        void Update(HREmployeeUpdationDto hrEmployee);
        void SemiDelete(int id);
        void PermaDelete(int id);
        public LogContent CreateHrAccount(HREmployeeCreationDto hrEmployee);
        public LogContent EditHrAccount(HREmployeeUpdationDto hrEmployee);
        public LogContent Login(string email, string password);
    }
}
