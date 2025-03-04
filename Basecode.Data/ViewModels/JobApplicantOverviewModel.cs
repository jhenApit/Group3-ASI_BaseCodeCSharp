﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos.Applicants;
using Basecode.Data.Models;

namespace Basecode.Data.ViewModels
{
    public class JobApplicantOverviewModel
    {
        public List<JobPostings> jobPostings { get; set; }
        public List<Applicants>? applicants { get; set; }
    }
}
