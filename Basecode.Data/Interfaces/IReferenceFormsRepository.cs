using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IReferenceFormsRepository
    {
        IQueryable<ReferenceForms> RetrieveAll();
        ReferenceForms? GetByCharacterReferenceId(int characterReferenceId);
        void Add(ReferenceForms referenceForms);
        ReferenceForms? GetById(int id);
    }
}
