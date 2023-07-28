using Basecode.Data.Dtos;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class DashboardView
    {
        public HrEmployee User { get; set; }
        public int JobCount { get; set; }
        public List<Applicants> Candidates { get; set; }
        public int EmployeeCount { get; set; }
        public List<Interviews> Schedules { get; set; }

    }
}
