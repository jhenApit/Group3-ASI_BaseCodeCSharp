using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IReferenceFormsService
    {
        List<ReferenceForms> RetrieveAll();
        ReferenceForms? GetByCharacterReferenceId(int characterReferenceId);
        void Add(ReferenceFormsCreationDto ReferenceForms);
        ReferenceForms? GetById(int id);
    }
}
