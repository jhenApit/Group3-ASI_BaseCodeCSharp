﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IReferenceFormsRepository
    {
        Task<IQueryable<ReferenceForms>> RetrieveAllAsync();
        Task<ReferenceForms?> GetByCharacterReferenceIdAsync(int characterReferenceId);
        Task AddAsync(ReferenceForms referenceForms);
        Task<ReferenceForms?> GetByIdAsync(int id);
    }
}
