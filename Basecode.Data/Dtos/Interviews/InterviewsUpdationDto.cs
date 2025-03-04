﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basecode.Data.Enums.Enums;
using static Basecode.Data.Models.Interviews;

namespace Basecode.Data.Dtos.Interviews
{
    public class InterviewsUpdationDto
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int InterviewerId { get; set; }
        public InterviewType InterviewType { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public bool Results { get; set; }
    }
}
