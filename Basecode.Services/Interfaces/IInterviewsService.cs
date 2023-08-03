﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.Interviews;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IInterviewsService
    {
        List<Interviews> GetByApplicantId(int applicantId);
        Task<List<Interviews>> RetrieveAllAsync();
        Task<Interviews?> GetByApplicantIdAsync(int applicantId);
        Task AddAsync(InterviewsCreationDto Interviews);
        Task<Interviews?> GetByIdAsync(int id);
        Task UpdateAsync(InterviewsUpdationDto Interviews);
        Task DeleteAsync(int id);
    }
}
