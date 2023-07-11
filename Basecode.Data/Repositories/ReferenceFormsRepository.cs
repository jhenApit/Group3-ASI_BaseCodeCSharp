using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class ReferenceFormsRepository : BaseRepository, IReferenceFormsRepository
    {
        private readonly BasecodeContext _context;
        public ReferenceFormsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public void Add(ReferenceForms referenceForms)
        {
            _context.ReferenceForms.Add(referenceForms);
            _context.SaveChanges();
        }

        public ReferenceForms? GetByCharacterReferenceId(int characterReferenceId)
        {
            return _context.ReferenceForms.FirstOrDefault(e => e.CharacterReferenceId == characterReferenceId);
        }

        public ReferenceForms? GetById(int id)
        {
            return _context.ReferenceForms.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<ReferenceForms> RetrieveAll()
        {
            return this.GetDbSet<ReferenceForms>();
        }
    }

}
