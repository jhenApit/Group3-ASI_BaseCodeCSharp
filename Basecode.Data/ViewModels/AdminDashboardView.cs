using Basecode.Data.Dtos;
using Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basecode.Data.ViewModels
{
    public class AdminDashboardView
    {
        public HrEmployee User { get; set; }
        public JobPostings Jobs { get; set; }
        public Applicants Candidates { get; set; }
        public CurrentHires Employees { get; set; }
        public Interviews Schedules { get; set; }
    }
}
